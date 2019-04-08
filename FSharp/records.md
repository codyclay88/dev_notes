
The `Record` type is one of several F# types that make up its `Algebraic Type System`. 

Before we dig too much into `records`, let's analyze this `algebraic type system` thing I just mentioned. An `algebraic data type`, is a type that is made up of other types. More specifically, there are two operations that we can perform when constructing types "algebraically": `sum` and `product`. (As always, [Scott Wlaschin covers this much more in depth then I will here](https://fsharpforfunandprofit.com/posts/overview-of-types-in-fsharp/#sum-and-product-types)). 

`Record` types operate on the `product` side of the equation (see what I did there). This can be thought of as "ANDing" types together. 

Let's go ahead and create a `FullName` `record` type in F# like so:
```fsharp
type FullName = { FirstName: string; MiddleName: string; LastName: string }
// Alternatively we could break the definition to multiple lines
type FullName = {
    FirstName: string  // notice we don't need semicolons this way
    MiddleName: string
    LastName: string
}
```
If we were to write some pseudocode for this, it might look like the following:
```fsharp
type FullName = FirstName AND MiddleName AND LastName
```
`Record` types are similar to `Tuples`, but the fields of record types have *labels*, which can be very useful when dealing with types that have very similar structures, and is also better for "self-documenting" exactly what a value represents in your code.

A peek at the [Microsoft docs for Records](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/records) gives us this lovely grammar (is that the correct term?)
```fsharp
[ attributes ]
type [accessibility-modifier] typename =
    { [ mutable ] label1 : type1;
      [ mutable ] label2 : type2;
      ... }
    [ member-list ]
```
Let's break this down a bit:
- `attributes` is an optional list of, well, attributes to apply to the type. For example, you could use the `[<Struct>]` attribute to designate the type as a `struct record` instead of a `reference record`, which is the default.
- `typename` is the name of the record type (derp!) and can be any valid identifier
- `accessibility-modifier` can be any of the usual `public`, `private`, `internal` modifiers that declare the scope of where the type can be used
- In between the curly braces we can supply a list of `label` and `type` groupings, which represent the values that make up the type, as we have seen above in the `FullName` type. You can optionally specify one or more of the values as `mutable` (more on this later).
- `member-list` is an optional list of `members` for our type (more on this later as well).    

So with this in mind, lets revisit our `FullName` `record` by creating some instances of it:
```fsharp
let myName = {FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay";}
//=> val myName : FullName = {FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay";}

let myLovingWifesName: FullName = {
    FullName.FirstName = "Hayley"
    FullName.MiddleName = "Belle"
    FullName.LastName = "Clay"
}
//=> val myLovingWifesName : FullName = {FirstName = "Hayley"; MiddleName = "Belle"; LastName = "Clay";}
``` 
What we see above are `record expressions`, which is a way to initialize records by using only the labels that are defined in the record. Notice that the syntax is very similar to how we created the record type itself in the first place. Also, note in the first case that I didn't have to specify what the type of the value was, the compiler correctly infers that for me that it is a `FullName` based on the labels that we used, which is really awesome. The second example shows how you can explicity define the type. 

This whole idea of the compiler automatically inferring the types got me thinking though, what would the compiler do if I have one type that has the exact labels as another. This led me to an experiment:
```fsharp
type FullName = { 
    FirstName: string 
    MiddleName: string
    LastName: string
}
// these types have the exact same structure; labels and types
type OtherFullName = { 
    FirstName: string 
    MiddleName: string
    LastName: string
}
```
I typed the `OtherFullName` type in, and IntelliSense surprisingly didn't yell at me. So I highlighted these records and ran them through F# interactive, and still no kicking and screaming! I got the following type definitions:
```fsharp
type FullName =
  {FirstName: string;
   MiddleName: string;
   LastName: string;}
type OtherFullName =
  {FirstName: string;
   MiddleName: string;
   LastName: string;}
```
So I thought, "Ha, well what if I try and create an instance of *something* that has these three values, surely that'll get a rise out of ye olde compiler". Below shows the results of my experimentation:
```fsharp
let me = {FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"; }
//=> val me : OtherFullName = {FirstName = "Kristian";
//                          MiddleName = "Cody";
//                          LastName = "Clay";}
```
What? No fuss at all? As we can see, the compiler inferred that my value was of `OtherFullName`, which leads me to believe that the compiler will simply choose the definition that is provided last. I can test this by flip-flopping the order of the definitions and running this same experiment again, which gave me this:
```fsharp
let me = { FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"; };;
//=> val me : FullName = {FirstName = "Kristian";
//                     MiddleName = "Cody";
//                     LastName = "Clay";}
```
Which confirms my suspicions! This is very cool, the compiler is just like, "Ah, whatever, this'll work". But it is probably more like, "Hmm.. This value could be of two different types. I won't make the programmer's life any more difficult. I'll just use the most recent type that was defined that matches these labels. I'm awesome like that".

Okay, I spent way too much time on that. Let's move on. 

So we learned that we can create an instance of a `record` via `record expressions`. Very cool. One thing we need to remember about F#, and functional languages in general, and a lot of very smart developers, is they they all prefer `immutability`. Meaning that once a value has been created, it cannot be modified. Notice the term `value` here rather than `variable`, this is deliberate in the design of languages like F# to more directly state that once "something" is created, it cannot be modified.

`Immutability` is one of those concepts that should be pretty simple but it's also pretty complicated. I won't dig too much into the pros and cons of `immutability` here, but try to understand that defaulting to `immutable data structures` means that you get a lot of guarantees about the *preservation* of your values throughout the lifecycle of your application. Meaning that when you do this:
```fsharp
let me = {FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"; }
```
You can be sure that no other thread of execution is able to modify those values. That reference to `me` will always contain the same values as when you created it. 

With this in mind though, let's say that I decide that I want to change my legal name to be "Cody Kristian Clay" instead of "Kristian Cody Clay" (because people refer to me as Cody, and Kristian is too close to Kristin and Kirsten and a bunch of girl names). So how do we do that if we already have an instance of `me` but we cannot change it? 

In times like this, we can just create a new instance in place of the original one. We could do this like so:
```fsharp
// The "OG"
let me = { FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"; }
//The new chief, flip-flop the first and middle names
let newMe = { FirstName = me.MiddleName; MiddleName = me.FirstName; LastName = me.LastName }
```
This works, but its quite a bit of typing, and we didn't actually change the `LastName` field. You can imagine with bigger data structures where we are only changing the value of one of its fields that this would get a bit annoying. 

Luckily, F# provides a nice syntax to use when we are creating one record instance from another one:
```fsharp
let me = { FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"; }

// showing off our new "with" syntax
let newMe = { me with FirstName = me.MiddleName; MiddleName = me.FirstName }
//=> val newMe : FullName = {FirstName = "Cody";
//                         MiddleName = "Kristian";
//                         LastName = "Clay";}
```
Works like a dang charm. As you can see, we no longer have to explicity say what the `LastName` field will be, and I love the syntax of saying `"original with [values]"`, which I think reads very well. According to the [Microsoft docs](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/records#creating-records-by-using-record-expressions), this is actually called a *copy and paste expression*. Cool beans!

While the fields within a `record` are `immutable` by default, you can override this by using the `mutable` keyword in front of one or more of the fields. 

So let's create a new type called `SexistFullName`, that *could* be used as a representation of a woman's name:
```fsharp
type SexistFullName = { 
    FirstName: string 
    MiddleName: string
    mutable LastName: string // because women generally change their name when they get married, right? Ugh, I'm gonna regret using this example.
}
``` 
While this example is obviously a bit dumb (even technically, because in a perfect world women only marry once and it wouldn't be very expensive to create only a single extra instance of a woman's name), I think it gets the point across. Marking the `LastName` field as `mutable` allows us to change it later without the compiler barking at us. 

So let's represent my wife's name before we were married:
```fsharp
let myBetrothed = {FirstName = "Hayley"; MiddleName = "Belle"; LastName = "Parsons" }
//=> val myBetrothed : SexistFullName = {FirstName = "Hayley";
//                                     MiddleName = "Belle";
//                                     LastName = "Parsons";}
```
Now let's show how we can change her `LastName` after we "wed":
```fsharp
myBetrothed.LastName <- "Clay";;
//=> val it : unit = ()
```
Notice that we aren't using the `=` operator to reassign this value, we use the `<-` operator instead. The idea that the F# language provides an operator exclusively for the purpose of reassigning values tells me that what we are doing here is kind of a special case. From what I've read, I think it's generally agreed to keep values `immutable` as often as you can, unless you really need `mutability`. 

I feel like it's also kind of important to note that this operation did not return the updated `SexistFullName` representation, it actually returned `val it : unit = ()`, meaning that it returned nothing, which makes it feel like a side-effect. We can prove that it worked however by now typing the name into F# Interactive:
```fsharp
myBetrothed;;
//=> val it : SexistFullName = {FirstName = "Hayley";
//                           MiddleName = "Belle";
//                           LastName = "Clay";}
```

Okay, mutability is bad, but we can do it if we *need* to. Let's move on. 

The point of this whole `algebraic type system` thing is that you can combine existing types in numerous ways to get an infinite (maybe not "infinite" but a whole lot) number of new types. Let's say that we wanted to create a `Person` record type that contains a `FullName` value and (notice the AND) the age of the `Person`. 

```fsharp
type Person = {
    Name: FullName
    Age: int
}

type FullName = { 
    FirstName: string 
    MiddleName: string
    LastName: string
}
// This won't work, erase this from your memory
```
As the comment says, this won't work, and it's because the ordering of your type definitions is important to the F# compiler (as we saw a glimpse of previously with the `FullName` and `OtherFullName` record definitions).

Because `Person` depends on `FullName`, `FullName` must be defined before `Person`. Alternatively, we could combine these types as "Mutually Recursive Records" ([here](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/records#creating-mutually-recursive-records)) using the "and" keyword (again, notice the AND). 
```fsharp
type FullName = { 
    FirstName: string 
    MiddleName: string
    LastName: string
}

type Person = {
    Name: FullName
    Age: int
}

// OR defining them as Mutually Recursive Records:
type Person = {
    Name: FullName
    Age: int
} and FullName = { 
    FirstName: string 
    MiddleName: string
    LastName: string
}
```
Both ways will work, I think it's more of a "beauty is in the eye of the beholder" thing. Using the "and" keyword is probably more idiomatic and more explicit, but I feel like it more or less binds them together for eternity (which is NOT a bad thing in the right context), and it might feel awkward in this case to use `FullName` somewhere else. Again, it all depends.

We now have a grasp on how you can define record types, how to create instances of a record type, and how we can combine record types to make more record types (in sickness and in health). Let's move on to how we can interact with records out in the wild. 

Functional languages really love `pattern matching`, and F# is (thankfully) no exception. You can do A LOT with pattern matching, and I would like to do a separate post about all the wonderous ways we can use it, but right now I will focus only on pattern matching in the context of record types. 

If we think about records, they are really just containers for data, where each piece of data has a specific label. So if we think about the patterns that could arise from a group of records, we think about the labels that exist within the type and the values that the labels refer to (and probably the types of the values that the labels refer to, but I'm not gonna go there). 

So let's say we have a list of `FullName` values, and we want to process these names by printing something to the screen. Let's see how pattern matchin can help us here:
```fsharp
let me = {FirstName = "Kristian"; MiddleName = "Cody"; LastName = "Clay"}
let random1 = {FirstName = "Abraham"; MiddleName = "Jenkins"; LastName = "Lincoln"}
let random2 = {FirstName = "George"; MiddleName = "Freakin"; LastName = "Washington"}
let hayley = {FirstName = "Hayley"; MiddleName = "Belle"; LastName = "Clay"}
let oleSteve = {FirstName = "Stephen"; MiddleName = "Frederick"; LastName = "Parsons"}
let names = [me; random1; random2; hayley; oleSteve]

let isFam name =
         match name with
         | { MiddleName = "Cody"; LastName = "Clay" } -> printfn "We got us a try-hard!"
         | { LastName = "Clay" } -> printfn "Gee wilikers we got a Clay!"
         | { LastName = "Parsons" } -> printfn "Holy crap, its a Parsons!"
         | { FirstName = "Abraham" } -> printfn "President or Father of Judaism??"
         | _ -> printfn "Bah, Humbug!" 

List.iter isFam names
//=> We got us a try-hard!
//=> President or Father of Judaism??
//=> Bah, Humbug!
//=> Gee wilikers we got a Clay!
//=> Holy crap, its a Parsons!
``` 
Here we can see some of the glory of `Pattern Matchin' in Action`!!! First we create some instances of `FullName`, then we put them all in a list to give us a `FullName list`. Then we create a function `isFam` that takes a name as a parameter, and then pattern matches the name against a series of cases. You can see here that we can pattern match using multiple labels and values, as in the first case, or just one label/value. In the last case, we use `_` to denote it as the default case, meaning the names that don't match any of the defined cases will fall to this one. Finally we use the `iter` function from the `List` module to iterate through each name in our `names` list using the `isFam` function we provided.

This is just a glimpse of what pattern matching is capable of, the [Microsoft docs are here](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/pattern-matching).

The last thing that I want to write about is the `member-list` section from our original Microsoft definition, shown again below:
```fsharp
[ attributes ]
type [accessibility-modifier] typename =
    { [ mutable ] label1 : type1;
      [ mutable ] label2 : type2;
      ... }
    [ member-list ]
``` 
From my understanding, `members` in F# give us the ability to add a bit more "Object-Oriented*ness*" to our, for all intents and purposes, functional-style types. This is a roundabout way of saying that it gives us the ability to extend our F# types (not just classes) by adding functions and properties.

I'm gonna summarize a bit from [Scott Wlaschin's post about it here](https://fsharpforfunandprofit.com/posts/type-extensions/), but through a feature called `type extensions` we can add extra functionality to our record types (or any type). Let's see an example before going too much further:
```fsharp
type SexistFullName = { 
    FirstName: string; 
    MiddleName: string; 
    mutable LastName: string } with
    member this.UpdateLastName newName =
        this.LastName <- newName
    static member Empty = {FirstName = ""; MiddleName = ""; LastName = ""}
    override this.ToString() = 
        this.FirstName + " " + this.MiddleName + " " + this.LastName 
```
Here we have our `SexistFullName`, which has a `mutable` `LastName` field along with some additions. Let's break down what we have added:
- We used the `with` keyword to define the start of our `member-list`
- We added an `UpdateLastName` function (or is it now a method?) that updates the `LastName` value (or is it now a variable?), which we can do because we have declared `LastName` as `mutable`. 
- We added a `ToString` method that combines our `FullName`, `MiddleName`, and `LastName` into one string. 
- We define each of our new members with the `member` keyword(derp!)
- We prefix the name of our new `UpdateLastName` member with the `this` keyword, marking it as an `instance method`, meaning this function acts on an *instance* of `SexistFullName`, where `this` refers to the given instance. 
- We add `static` in front of our `Emtpy` member (which is this case is a property, not a method), marking it as a `static property`. By the very name, `static members` don't operate on a single instance of a class, but rather all instances of a class, and therefore we do not use the `this` keyword with it.
- We add a `ToString` `member method`, but in this case we actually need to `override` the ToString provided by .NET. This is pretty interesting, and shows that even F# records also inherit from the .NET `Object` class, just like all classes in C#. We use the `override` keyword to do this, and because the `ToString` member method already exists, we do not supply the `member` keyword again. 

What we have done here is "OOified" our record type, which is pretty cool, but it makes me wonder if this is something I should actually do, or if it is provided more for interop with the greater .NET ecosystem. I'd guess that, as always, it depends on your use case, but if you have some opinions on this then please send me an email and let me know what you think. 

This is really all I can think to cover here. If there is something that I have missed or something that seems incorrect then please send me an email so that I can update it. The purpose of these blogs is to teach others about F# but also to learn it myself! So any input is appreciated!