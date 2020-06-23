using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalTaskNetwork
{

    #region Exceptions

    public class NoneTaskStatusException : Exception
    {
        public override string Message
        {
            get { return "Plan has encountered a task without status"; }
        }
    }

    #endregion

    public class Plan
    {
        private string planName;
        private Task rootTask;
        private Task currentTask;
        private Queue<Task> nextTask;


        public enum PlanStatus
        {
            InProgress,
            Complete,
            Failure
        }

        private PlanStatus planStatus;

        public Plan(Task root, string planName)
        {
            this.planName = planName;
            var tasks = root.DecomposeTask();
            rootTask = root;
            nextTask = new Queue<Task>(tasks);
            currentTask = nextTask.Dequeue();
            planStatus = PlanStatus.InProgress;
        }

        public PlanStatus Status
        {
            get { return planStatus; }
        }

        public void PlanIterate()
        {
            switch (currentTask.Status)
            {
                case Task.TaskStatus.Planned:
                    currentTask.StartExecution();
                    break;
                case Task.TaskStatus.InProgress:
                    break;
                case Task.TaskStatus.Complete:
                    if (nextTask.Count > 0)
                    {
                        currentTask = nextTask.Dequeue();
                    }
                    else
                    {
                        planStatus = PlanStatus.Complete;
                    }

                    break;
                case Task.TaskStatus.Failure:
                    planStatus = PlanStatus.Failure;
                    break;
                case Task.TaskStatus.None:
                    throw new NoneTaskStatusException();
            }
        }

        public static void TransferControlToTask(Plan p, Task t)
        {
            p.rootTask = t;
        }

        public static bool PlanSatusNotFailure(Plan p)
        {
            return p.Status != PlanStatus.Failure;
        }

    }
}