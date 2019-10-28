# Domain Driven Design Notes
#### From Pluralsight course "Domain Driven Design Fundamentals" by Steve Smith and Julie Lerman


## Introduction
Benefits of DDD:
- Flexibility
- Maps the Customer's vision of the problem
- Provides a path through a very complex problem
- Well-organized and easily tested code
- Many great patterns to leverage

DDD provides many technical benefits, such as maintainability, but these concepts should be applied only to complex domains where the model and the linguistic processes provide clear benefits in the communication of complex information, and in the formulation of a common understanding of a domain. 

This leads to some drawbacks of DDD:
- Time and effort
    - discuss & model the domain with domain experts
    - Isolate domain logic from other parts of the application
- Learning curve
    - new principles
    - new patterns
    - new processes
- Only makes sense when there is complexity in the problem
    - Not just CRUD or data-driven applications
    - Not just technical complexity without business domain complexity 
- Team or company buy-in to DDD

DDD focuses on several things:
1. Interaction with Domain Experts
2. Model a single Sub-domain at a time
3. Implementation of Sub-Domains

The key takeaway is that DDD makes it easier to solve complex problems through software by understanding the clients needs. 

## Modelling Problems in Software
Key to a DDD solution is breaking up the business domain into smaller subdomains using bounded contexts and ubiquitous language. This can be achieved through communication with Domain Experts, which are individuals that understand the various business problems. 

As you develop your models, ensure that you develop bounded contexts. Meaning, rules that apply in one part of the system, may not apply in another. 

"Explicitly define the context within which a model applies.. Keep the model strictly consisten within these bounds, but don't be distracted or confused by issues outside"
quoted by Eric Evans

Ideally, each bounded context should be responsible for its own data, its own code, and even its own team of developers. Practically though, this level of separation is not always possible, but the concept should always be considered. 

A Sub-domain is a problem space concept, where a bounded context is a solution space concept. The Sub-domain describes how the business has been broken down in its constituent parts, while the Bounded Context describes how the software has been broken down to most aptly solve the problems outlined in the sub-domain. Ideally, these two things correspond, but they don't always. 

A Concept Map helps us visualize the different parts of the application. 

Across your different bounded contexts, there may be concepts that are shared. For example, authentication. This type of functionality should be placed in the Shared Kernel. 

## Elements of a Domain Model
This section focuses on the programming concepts that are used when modelling a bounded context.

"The Domain Layer is responsible for representing the concepts of the business, information about the business situation, and business rules. State that reflects the business situation is controlled and used here, even though the technical details of storing are delegated to the infrastructure. This layer is the heart of business software" - Eric Evans

Think in terms of *behaviors* - for example: "Schedule an appointment", "Accept a new patient", "Book a room" - rather than attributes - such as "Appointment.Time", "Customer.Telephone", "Room.Number". 

### Anemic Vs. Rich Domain Models
An anemic domain model is focused on the state of it's objects rather than behavior. These are classes that are typically nothing more than "bags of getters and setters". These types of domain models have a negative connotation in a DDD context. 





## Glossary

- Shared Kernel - library that contains code that is shared throughout your project 