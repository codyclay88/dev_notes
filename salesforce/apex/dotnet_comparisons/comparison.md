
### What is similar?

Apex is very similar to C# - it is saved, compiled, and executed directly on the Lightning Platform. Like C#, it is also object oriented. 

### Execution Context
For ASP.NET apps, code is executed in the context of an application domain. In the Lightning Platform world, code executes within an Execution Context. This context represents the time between when the code is executed and when it ends. The important thing to understand is that the Apex code you write is not always the only code that is executing. 

To understand how this works, you need to know all the ways that Apex code can be executed on the platform. 

- Database Trigger: Invoked for a specific event on a custom or standard object
- Anonymous Apex: Code snippets executed on the fly in Dev Console and other tools
- Asynchronous Apex: Occurs when executing a future or queueable Apex, running a batch job, or scheduling Apex to run at a specific interval
- Web Services: Code that is exposed via SOAP or REST web services
- Email Services: Code that is set up to process inbound email
- Visualforce or Lightning Pages: Visualforce controllers and Lightning components can execute Apex code automatically or when a user initiates an action, such as clicking a button. Lightning components can also be executed by Lightning processes and flows. 

Besides invoking Apex code, actions, such as creating a new task, sending an email, performing a field update, or sending an outbound message, can all be triggered by one of the declarative platform features. These actions also run within an execution context. 

Another important consideration is the context of the user executing the Apex code. By default, Apex executes in the system context. Apex code has access to all objects and fields. Object permissions, field-level security, and sharing rules aren't applied for the current user. You can use the `with sharing` keyword to specify that the sharing rules for the current user be taken into account for a class. 

### Working With Limits
This brings us back to the subject of working with limits. The two limits you will probably be the most concerned with involve the number of SOQL queries or DML statements. These tend to trip up developers new to the platform. 

### Working in Bulk
Many developers fall into a common trap of designing their code to work with a single record. They quickly learn that on the Lightning Platform this can be a huge mistake. Apex triggers can receive up to 200 objects at once. Currently, the synchronous limit for the total number of SOQL queries is 100, and 150 for the total number of DML statements issued. So, if you have a trigger that performs a SOQL query or DML statement inside a loop and that trigger was fired for a bulk operation, then it is probably going to blow up in your face, in the form of a `limits error`. 

A common pattern for working around these issues is to "bulkify" your code. 