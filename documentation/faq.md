## 4. Frequently Asked Questions

### Is this really an HTN planner?
Yes. It is by definition. The only major diversions are integrity rules and the usage of generative function instead of a set of decomposition methods. Integrity rules are just an extension of the classic HTN method that allows task execution interruption. Generative functions map states to the proper task decompositions, while preconditions in decomposition methods are used to check applicability of the decomposition to a state. One generative function can describe multiple decomposition methods. The difference in functionality is that we don't search a proper decomposition, but generate one for a given state. It is faster performance-wise and easier to code, as you don't need to define same sequences multiple times for several different decomposition methods of one compound task.

### Can I use it?
Of course! However, I strongly encourage you to wait until more user-friendly solution is implemented. The solution presented here is perfectly functional and I'm using it myself for my projects.
However, its usability and convinience of use is less than desirable right now.

### Is this project finished?
Yes, but not really. We are planning a lot of features in future including some quality of life extension, so you can use it in your projects without dealing with a lot of unnecessary heasache. Here I present to you the bare bones of the project on top of which I plan to build a complete system.

### My agents stop working for some reason. Why?
Our system depends on Coroutines. All coroutines in Unity are attached to some GameObject. 
If said GamObject is disabled, all coroutines, attached to it are stopped. The best way to avoid this is to reference agent gameobject upon task construction. 
You don't want your agent disabled while it is performing an assigned task anyway.

### Why does your F.A.Q. section has so few questions?
This project is currently under development, so this section will be expanded in future.
