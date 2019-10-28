
### The General Trigger Pattern
```apex
trigger <<NAME>> on <<OBJECT>> (<<before/after>> <<event>>) {
    for(<<Object>> <<variable>> : Trigger.new) {
        // Create an object (optional)
        // Update record
        // Explicit save (optional)
    }
}
```


```apex
trigger HelloWorld on Lead (before update) {
    for (Lead l : Trigger.new) {
        l.FirstName = 'Hello';
        l.LastName = 'World';
    }
}
```

- `HelloWorld` is the name of the trigger
- `Lead` is the object type that is acted upon
- `(before update)` is the event that we are operating on. This could be a comma-separated list of multiple events that we want to trigger on. This is saying to run this before the update occurs
- `for(Lead l : Trigger.new)` defines a loop for our trigger. All triggers we write will have a loop like this. This is because multiple triggers could be processed at one time, so we want to iterate over multiple triggers at once. This line says that for all new Triggers, assign it to the variable denoted by the letter 'l'. 
- `l.FirstName = 'Hello';` is saying that we want to access the FirstName field in the record.


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
- `AppleWatch` is the name of the trigger
- `Opportunity` is the object type that we are operating on. 
- `(after insert)` is the event that we are triggering from
- `Task t = new Task();` creates a new Task object
- `insert t;` inserts the new task that we created into the Task table. 


```apex
trigger WarrantySummary on Case (before insert) {
    for (Case myCase : Trigger.new) {
        Date purchaseDate          = myCase.Product_Purchase_Date__C;
        DateTime createdDate       = DateTime.now();
        Integer warrantyDays       = myCase.Product_Total_Warranty_Days__c;
        Decimal warrantyPercentage = (100 * (purchaseDate.daysBetween(Date.today()) / 
                                        myCase.Product_Total_Warranty_Days__c)).setScale(2);
        Bool hasExtendedWarranty   = myCase.Has_Extended_Warranty__c;

        myCase.Warranty_Summary_c 
            = 'Product purchased on ' + purchaseDate.format() 
            + ' and case created on ' + createdDate.format()  + '\n'
            + 'Warranty is for ' + warrantyDays
            + ' days and is ' + warrantyPercentage + '% through its warranty period.\n'
            + 'Extended warranty: ' + hasExtendedWarranty + '\n'
            + 'Have a nice day!';

    }
}
```