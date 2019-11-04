

### Event-driven Architecture

An Event is a thing that happened that we want to broadcast to anyone who cares, our medium of broadcasting is commonly called a bus. A Publisher can publish one or more events, and subscriber can listen for those events and choose to act on that event or not. Each Subscriber gets its own instance of each event, and subscribers and replay historical events. 

Each bus is specific to one particular type of event. Each subscriber that is subscribed to a bus gets all the events that get published, and multiple publishers can publish to a single bus. 

Publishers don't know anything about the subscribers, and the subscribers don't have any idea about who the Publishers are. 

### Salesforce Event Bus
Salesforce provides us an event bus that we can use to decouple our various components, both within Salesforce and outside of Salesforce. 

### Platform Event Objects
Salesforce allows us to define our own custom Platform Event Objects. These are very similar to normal SObject types that we would normally create. Platform Events can contain custom fields, which take the form of primitive data types, which allow us to provide some context about the event. 

