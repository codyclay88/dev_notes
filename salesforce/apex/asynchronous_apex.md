### When to go Asynchronous

The following three reasons are usually behind choosing asynchronous programming:
- **Processing a very large number of records.** This reason is unique to the multi-tenanted world of the Lightning Platform where limits rule. The limits associated with asynchronous processes are higher than those with synchronous processes. Therefore, if you need to process thousands or even millions of records, asynchronous programming is your best bet. 
- **Making callouts to external web services.** Callouts can take a long time to process, but in the Lightning Platform, triggers can't make callouts directly. 
- **Creating a better, faster user experience** by offloading some processing to asynchronous calls. Why do everything at once? If it can wait, let it. 

### Future Methods
In situations where you eed to make a callout to a web service or want to offload simple processing to an asynchronous task, creating a future method could be the way to go. 

Changing a method from synchronous to asynchronous processing is amazingly easy. Essentially you just add the `@future` annotation to your method. Other than that, just make sure that the method is static and returns only a void type. For example, to create a method for performing a web service callout, we could do something like this:

```java
public class MyFutureClass {
    // Include callout=true when making callouts
    @future(callout=true)
    static void myFutureMethod(Set<Id> ids) {
        List<Contact> contacts = [SELECT Id, LastName, FirstName, Email FROM Contact WHERE Id IN :ids];
        for (Contact con: contacts) {
            String response = anotherClass.calloutMethod(con.Id, con.FirstName, con.LastName, con.Email);
        }
    }
}
```

### Future Limitations
Future methods have been around for several years. Although they offer a great option for Salesforce developers, they're not without drawbacks. Here are some limitations to consider before using a future method. 
- You can't track execution because no Apex job ID is returned. 
- Parameters must be primitive data types, arrays of primitive data types, or collections of primitive data types. Future methods can't take objects as arguments. 
- You can't chain future methods and have one call another. 

### Batch or Scheduled Apex
Another long-used asynchronous tool is the batchable interface. The No. 1 reason to use it is if you need to process a large number of records. For example, if you want to clean up or archive up to 50 million records, the batchable interface is your answer. You can even schedule batches to run at a particular time. 

To use it, your class implements the `Database.Batchable` interface. You also define `start()`, `execute()`, and `finish()` methods. You can then invoke a batch class using the `Database.executeBatch` method. For example, the following code creates a batchable class that processes all accounts in an org and then sends an email when it is done. 

```java
global class MyBatchableClass implements Database.Batchable<SObject>, Database.Stateful {
    global Integer numOfRecs = 0;
    global Database.QueryLocator start(Database.BatchableContext bc) {
        return Database.getQueryLocator('SELECT Id, Name FROM Account');
    }
    global void execute(Database.BatchableContext bc, List<Account> scope) {
        for (Account acc : scope) {
            numOfRecs = numOfRecs + 1;
        }
    }
    global void finish(Database.BatchableContext bc) {
        EmailManager.sendMail('someAddress@somewherecom', numOfRecs + ' Accounts were processed!', 'Meet me at the bar for drinks to celebrate');
    }
}
```

You could then invoke the batch class using anonymous code such as this:
```java
MyBatchableClass myBatchObject = new MyBatchableClass();
Database.executeBatch(myBatchObject);
```

### Batchable Limitations
The batchable interface is great, but as with just about everything, consider its limitations. 
- Troubleshooting can be troublesome.
- Jobs are queued and subject to server availability, which can sometimes take longer than anticipated.
- Limits, oh limits. 

### Queueable Apex
For a long time, future methods and the batchable interface were the primary ways developers had to do asynchronous processing. But remember all those limitations we talked about? Well, they were causing problems for some developers, so there was an outcry for a better solution. 

In Winter '15, Salesforce responded with Queueable Apex. It represents the best of future methods and the batchable interface, all rolled into one super-duper asynchronous tool. Developers forced to use the slower batchable interface to get around limitations of future methods could now return to a tool that made more sense. Queueable Apex provides the following benefits to future methods:
- Non-primitive types: Classes can accept parameter variables of non-primitive data types, such as sObjects or custom Apex types. 
- Monitoring: When you submit your job, a jobId is returned that you can use to identify the job and monitor its progress
- Chaining jobs: you can chain one job to another job by starting a second job from a running job. Chaining jobs is useful for sequential processing. 

Because Queueable Apex includes the best of future methods, it's much easier to implement that Batch Apex. It just doesn't have those pesky limits we talked about. To demonstrate how it works, let's take the sample code we used to create a future method to do a web callout, but implement it with Queueable Apex. 
```java
public class MyQueueableClass implements Queueable {
    private List<Contact> contacts;

    public MyQueueableClass(List<Contact> myContacts) {
        contacts = myContacts;
    }

    public void execute(QueueableContext context) {
        for (Contact con : contacts) {
            String response = anotherCLass.calloutMethod(con.Id, con.FirstName, con.LastName, con.Email);
        }
    }
}
```

To invoke Queueable Apex, you need something like the following:
```java
List<Contact> contacts = [SELECT Id, LastName, FirstName, Email FROM Contact WHERE Is_Active__c = true];
Id jobId = System.enqueueJob(new MyQueueableeClass(contacts));
```
