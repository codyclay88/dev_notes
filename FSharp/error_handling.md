Three types of errors:
- Domain errors: errors that are expected as part of the business process and must be included in the design of the system. 
- Panics: errors that leave the system in an unknown state, such as unhandleable system errors (such as "out of memory") or errors caused by programmer oversight (divide by zero or null reference)
- Infrastructure Errors: errors that are to be expected as part of the architecture but are not part of any business process and are not included in the domain, such as network timeout or an authentication failure

Domain errors are part of the domain, like anything else, and so should be incorporated in our domain modelling, discussed with domain experts, and documented in the type system, if possible.

Panics are best handled by abandoning the workflow and raising an exception that is then caught at the highest appropriate level (such as the main function of the application or equivalent). 

Infrastructure Errors can be handled using either of the above approaches. The exact choice depends on the architecture you're using. If the code consists of very small services, then exceptions might be cleaner, but if you have a more monolithic app, you might want to make the error handling more explicit. 

### Modeling domain errors with types
We will generally model errors as a choice type, with a separate case for each kind of error that needs special attention. 
```fsharp
type PlaceOrderError =
| ValidationError of string
| ProductOutOfStock of ProductCode
| RemoteServiceError of RemoteServiceError
```
- The `ValidationError` case would be used for the validation of properties, such as any length or format errors
- The `ProductOutOfStock` case would be used when the customer attempts to buy an out-of-stock product. There might be a special business process to handle this. 
- The `RemoteServerError` case is an example of how to handle an infrastructure error. Rather than just throwing an exception, we could handle this case by, say, retrying a certain number of times before giving up

One nice thing about exceptions is that they keep your "happy path" code clean. If we return errors from each step, then the code gets much uglier. We would typically have conditionals after each potential error, as well as try/catch blocks to trap potential exceptions. 

The problem with this approach is that two-thirds of the code is now devoted to error handling -- our original simple and clean code has now been ruined. We have a challenge: how can we introduce proper error handling while preserving the elegance of the pipeline model?

### Chaining Result Generating Functions
In general, we have some `Result` generating functions, how can we compose them together in a clean way?

Here's a visual representation of the problem. A normal function can be visualized as a piece of railroad track:
```
===============
```
But a function with a `Result` output can be visualized as a railroad track that splits into two:
```
=============    // Success
      \
       ======    // Failure
```
We can call these kinds of functions "switch" functions, after the railroad analogy. Ther're often called "monadic" functions as well. 

So how do we connect two of these "switch" functions? If the output is successful, we want to go on to the next function in the series, but if the output is successful, we want to go on to the next function in the series, but if the output is an error, we want to bypass it:
```
=============    -- Success ->   =============
      \                                \
       ======    -- Fail (bypass) ->    ======
```
How do we combine these two switches so that both failure tracks are connected?
```
==============================================
      \                                \
       =======================================
```
And if you connect all the steps in the pipeline in this way, you get the "two-track" model of error-handling, or "railway-oriented-programming", which looks like this:
```
===================================================
      \        \       \        \        \
       ============================================
```
In this approach, the top track is the happy path, and the bottom path is the failure path. You start off on the success track, and if you're lucky you stay on it to the end. But if there is an error, you get shunted onto the failure track and bypass the rest of the steps in the pipeline. 

While this looks great there is a big problem: we can't compose these kinds of result-generating functions together, because the type of the two-track output is not the same as the type of the one track input:
```
=============    XX   =============
      \          XX         \
       ======    XX          ======
```
How do we solve this issue? How can we connect a two track output to a one-track input? Well, let's observe that if the second function had a two-track input, then there would be no problem connecting them. So we need to convert a "switch" function, with one input and two outputs, into a two-track function. 

To do this, let's create a special "adapter-block" that has a slot for a switch function and which converts it into a two-track function. If we then convert all our steps into two-track functions, we can compose them together nicely after they have been converted. The final result is a two-track pipeline, with a "success" track and a "failure" track, just as we want. 

### Implementing the adapter blocks
The adapter that converts switch functions to two-track functions is a very important one in the functional programming toolkit -- its commonly called `bind` or `flatMap` in FP terminology. It's surprisingly easy to implement. 
- The input is a "switch" function. The output is a new two-track-only function, represented as a lambda that has a two-track input and a two-track output. 
- If the two-track input is a success, the pass that input to the switch function. The output of the switch function is a two-track value, so we don't need to do anything further with it.
- If the two-track input is a failure, then bypass the switch function and return the value.

```fsharp
let bind switchFn =
    fun twoTrackInput -> 
        match twoTrackInput with
        | Ok success -> switchFn success
        | Error failure -> Error failure
```
An equivalent but more common implementation is to have two input parameters to `bind`-- the switch function and a two-track value (a `Result`) -- and to eliminate the lambda, like this:
```fsharp
let bind switchFn twoTrackInput = 
    match twoTrackInput with
    | Ok success -> switchFn success
    | Error failure -> Error failure
```
Both implementations are equivalent: the second implementation, when curried, is the same as the first. 

Another useful adapter block is one that converts single-track functions into two-track functions. This is commonly called `map` in FP terminology. 
- The input is a one-track function and a two-track value (a `Result`).
- If the input `Result` is a success, then pass that input to the one-track function and wrap the output in `Ok` to make it into a `Result` again (because the output needs to be two-track)
- If the input `Result` is a failure, then bypass the function, as before.
```fsharp
let map f aResult =
    match aResult with
    | Ok success -> Ok (f success)
    | Error failure -> Error failure
```
With `bind`, `map`, and a few other similar functions, we'll have a powerful toolkit that we can use to compose all sorts of mismatched functions. 

### Converting to a common error type
Unlike the success track, where the type can change at each step, the error track has the same uniform type all the way along the track. That is, every function in the pipeline must have the *same error type*. 

In many cases, that means we'll need to tweak the error types to make them compatible with each other. To do that, let's create a function that is similar to `map` but which acts on the value in the *failure* track. That function is called `mapError` and would be implemented like this:
```fsharp
let mapError f aResult =
    match aResult with
    | Ok success -> Ok success
    | Error failure -> Error (f failure)
```
For example, let's say that we have an `AppleError` and a `BananaError` and we have two functions that use them as their error types.