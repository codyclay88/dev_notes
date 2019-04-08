# Exploring F# Lists

#### This post is an attempt to learn more about how F# Lists work *under the hood* not necessarily how to use Lists in F#. A later post will go deeper into applications of F# Lists and the List module. 

Lists in F# are `ordered`, `immutable`, `singly-linked`, and made up of elements of the same type.  

This means that, like most values in F#, they cannot be modified after they have been created (due to their `immutable` nature). Also, because of their `singly-linked` nature, lists should be used primarily as containers of values for the purpose of enumeration.

The "standard" way of creating a list is like so: 
```fsharp
let listOfInts = [1;2;3;4;5]

// OR

let listOfInts = [
    1
    2
    3
    4
    5
] // can be more readable when creating lists of bigger types
```
In the simple case shown above, for creating a sequential list of integers, you could also create the list using the range operator `(..)`. 
```fsharp
let myList = [1..5]
//=> val myList : int list = [1;2;3;4;5]
```

You can also create a list with a sequence expression:
```fsharp
let myList = [for i in 1 .. 5 -> i ]
//=> val myList : int list = [1;2;3;4;5]
```

If you look at the type definition for the List type in [Microsoft's documentation](https://msdn.microsoft.com/visualfsharpdocs/conceptual/collections.list%5b%27t%5d-union-%5bfsharp%5d#syntax), you will find this:
```fsharp
[<DefaultAugmentation(false)>]
[<StructuralEquality>]
[<StructuralComparison>]
type List<'T> =
| ( [] )
| ( :: ) of 'T * 'T list
with
interface IStructuralEquatable
interface IComparable
interface IComparable
interface IStructuralComparable
interface IEnumerable
interface IEnumerable
static member List.Cons : 'T * 'T list -> 'T list
static member List.Empty :  'T list
member this.Head :  'T
member this.IsEmpty :  bool
member this.Item (int) :  'T
member this.Length :  int
member this.Tail :  'T list
end
```
As a beginner in F# myself, this looks a little confusing to me, so let's try and break it down a bit. 
We'll start at what I believe to be the most obvious parts, the member declarations:
```fsharp
...
static member List.Cons : 'T * 'T list -> 'T list
static member List.Empty :  'T list
member this.Head :  'T
member this.IsEmpty :  bool
member this.Item (int) :  'T
member this.Length :  int
member this.Tail :  'T list
...
```
What this tells us is that the List type gives us multiple `members` that we can use in a similar way to properties that you may have used in C#, via `.` notation. 

Using the `myList` example from above, we can try and use these members (we'll start with the instance members):
```fsharp
// Lets start with the "instance" memb  ers first

let listLength = myList.Length
//=> val listLength : int = 5

let isListEmpty = myList.IsEmpty
//=> val isListEmpty : bool = false

let secondItem = myList.Item(1)
//=> val secondItem : int = 2

let head = myList.Head
//=> val head : int = 1

let tail = myList.Tail
//=> val tail : int list = [2;3;4;5]
```

For the most part, these are pretty standard List operations that you'll find in other languages. You can get the length of a List with the `Length` member, check if its empty with the `IsEmpty` member, and get the element at a particular index with the `Item` member. 

I have an interesting feeling though that we should go a bit deeper in to the `Head` and `Tail` members. In all of the other functional languages that I have checked out, they all have some way of manipulating the head of a list (the first element) and the tail (the original list minus the first element). Let's make a mental note to return to this later. 

Now lets take a peek at the following portion of the declaration:
```fsharp
type List<'T> =
| ( [] )
| ( :: ) of 'T * 'T list
```
This tells us that List is a generic type, meaning that it can work with any type, that it is also a `discriminated union` type, meaning that, in this case, the value is either one of two options; `[]` or `(::) of 'T of * 'T list`. 

Woah. Lets take a step back. 

Okay, at face value, the first case, `[]` seems pretty simple, a List may be empty. 

The other case seems a bit more complicated. Looking at [this](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/lists#operators-for-working-with-lists) document from Microsoft, I see that you can attach elements to a list with the `cons` operator, which is this guy: `::`. This operation would look like the following:
```fsharp
let myOtherList = 0 :: myList // myList was created in previous examples
//=> val myOtherList : int list = [0;1;2;3;4;5]
```
So it would appear that the `::` (cons) operator allows you create a new list (because lists are immutable) from a single element and an existing list (made up of elements of the same type as the aforementioned element), where the first argument is the head of the new list, and the second argument (which was the already existing list) is the tail.

Notice the `Head` and `Tail` terminology again. Does this have anything to do with the second union case,`(::) of 'T * 'T list`? This leads me to believe that a non-empty List is actually a tuple (I'm guessing this from the `*` symbol, which is used to construct tuples) that consists of a `Head` (`'T`) and a `Tail` (`'T list`), to give us `'T * 'T list`, which is combined using the `::` (cons) operator to give us our full list. 

This is pretty cool and actually makes a lot of sense. The `Head` and `Tail` members of the `List` type are really only accessing the first and second members of the `'T * 'T list` tuple, respectively, that make up the second case of the type. 

This takes us back to the `static member List.Cons : 'T * 'T list -> 'T list` portion of the type definition, which we can use like so:
```fsharp
let consList = List.Cons (1, [2;3;4;5]) // (1, [2;3;4;5]) is a tuple
//=> val consList : int list = [1;2;3;4;5]
```
Which, as we discussed before, takes a tuple of type `int * int list` and turns it into `int list` (note that `int` is interchangeable with any type because List is generic). 

If we look at the other static member, `static member List.Empty :  'T list`, it can be used like so:
```fsharp
let emptyList: int list = List.Empty
//OR
let emptyList = List<int>.Empty

//=> val emptyList : int list = []
```
I messed around with this for a few seconds before finally getting it right. Because List is generic and you are creating something that is empty, the compiler has no way of inferring the generic type parameter, so you must specify the type, either by providing the type of the value (as in the first case above) or by providing a generic type argument (as in the second case).

Now we can take a look at the interface declarations from the type definition:
```fsharp
...
interface IStructuralEquatable
interface IComparable
interface IComparable
interface IStructuralComparable
interface IEnumerable
interface IEnumerable
...
```
Because F# is a .NET language, F# types implement various .NET interfaces so that the types can interoperate with the greater .NET ecosystem. These declarations tell us that the List type implements these commonly used .NET interfaces, telling us that an `FSharpList` (as they are called when using an F# list from another .NET language) can be used any where that you would use an IEnumerable, for example.

**NOTE**: I'm not quite sure why `IEnumerable` and `IComparable` are listed twice, my best guess is that there is a `System.Collections.Generic.IEnumerable<'T>` as well as `System.Collections.IEnumerable`, and that the documentation that I got this from didn't differentiate. 

Finally, let's take a look at the attributes in the type definition:
```fsharp
[<DefaultAugmentation(false)>]
[<StructuralEquality>]
[<StructuralComparison>]
...
```
Let's first look at the `[<DefaultAugmentation(false)>]` attribute. Reading [this documentation from Microsoft](https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/core.defaultaugmentationattribute-class-%5Bfsharp%5D) tells me that "adding this attribute to a discriminated union with the value **false** turns off the generation of standard helper member tester, constructor, and accessor members for the generated Common Language Infrastructure (CLI) class for that type." Hmm.. I guess that in F# there is some code generation the F# compiler will provide for discriminated unions, unless you specify this attribute with a value of false. This makes sense because when declaring a list value, you can use the following syntax:
```fsharp
let myList: int list = [1;2;3;4;5] 
```
rather than using a constructor provided by the discriminated union. In fact, it seems like there are no constructors provided by the discriminated union, meaning you have to use other means of creating an instance of this type, namely using the `[e1;e2]` syntax. Which makes sense after reading the Microsoft documentation for that attribute above.

*This is my interpretation, if this is incorrect or not complete, please let me know so I can update this post*

Lets look at the next two, `[<StructuralEquality>]` and `[<StructuralComparison>]`. Before even diving into these, I know that most functional programming languages prefer to use structural equality rather than referential equality. Meaning that in F#, two values are considered to be the same if the inner values that make up the two values are the same (this is not always the case, some types prefer different means of equality). This is in contrast to C# for example, where equality is determined by whether the two variables occupy the same address in memory (of course you can override the default behavior in C# and F#, but the defaults exist nonetheless).  

According to [this](https://blogs.msdn.microsoft.com/dsyme/2009/11/08/equality-and-comparison-constraints-in-f/) post by Don Syme (the "Colonel Sanders" of F#) these attributes were added in F# 1.9.7 as a means to make equality and comparison constraints more explicit in the language. Long story short, using these attributes makes it very clear that this type uses structural equality and comparison. 

And that is about all I can think to cover here, this article will surely be updated in the future as I continue to learn more about F#. In the future I will be writing more posts about how to use Lists and some of the common operations that are defined in the List module. 

#### If you are reading this and have found some horribly obvious wrong statements, please email me so I can update the article and help out other people that are new to F#. Also, please try and be nice about it. 
