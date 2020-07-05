### i. Scripts: Task.cs and CoroutineStarter.cs

#### Task.cs

This is the base class for any task within the system. 

##### Class types

Condition() — simple boolean function without parameters. Condition() is used for referencing predicate methods that are needed to interpret the actual state of the game environment. 

##### Class constructor

*taskName* — task name within the system.

*coroutineRunner* — [CuroutineStarter](https://github.com/KrylovBoris/Hierarchical-Task-Network-planner-for-Unity/blob/master/documentation/tasks.md#coroutinestartercs) class instance that will start precondition checks, task execution, integrity rules.

*preConditionsList* — delegate array of Condition() type, that is used to define preconditions. Preconditions are checked before task execution. You can pass a list of boolean methods, but note that for the task to start all of them shall be true when checked.

*integrityRules* — delegate array of Condition() type, that is used to describe task integrity rules. Integrity rules are special extension of the HTN framework that allows the designer to control the plan execution. If some condition must be remained true throughout the whole task execution. The game environment is exists and can change its state not only because of agent actions, but also because of outside factors (like player actions or other agents). Thus it may be useful to store within the array delegates that reference interruption flags or some conditions that indicate that the plan is executable while said conditions are true. If the integrity rule is broken, the plan execution must be stopped. As preconditions, you can pass multiple integrityrules, but all of them must remain true throughout task execution.

##### Class properties

*Name* — task name within the system. A task symbol that is required for debugging.

*Type* — task type, compound or primitive. 

*Status* — current status of Task class instance. It can be used to control the task execution within and outside of the plan instance:
 * None — unused task status. Doesn't  typical  Task;
 * Planned — a task is placed in a queue;
 * InProgress — a task is being executed;
 * Complete — a task successfuly finished;
 * Failure — a precondition or an integrity rule is false.

#### CoroutineStarter.cs

It is a simple Monobehaviour script that is requiered to run coroutines that is used only to start coroutines that are needed to invoke task decomposition or task execution.

[[Previous]()|[Table of contents](https://github.com/KrylovBoris/Hierarchical-Task-Network-planner-for-Unity/blob/master/documentation/_table_of_contents.md)|[Next](https://github.com/KrylovBoris/Hierarchical-Task-Network-planner-for-Unity/blob/master/documentation/compound-tasks.md)]
