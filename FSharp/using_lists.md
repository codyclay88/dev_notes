
Previously I wrote about the List data structure in F#. Primarily I wanted to get an idea about how an F# List is different from a C# list, but how it is similar to Lists that I have seen in other functional languages. With all this said, I actually had a good bit of fun researching through the Microsoft docs and actually digging a bit into the source code itself (*An aside: digging through the F# source code on GitHub is a whole lot easier that it is in C# or Java for example, due to how F# requires your code to be structured with "higher-level" files and modules listed after "lower-level" modules*). 

While that post dug a bit into how Lists work, I'd now like to dig a bit deeper on how we can use them.

A quick recap first though, we can create new lists in several ways:
```fsharp
let emptyList: int list = []
//=> val emptyList : int list = []

let anotherEmpty = List<int>.Empty
//=> val anotherEmpty : int list = []

let basic = [1;2;3;4;5]
//=> val basic : int list = [1; 2; 3; 4; 5]

let overMultipleLines = [
    1
    2
    3 
]
//=> val overMultipleLines : int list = [1; 2; 3]

let withRange = [1..5]
//=> val withRange : int list = [1; 2; 3; 4; 5]

let withRangeExtra = [1..2..10] 
//=> val withRangeExtra : int list = [1; 3; 5; 7; 9]

let withListExpression = [for i in 1..5 -> i]
//=> withListExpression : int list = [1; 2; 3; 4; 5]

let withConsOperator = 1 :: [2;3;4;5]
//=> val withConsOperator : int list = [1; 2; 3; 4; 5]

let chainingCons = 1 :: 2 :: [3;4;5] 
//=> chainingCons : int list = [1; 2; 3; 4; 5]

let extremeChaining = 1 :: 2 :: 3 :: 4 :: 5 :: []
//=> extremeChaining : int list = [1; 2; 3; 4; 5]

let staticCons = List.Cons(1, [2;3;4;5])
//=> staticCons : int list = [1; 2; 3; 4; 5]
```

Now that we now how to create lists, what can we *do* with them? 

Well, F# provides a pretty extensive module called `List` full of functions just waiting for us to dive in to. So let's begin! *cracks knuckles*
*I apologize, but this is gonna be a pretty exhaustive list, I'm gonna go through as many of the functions in this module as I can, because I want to learn this too!*




Okay, with that out of the way, let's take a small step back and try to get a bit of context. Functional languages like F# do not like to use "loops", meaning that they don't like to use the traditional "for", "while", "do..while" constructs that you'll see in other more traditional "imperative"-style languages. The main reason (that I can see) for this is because these styles of looping are provided through statements. 

PATTERN MATCHING

RECURSIVE FUNCTION CALLS

