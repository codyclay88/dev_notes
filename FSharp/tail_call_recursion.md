In many languages, calls to recursive functions are treated like any other function call, and require the allocation of an additional stack frame for each call. This means that functions which make many recursive calls may cause a stack overflow. As a functional programming language, F# encourages using recursion for some tasks, so it's important for the language to provide a mechanism to avoid overflowing the stack when using recursive functions. Tail calls are important because they can be invoked without extending the call stack, and therefore recursive algorithms which use tail calls can be called without worrying about causing a stack overflow at runtime. In many cases, it is possible for the compiler to turn tail recursion into loops, resulting in performance equivalent to imperative code. 

## What exactly is a tail call?
A tail call is a call within a function whose result is immediately used as the output of the function (although this is phrased in terms of functions, in F# the same rules apply to .NET methods, too). That is, a tail call is a call which is in *tail* position, which is defined recursively as follows:
1. The Body of a function or method
   ```fsharp
   // Expression e is in tail position
   fun() -> e
   ```
2. The body on an action in a match expression, where the match expression is in tail position
    ```fsharp
    // Expressions e and f are in tail position
    function 
    | 1 -> e
    | _ -> f
    ```
3. The body of an if, elif, or else branch, where the conditional expression in is tail position
   ```fsharp
   // e and f are both in tail position
   fun() -> 
       if true then e
       else f
   ```
4. The last expression in a sequence, where the sequence is in tail position
   ```fsharp
   // e is in tail call position
   fun() -> 
       printfn "First statement in a sequence"
       printfn "Second statement in a sequence"
       e
   ```
5. The body of a let or let rec binding, where the binding expression is in tail position. 
   ```fsharp
   fun() -> 
       let x = 1
       e
   ```

One example of a common mistake which results in a non-tail call is the following:
```fsharp
let rec sum = function
| [] -> 0
| x :: xs -> x + sum xs
```
In this code, the recursive call to sum is not a tail call. Although this call appears at the end of the function in the *text* of the source code, it is not in tail position since the result of the call is used by the (+) operator.

## How can I verify that tail calls are being used?
The easiest way is to ensure that tail calls are being used is to understand and apply the rules from the previous section. While the F# compiler itself doesn't currently provide any way to verify that tail calls have been used at a particular call site, you can be sure by looking at the compiled version of the code using an MSIL Disassembler. 

## How are tail calls compiled?
When the compiler comes across a tail call, there are a few different forms that the resulting code may take. The next few sections will go into all the gory details, but here's a quick summary. F# will use a .NET tail call when compiling a call which is in tail position unless any of the following is true:
- You turn off the --tailcalls compiler option (this is the default behavior in Visual Studio for Debug builds, to enable a better debugging experience)
- The compiler can determin that no recursive paths through the call site exist, in which case it may emit a normal call for efficiency reasons. 
- The compiler can use gotos instead of recursive calls
- The call is to a first class function value returning init
- The call takes place in a try-catch or try-finally block (note that these calls aren't really in tail position, by definition)