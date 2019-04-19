Distributed development is hard. 

The Saga Pattern is designed to solve one of the problems that make it hard: Transactions. In many cases your apps may need to perform a "multi-step" action, where when step 1 is completed successfully, then we move on to step 2, and after step 2 completed successfully, we move to step 3, and so on. 

In a distributed, eventually consistent system, it can be hard to know if when a change is made, that the change permeated successfully throughout the entire system. 

A saga is a sequence of local transactions where each transaction updates data within a single service. The first transaction is initiated by an external request corresponding to the system operation, and then each subsequent step is triggered by the completion of the previous one. 

There are multiple ways to implement a saga transaction, two of which are:
- Events/Choreography: When there is no central coordination, each service produces and listens to other service's events and decides if an action should be taken or not. 
- Command/Orchestration: When a coordinator service is responsible for centralizing the saga's decision making and sequencing business logic. 


### Events/Choreography
In the Events/Choreography approach, the first service executes a transaction and then published an event. This event is listened by one or more services which execute local transactions and publish (or not) new events.

The distributed transaction ends when the last service executes its local transaction and does not publish any events or the event published is not heard by any of the saga participants. 

In an e-commerce example:
- An *Order Service* would save a new order, set the state as pending, and publish an event called **ORDER_CREATED_EVENT**.
- The *Payment Service* listens to **ORDER_CREATED_EVENT**, charge the client and publish the event **BILLED_ORDER_EVENT**.
- The *Stock Service* listens to **BILLED_ORDER_EVENT**, update the stock, prepare the products bought in the order and publish **ORDER_PREPARED_EVENT** 
- The *Delivery Service* listens to **ORDER_PREPARED_EVENT** and then picks up and delivers the product. At the end, it publishes an **ORDER_DELIVERED_EVENT**
- Finally, the *Order Service* listens to **ORDER_DELIVERED_EVENT** and sets the state of the order as concluded. 
If the state of the order needs to be tracked, then the Order Service could listen to all the events and update the state accordingly. 

Rolling back a distributed transaction does not come for free. Normally you would have to implement another operation/transaction to compensate for what was done previously. 

In the previous example, if the Stock Service failed during a transaction, the rollback would look like this:
- The *Stock Service* produces a **PRODUCT_OUT_OF_STOCK_EVENT**
- Both the *Order Service* and the *Payment Service* listen for that message:
    - The *Payment Service* refunds the client
    - The *Order Service* sets the order state as failed. 
It is crucial in this case to define a common shared ID for each transaction, so whenever you throw an event, all listeners know with transaction it refers to. 

Benefits of Events/Choreography:
- Simple, easy to understand
- Loosely couples the services as they don't have direct knowledge of each other. 

Drawbacks:
- Can get confusing as you add more and more services and can be difficult to keep track of who cares about which messages. 
- Testing can be tricky as you have to have all the services running


### Command/Orchestration

In this approach, we define a new service with the sole responsibility of telling each participant what to do and when. The saga orchestrator communicates with each service in a command/reply style telling them what operations need to be performed. 

Back to our e-commerce example:
- The *Order Service* saves a pending order and asks the *Order Saga Orchestrator* to start a *Create Order Transaction*
- The *Order Saga Orchestrator* sends an *Execute Payment* command to the *Payment Service*, and it replies with a *Payment Executed* message
- The *Order Saga Orchestrator* sends a *Prepare Order* command to the *Stock Service*, and it replies with an *Order Prepared* message
- The *Order Saga Orchestrator* sends a *Deliver Order* command to the *Delivery Service*, and it replies with an *Order Delivered* message. 

The *Order Saga Orchestrator* knows what is needed to execute a *Create Order* transaction. If anything fails, it is also responsible for coordinating the rollback by sending commands to each participant to undo the previous operation. 

A standard way to model a saga or orchestrator is a State Machine where each transformation corresponds to a command or message. State machines are an excellent pattern to structure well-defined behavior as they are easy to implement and great for testing. 

Rollbacks also become a bit easier when you have an orchestrator to coordinate everything:
- The *Stock Service* replies with an *Out-of-Stock" message
- The *Order Sage Orchestrator* recognizes that the transaction has failed and starts the rollback
    - In this case, only a single operation was executed successfully before the failure, so the OSO sends a *Refund Client* command to the *Payment Service* and sets the order state as failed. 

Benefits:
- Centralize the orchestration of the distributed transaction
- Reduces the complexity of the participants as they only need to execute/reply to commands. 
- Easier to implement and test
- Rollbacks are easier to manage

The important thing to note here is that you shouldn't put too much logic in the Orchestrator, ending up with too smart and orchestrator with dumb services. 





