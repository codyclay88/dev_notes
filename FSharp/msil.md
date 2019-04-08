MSIL stands for Microsoft Intermediate Language. 

MSIL code is the set of instructions that are generated when our source code is compiled. 

.NET programming languages do not compile directly to executable machine code; instead they compile to an intermediate format called MSIL. This MSIL code is then sent to the CLR (Common Language Runtime) that converts the code to machine language. 

At runtime, the CLR's JIT (Just In Time) compiler will convert the IL code into native code to the host's Operating System.

MSIL provides language interoperability as code in any .net language is compiled on MSIL. 
Code in F# will have the same performance as equivalent code written in C#.
Provides support for different runtime environments
JIT compiler in CLR converts MSIL code into native machine code, which is then executed by the OS.  

### How does it work?
All operations in MSIL are executed on the Stack. When a function is called, its parameters and local variables are allocated on the stack. Function code starting from this stack state may push some values onto the stack, makes operations with these values, and pop values from the stack. 

Execution of both MSIL commands and functions is done in three steps: 
1. Push command operands or function parameters onto the stack
2. Execute the MSIL command or call function. The command or function pops their operands (parameters) from the stack and pushes onto the stack result (return value).
3. Read result from the stack

Steps 1 and 3 are optional. For example, the void function doesn't push a return value onto the stack. 

The stack contains objects of value types and references to objects of reference type. Reference type objects are kept in the heap. 

MSIL commands used to push values onto the stack are called `ld ...` (load). Commands used to pop values from the stack are called `st ...` (store), because values are stored in variables. Therefore, we will call the push operation loading and the pop operation storing.  