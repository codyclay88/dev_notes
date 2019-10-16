
[Microsoft docs](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/interfaces) say that "interfaces define a contract that can be implemented by classes and structs. An interface can contain methods, properties, events, and indexers."

Prior to C# 8, interfaces could not define an implementation, they were strictly contracts that defined the behavior that a class has, but they could not define how the behavior was implemented. 

Rather than talk about it, let's actually look at one. 
```csharp
// interfaces are defined by the *interface* keyword
public interface IQuoteAdamSandlerMovies
{
    string MakeMeChuckle();
}
```
I know what you are thinking, could I have come up with a better example? Probably not, but let's break this down a little bit. 

// MAKE NOTE OF NO ACCESS MODIFIERS

Notice that `MakeMeChuckle()` does not provide an implementation, it is just a definition. Because of this, an interface cannot be instantiated. The following will not compile:

```csharp
// WRONG!
var quoteGenerator = new IQuoteAdamSandlerMovies();
```

Again, interfaces define *what* can be done, not *how* to do it, so it makes since that you wouldn't be able to just "new" one up. An interface requires a class to define an implementation. Let's create one!

```csharp
public class SimpleBillyMadisonQuoter : IQuoteAdamSandlerMovies
{
    string MakeMeChuckle() => "Stop looking at me swan!";
}
```

Now that we have an implementation, we can create an instance of it.
```csharp
var quoteGenerator = new SimpleBillyMadisonQuoter();
quoteGenerator.MakeMeChuckle();
// => "Stop looking at me swan!"
```


```csharp
public interface IEnumerable
{
    IEnumerator GetEnumerator();
}
```
What you are seeing above is the `IEnumerable` interface defined in the `System.Collections`
namespace provided by .NET.  




*We'll take a look at the new C# 8 features in a little bit*

Because interfaces only define *what* an object can do, they cannot be used on their own. You cannot create an instance of an interface because on it's own, an interface does not provide implementation. For example, the following will not compile. 

```csharp
var doer = new IDoThings();
```




As of C# 8 however, this has changed. Interfaces can now define a default implementation for it's members, allowing classes that implement an interface to now inherit that default implementation. 

Let's take a look at this using the new features in C# 8. 
```csharp
public interface IDoThings
{
    void DoSomething();
    void DoSomethingElse() => Console.WriteLine("I'm doing something else!");
}
```

This allows us to extend existing interfaces without breaking any classes that previously implemented this interface.  

Interfaces may employ multiple inheritance, meaning that a single class can implement multiple interfaces. 

// TALK ABOUT HOW INTERFACES ARE USEFUL

// POLYMORPHISM

// THE NOTION OF POLYMORPHISM ENABLES DESIGN PATTERNS 
