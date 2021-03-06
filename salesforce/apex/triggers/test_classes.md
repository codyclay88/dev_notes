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

And another!
```apex
trigger CalculateLeadingCompetitor on Opportunity (before insert, before update) {
    for (Opportunity opp : Trigger.new) {
        List<Decimal> compPrices = new List<Decimal>();
        List<String> compNames = new List<String>();
        
        compPrices.add(opp.Competitor_1_Price__c);
        compPrices.add(opp.Competitor_2_Price__c);
        compPrices.add(opp.Competitor_3_Price__c);
        
        compNames.add(opp.Competitor_1__c);
        compNames.add(opp.Competitor_2__c);
        compNames.add(opp.Competitor_3__c);
        
        Integer bestIndex = 0;
        Decimal bestPrice = compPrices.get(0); 
        for(Integer i = 1; i < compPrices.size(); i++) {
            Decimal price = compPrices.get(i);
            if(price != null && price < bestPrice) {
                bestIndex = i;
            }
        }
        
        opp.Leading_Competitor__c = compNames.get(bestIndex);
    }
}
```
Test class:
```apex
@isTest
private class CalculateLeadingCompetitorTest {
    @isTest static void test() {
        Opportunity opp = new Opportunity();
        opp.Competitor_1__c = "Amazon";
        opp.Competitor_1_Price__c = 10000;
        opp.Competitor_2__c = "Google";
        opp.Competitor_2_Price__c = 5000;
        opp.Competitor_3__c = "Microsoft";
        opp.Competitor_3_Price__c = 20000;
        insert opp;

        opp.Competitor_3_Price__c = 15000;
        update opp;
    }
}
```
