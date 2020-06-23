using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace HierarchicalTaskNetwork
{
    public abstract class Task
    {
        private string taskName;

        private TaskStatus taskStatus;

        public static readonly Condition[] EmptyCondition =
        {
            () => true
        };


        public delegate bool Condition();

        protected readonly Condition[] preConditionsList;
        protected readonly Condition[] integrityRules;
        protected readonly CoroutineStarter taskCouroutineStarter;

        public Task(string name,
            CoroutineStarter coroutineRunner,
            Condition[] conditions = null,
            Condition[] rules = null)
        {
            taskName = name;
            taskCouroutineStarter = coroutineRunner;
            if (conditions == null)
            {
                preConditionsList = EmptyCondition;
            }
            else
            {
                preConditionsList = new Condition[conditions.Length];
                conditions.CopyTo(preConditionsList, 0);
            }

            if (rules == null)
            {
                integrityRules = EmptyCondition;
            }
            else
            {
                integrityRules = new Condition[rules.Length];
                rules.CopyTo(integrityRules, 0);
            }

            taskStatus = TaskStatus.Planned;
        }

        public enum TaskType
        {
            Empty,
            Simple,
            Complex
        }

        public enum TaskStatus
        {
            None,
            Planned,
            InProgress,
            Complete,
            Failure
        }

        public string Name
        {
            get { return taskName; }
        }

        public virtual TaskType Type
        {
            get { return TaskType.Empty; }
        }

        public TaskStatus Status
        {
            get { return taskStatus; }
        }


        protected void SetStatus(TaskStatus val)
        {
            taskStatus = val;
        }


        #region Checks

        protected bool BasicCheck(Condition[] collection)
        {
            var result = true;
            try
            {
                foreach (var condition in collection)
                {

                    UnityEngine.Debug.Assert(condition != null);
                    result &= condition.Invoke();
                    if (!result) return false;


                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }


        protected bool CheckPreConditions()
        {
            return BasicCheck(preConditionsList);
        }

        protected bool CheckTaskIntegrity()
        {
            return BasicCheck(integrityRules);
        }

        #endregion


        protected virtual IEnumerator Execution()
        {
            return null;
        }

        public virtual void StartExecution()
        {
            taskCouroutineStarter.StartRunningCoroutine(Execution());
        }

        public virtual Task[] DecomposeTask()
        {
            return null;
        }

    }
}