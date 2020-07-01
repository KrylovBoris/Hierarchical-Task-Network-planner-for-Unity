### Scripts: Task.cs and CoroutineStarter.cs

#### Task.cs

This is the base class for any task within the system. 

##### Class types

Condition() — simple boolean function without parameters. Condition() is used for 

##### Class constructor

*taskName* — task name withing the system.

*coroutineRunner* — [CuroutineStarter]() class instance that will start precondition checks, task execution, integrity rules.

*preConditionsList* — delegate array of Condition() type, that is used to define preconditions. Preconditions are checked before task execution.

*integrityRules* — delegate array of Condition() type, that is used to describe task integrity rules. Integrity rules are special extension of the HTN framework that allows the designer to control the plan execution. If some condition must be remained true throughout the whole task execution. The game    должны быть истинными на протяжении всего времени выполнения задачи. Они необходимы, чтобы контролировать корректность выполнения плана в изменчивой среде Unity. Поскольку среда игровой сцены существует и меняет состояния не только из-за действий агента, но и из-за внешних факторов некоторые условия нам необходимо проверять, пока выполняется задача, и прерывать выполнение плана в случае, если какое-то правило целостности нарушено.

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
