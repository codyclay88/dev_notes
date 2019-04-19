Most objects that you use within an application have a lifecycle that outlives a single instance. 

When you create an object, generally you must reconstitute it's state from it's last known values, then do stuff with that object, then save those changes. 

A repository represents all objects of a certain type as a conceptual set, like a collection with more elaborate querying capability. 

You should think of a repository as an in-memory collection. When calling a repository, you should not assume that any database access is performed as a result of calling it. Your calling code doesnt care about how it gets the data, just that it gets it. 

Repositories should only be created for aggregate roots. When persisting an aggregate root, all the child entities should be persisted as well. 

Repositories are used to find and update existing objects. 
Factories are necessary when creating new objects. 