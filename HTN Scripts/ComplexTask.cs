using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace HierarchicalTaskNetwork
{
public class ComplexTask: Task
{
    public delegate Task[] DecompositionMethod();

    private Plan _taskExecutionPlan;
    
    public override TaskType Type
    {
        get { return TaskType.Complex; }
    }
    
    readonly protected DecompositionMethod Decompose;
    
    public ComplexTask(string name,
        CoroutineStarter coroutineRunner,
        DecompositionMethod decompose,
        Condition[] conditions = null, 
        Condition[] rules = null) : base(name, coroutineRunner, conditions, rules)
    {
        Decompose = decompose;
    }

    public override Task[] DecomposeTask()
    {
        return Decompose.Invoke();
    }

    public override void StartExecution()
    {
        SetStatus(TaskStatus.InProgress);
        _taskExecutionPlan = new Plan(this, this.Name);
        base.StartExecution();
    }

    protected override IEnumerator Execution()
    {
        this.SetStatus(TaskStatus.InProgress);
        if (!CheckPreConditions())
        {
            this.SetStatus(TaskStatus.Failure);
            yield break;
        }

        while (this.Status != TaskStatus.Complete)
        {
            if (!CheckTaskIntegrity() || _taskExecutionPlan.Status == Plan.PlanStatus.Failure)
            {
                this.SetStatus(TaskStatus.Failure);
                yield break;
            }

            _taskExecutionPlan.PlanIterate();
            
            if (_taskExecutionPlan.Status == Plan.PlanStatus.Complete)
            {
                this.SetStatus(TaskStatus.Complete);
            }
            yield return null;
        }
    }
}
}
