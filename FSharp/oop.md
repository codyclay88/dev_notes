
As a beginner in F#, I read regularly that while using F# that you need to think *functionally*. I have been reading [some great tutorials from Scott Wlaschin](https://fsharpforfunandprofit.com/series/thinking-functionally.html) on exactly how to do that, but as I look through the F# docs I feel like there is quite a bit of Object-Oriented content that I should also get a good grasp of. This post is going to go over some of the basics when it comes to programming in the OOP style with F#. 

As stated in the [Microsoft docs](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/classes), classes are types that represent objects that can have properties, methods, and events. 

The docs also give us this lovely grammar (is that the right term?) for defining a class:
```fsharp
// Class definition:
type [access-modifier] type-name [type-params] [access-modifier] ( parameter-list ) [ as identifier ] =
[ class ]
[ inherit base-type-name(base-constructor-args) ]
[ let-bindings ]
[ do-bindings ]
member-list
...
[ end ]
// Mutually recursive class definitions:
type [access-modifier] type-name1 ...
and [access-modifier] type-name2 ...
...
```
Cool beans. So this looks a bit cryptic, but luckily they do go into detail about the different parts presented here. Let's break down the required bits first. 

The `type-name` is any valid identifier. 

The `parameter-list` describes the constructor parameters. According to the docs, "there is always a primary constructor whose arguments are defined in the `parameter-list` that follows the type name, whose body consists of `let` and `let rec` bindings at the start of the class declaration and the `do` bindings that follow. The arguments of the primary constructor are in scope throughout the class definition." This tells me that the default constructor is built into the class definition itself, and that the parameters passed to the constructor are then  immutable private fields (Confirmed [here by Scott Wlaschin](https://fsharpforfunandprofit.com/posts/classes/)). 

You can add additional constructors by using the `new` keyword to add a member, like so:
```fsharp
// lets create a FullName record type
type FullName = {
    FirstName: string
    LastName: string
}

// our beautiful class
type Person (firstName, lastName) =
    // additional constructors must call the primary constructor
    // default constructor with no params
    new() = Person("Grizzly", "Adams")
    // using the FullName type above
    new(fullName: FullName) = Person(fullName.FirstName, fullName.LastName)
    
    // members
    member this.FirstName: string = firstName
    member this.LastName: string = lastName

// Can now create a Person in three ways
let beard = new Person()
let me = new Person("Cody", "Clay") // <- the primary constructor
let myBrother = new Person({FirstName = "Jordan"; LastName = "Clay"})
```
As seen in the comments in the example above, any additional constructors must call the primary constructor. 

The `member-list` consists of "additional constructors, instance, and static method declarations, interface declarations, abstract bindings, and property and event declarations".  

`Members` actually has [its own section in the docs](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/members/index), and it seems like it's scope extends beyond classes and the OO features of F#. I'll try to touch on them a bit here. 

The docs say that "`Members` are features that are part of a type definition and are declared with the `member` keyword. F# object types such as records, classes, discriminated unions, interfaces, and structures all support members". With this knowledge, we can assume that members are essentially the public interface that our class (or various other types, so it seems) provides, which could be properties, methods, interface declarations, etc. 

In the previous example, our `Person` class had several lines that began with the `member` keyword:
```fsharp
...
member this.FirstName: string = firstName
member this.LastName: string = lastName
...
```
These are examples of `property members`. These are similar to properties that you would use in C#, except that the default behavior is that they are read-only. This would be the equivalent of the following in C#:
```csharp
...
public string FirstName { get; }
public string LastName { get; }
...
``` 
Back to our F# implementation, you can see the `this` keyword in front of our member names. As [stated by Scott Wlaschin](https://fsharpforfunandprofit.com/posts/classes/) (same link that was referenced earlier, you should probably read it too), this is a "self-identifier" that can be used to refer to the current instance of the class. Every non-static member must have a self identifier. 

We could extend out Person class from earlier to add some member methods:
```fsharp
type Person (firstName, lastName) =
    // constructor members (yes, these are members too)
    new() = Person("Grizzly", "Adams")
    new(fullName: FullName) = Person(fullName.FirstName, fullName.LastName)

    // property members
    member this.FirstName: string = firstName
    member this.LastName: string = lastName

    // method members
    member this.HollaAtYaBoi yaBoi =
        printfn "Its ya boi %s!!!" yaBoi // People do this, right?
```
This is nearly the same as creating a standalone function as you normally would, but we can now `HollaAtYaBoi` from an instance of a Person:
```fsharp
let me = new Person("Cody", "Clay")
me.HollaAtYaBoi "T-Pain"
// Its ya boi T-Pain!!!
//=> val it : unit = ()     // this is the actual output
```

At this point, I'd like to take a second to talk about class signatures. By sending this code to F# Interactive, I got this class signature back:
```fsharp
type Person =
  class
    new : unit -> Person
    new : fullName:FullName -> Person
    new : firstName:string * lastName:string -> Person
    member HollaAtYaBoi : yaBoi:string -> unit
    member FirstName : string
    member LastName : string
  end
```
Yet again, [Scott Wlaschin does a great job](https://fsharpforfunandprofit.com/posts/classes/) explaining these topics, so I will try my best here to avoid blatant plagiarism. 

The class signature that gets generated contains the signatures for all the constructors, methods, and properties in your class (almost verbatim plagiarism, I apologize). These signatures are similar to what you'd see in normal F# values and functions, but the signatures imply a sort of "context awareness" to the class that they exist within. 

For example, the signature for the property members, `member FirstName : string` and `member LastName : string` are very similar to what you'd see for normal values aside from being prefixed with `member` instead of `val` and not giving an explicit value (which makes a bit of sense, because their is no instance just yet and therefore the *instance members* can't really have a value). 

The member method signature, `member HollaAtYaBoi : yaBoi:string -> unit` is again similar to what you'd see for a normal standalone function aside for beginning with `member` and that the parameters are explicitly named. 

The outlier here is the constructor definitions: 
```fsharp
new : unit -> Person
new : fullName:FullName -> Person
new : firstName:string * lastName:string -> Person
```
These have their own sort of unique syntax, in that they begin with the `new` keyword, and that each constructor returns a `Person`. Also note that the signature for the third constructor here (which is actually the primary constructor) takes a tuple of `firstName: string * lastName: string` rather than two distinct parameters, which would have been the case when using the same `ClassName(param1, param2, ...)` syntax in C#, for example. 

Moving on from the class definition, let's say that we have some "helper" methods that we want to define in our class, but we want them to be private (not exposed to the outside world). Let's go back to Person again:
```fsharp
type Person (firstName, lastName) =
    // private field
    let fullName = { FirstName = firstName; LastName = lastName }
    // ^ note that we are using firstName and lastName here, 
    // which were passed in from the constructor. Proof that constructor
    // parameters become immutable, private, fields once instantiated. 

    // private functions
    let greetYaBoiInThirdPerson (yaBoi: string) = 
        fullName.FirstName + " says: Its ya boi " + yaBoi + "!!!!"
        // ^ note we are using the fullName private field here

    // constructor members (yes, these are members too)
    new() = Person("Grizzly", "Adams")
    new(fullName: FullName) = Person(fullName.FirstName, fullName.LastName)

    // property members
    member this.FirstName: string = firstName
    member this.LastName: string = lastName

    // method members
    member this.HollaAtYaBoi yaBoi =
        printfn "%s" (greetYaBoiInThirdPerson yaBoi) // modified to use private function
```
We have introduced a function called `greetYaBoiInThirdPerson` using the `let` keyword. In the context of a class defintion, using the `let` keyword will create private fields and functions, so this function cannot be accessed from external callers. An example of a private field is seen above it with the `fullName` value, which is creating a `FullName` instance (this type was created in an earlier snippet) from the `firstName` and `lastName` constructor parameters.

As a confirmation that these private fields can't be accessed from outside the class, the class definition generated by the compiler in F# Interactive did not change after recompiling the `Person` class with these changes. 

Let's go back to constructors for a second. The constructor that F# provides is different from what we might see in a language like C#. In C#, a constructor might look like this: 
```csharp
public class Person 
{
    public Person(string firstName, string lastName) 
    {
        Console.WriteLine($"Creating Person: {firstName} {lastName}");
        FirstName = firstName;
        LastName = lastName;
        Console.WriteLine($"Created Person: {this.FirstName} {this.LastName}");
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; } 
}
``` 
In this example we are performing a bit of a "side effect", which is simply logging out our progress while creating our `Person` instance. These side effects could be anything from logging to running database queries or whatever you want. 

If F#, the constructors act a bit different. Lets go back to our F# implementation of `Person`, this time removing the secondary constructors:
```fsharp
type Person (firstName, lastName) =
    // private field
    let fullName = { FirstName = firstName; LastName = lastName }

    // private functions
    let greetYaBoiInThirdPerson (yaBoi: string) = 
        fullName.FirstName + " says: Its ya boi " + yaBoi + "!!!!"

    // property members
    member this.FirstName: string = firstName
    member this.LastName: string = lastName

    // method members
    member this.HollaAtYaBoi yaBoi =
        printfn "%s" (greetYaBoiInThirdPerson yaBoi) 
```
So now that we are left with only our primary constructor, we can see that there isn't really an explicit place to write that sort of "side effect" code that we would have put in the `public Person(...) { }` block in C#. 

The answer to this in F# is to use `do` bindings. The combination of `let` and `do` bindings form the body of the primary constructor, and will execute whenever an instance of the class is created. 

Let's update our `Person` class to show this functionality:
```fsharp
type FullName = { ... }

type Person (firstName, lastName) =
    // we can create `do` bindings at any point BEFORE the member defintions
    do printfn "Creating Person %s %s" firstName lastName

    // private field
    let fullName = { FirstName = firstName; LastName = lastName } 
    
    // private functions
    let fullNameToString = fullName.FirstName + " " + fullName.LastName

    let greetYaBoiInThirdPerson (yaBoi: string) = 
        fullNameToString + " says: Its ya boi " + yaBoi + "!!!!"

    // we can mix `do` and `let` bindings
    do printfn "Created Person %s" fullNameToString   

    // at this point we can no longer use `do` bindings.
    // property members
    member this.FullName: FullName = fullName

    // method members
    member this.HollaAtYaBoi yaBoi =
        printfn "%s" (greetYaBoiInThirdPerson yaBoi)
```

`do` bindings must come before `member` definitions.