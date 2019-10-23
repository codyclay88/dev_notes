What is a Test Class?

A Test Class is Apex code that tests other Apex code. 

Salesforce actually requires that your code be tested before it goes into production. 
These are the following requirements that Salesforce places on all production code:
- 75% of all the lines of code must be executed by test classes
- All assertions in our test classes must pass

#### All test classes in your org must pass to deploy code into production

Let's write a test class for the following code:

```apex
trigger HelloWorld on Lead (before update) {
    for (Lead l : Trigger.new) {
        l.FirstName = 'Hello';
        l.LastName = 'World';
    }
}
```

Test class:
```apex
@isTest
private class HelloWorldTest {
    @isTest static void updateLead() {
        Lead myLead = new Lead();
        myLead.FirstName = 'Cody';
        myLead.LastName  = 'Clay';
        myLead.Company   = 'Advantage Technology';
        insert myLead;
    
        myLead.Company = 'Pluralsight';
        update myLead;
    }
}
```

The `@isTest` annotation tells Salesforce to treat the code differently. 

Note: When writing test classes, you will actually be operating against a "test database", this database has no records in it! So you must understand this when writing your test code and create any auxiliary records that are necessary for your test. 

Lets do some more:

```apex
trigger AppleWatch on Opportunity (after insert) {
    for(Opportunity opp : Trigger.new) {
        Task t = new Task();
        t.Subject     = 'Apple Watch Promo';
        t.Description = 'Send them on ASAP';
        t.Priority    = 'High';
        t.WhatId      = opp.Id;
        insert t;
    }
}
```

Test class:
```apex
@isTest
private class AppleWatchTest {
    @isTest static void createOpportunity() {
        Opportunity opp = new Opportunity();
        opp.Name = 'Swift Deal';
        opp.StageName = 'Prospecting';
        opp.CloseDate = Date.today();

        insert opp;
    }
}
```

And another!
```apex
trigger AppleWatchGold on Opportunity (after insert) {
    for(Opportunity opp : Trigger.new) {
        if(opp.Amount > 50000) {
            Task t = new Task();
            t.Subject     = 'Apple Watch Promo';
            t.Description = 'Send them on ASAP';
            t.Priority    = 'High';
            t.WhatId      = opp.Id;
            insert t;
        }
    }
}
```

Test class:
```apex
@isTest
private class AppleWatchGoldTest {
    @isTest static void createOpportunity() {
        Opportunity opp = new Opportunity();
        opp.Name      = 'Swift Deal';
        opp.StageName = 'Prospecting';
        opp.CloseDate = Date.today();
        opp.Amount    = 55000;
        insert opp;
    }
}
```

And Another!

```apex
trigger DedupeReminder on Account (after insert) {
    for (Account acc : Trigger.new) {
        Case c = new Case();
        c.Subject   = 'Dedupe this account';
        c.OwnerId   = 'SOME_USER_ID';
        c.AccountId = acc.Id;
        insert c;
    }
}
```

Test class:
```apex
@isTest 
private class DedupeReminderTest {
    @isTest static void createAccount() {
        Account acc = new Account();
        acc.Name    = 'SFDC99';
        insert acc;
    } 
}
```
