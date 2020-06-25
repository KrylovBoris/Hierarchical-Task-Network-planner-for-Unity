using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchicalTaskNetwork;

public class HtnPlanner : MonoBehaviour
{
    [Serializable]
    private struct AgentAndTask
    {
        public Agent agent;
        public string designatedTask;
    }

    [SerializeField]
    private AgentAndTask[] agentsAndTheirTasks;
    private Dictionary<uint, Task> _idToTask;
    
    void Awake()
    {
        _idToTask = new Dictionary<uint, Task>();
    }


    void Start()
    {
        SetAgents();
    }
    
    private void SetAgents()
    {
        for(int i = 0; i < agentsAndTheirTasks.Length; i++)
        {
            var agent = agentsAndTheirTasks[i].agent;
            AssignTask(agent, agent.GetTaskByName(agentsAndTheirTasks[i].designatedTask));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //If the task is complete or failed, restart the planning process
        foreach (var t in _idToTask.Values)
        {
            if (t.Status != Task.TaskStatus.InProgress )
                t.StartExecution();
        }
    }

    public void AssignTask(Agent a, Task task)
    {
        if (_idToTask.ContainsKey(a.Id))
        {
            _idToTask[a.Id] = task;
        }
        else
        {
            _idToTask.Add(a.Id, task);
        }
    }
    
    
    public void AgentDied(uint agent)
    {
        _idToTask.Remove(agent);
        Debug.Log("Agent" + agent + " has died and removed.");
    }
       

}
