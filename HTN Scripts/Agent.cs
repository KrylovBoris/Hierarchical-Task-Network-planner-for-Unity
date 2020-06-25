using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalTaskNetwork 
{
    public abstract class Agent : MonoBehaviour
    {
        public static uint agentIdGenerator = 0;
        private uint _id;
        public uint Id => _id;
        
        public delegate Task TaskCreation();
        protected Dictionary<string, TaskCreation> agentTaskDictionary;
        
        public Task GetTaskByName(string taskName)
        {
            if (agentTaskDictionary.ContainsKey(taskName))
            {
                return agentTaskDictionary[taskName].Invoke();
            }
            
            throw new UnknownTaskNameException(_id, taskName); 
        }

        //USE THIS METHOD TO SET YOUR DICTIONARY FOR TASK GENERATION
        protected abstract void SetTaskDictionary();

        private void Awake()
        {
            _id = agentIdGenerator++;
            agentTaskDictionary = new Dictionary<string, TaskCreation>();
            SetTaskDictionary();
        }
    }

    public class UnknownTaskNameException : Exception
    {
        public readonly uint agentId;
        public readonly string givenTaskName;

        public UnknownTaskNameException(uint id, string taskName)
        {
            agentId = id;
            givenTaskName = taskName;
        }

        public override string Message => "Agent " + agentId + " doesn't have task \"" + givenTaskName + "\" in its domain knowledge.";
            
    }
}