When creation of an object, or an entire aggregate, becomes too complex or reveals too much about the internal structure, factories provide encapsulation. 

Complex object creation is a responsibility of the domain layer, yet that task does not belong to the objects that express the model. The idea here is that you don't expect a car to build itself, there are complex pieces of machinery that do that for us. 

It's important to note that Factories themselves do not correspond to anything in the model, but they are part of the domain layer's responsibility. 

Client ==new(parameters)=> Factory ==create==> product

The interface of an object should encapsulate its implementation, thus allowing a client to use the object's behavior without knowing how it works. 
In the same way, a Factory encapsulates the knowledge needed to create a complex object. 

The two basic requirements for a good factory are:
- Each creation method is atomic and enforces all invariants of the created object or aggregate. A Factory should only be able to produce an object in a consistent state. For an entity, this means the creation of the entire aggregate, with all invariants satisfied, but probably with optional elements still to be added. For an immutable Value Object, this means that all attributes are initialized to their correct final state. If the interface makes it possible to request an object that can't be created correctly, then an exception should be raised or some other mechanism should be invoked that will ensure that no improper return value is possible. 
- The Factory should be abstracted to the type desired, rather than the concrete classes created. 

Factories are necessary when creating new objects. Factories should not be involved in persistence. 
Repositories are used to find and update existing objects. 