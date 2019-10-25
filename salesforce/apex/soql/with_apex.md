
We can combine SOQL with Apex in Triggers to access fields that are not in Trigger.new, and modify records not in Trigger.new

How can we use SOQL in Apex?
- Store the results of a SOQL query in a list
- Bind variables 

### Storing SOQL Query Results in Lists
```apex
List<Lead> leads = [SELECT Id FROM Lead];

List<Account> accounts = [SELECT Id, Name FROM Account];

List<Case> cases = [
    SELECT Id,
           Owner.Username,
      FROM Case
     WHERE Orgin = 'Web'
       AND CreatedDate = THIS_YEAR
  ORDER BY Status ASC, CreatedDate DESC
];
```

The following example is a trigger where you want to update the contact address information for each contact in a given account every time the account address information is changed.

```apex
trigger UpdateContactAddresses on Account (before update) {
    for (Account acc : Trigger.new) {
        List<Contact> contacts = [
            SELECT Id 
              FROM Contacts 
             WHERE AccountId = :acc.Id   
             // ^NOTE THE COLON, this binds an external variable
        ];
        for (Contact myCon : contacts) {
            myCon.MailingStreet     = acc.BillingStreet;
            myCon.MailingCity       = acc.BillingCity;
            myCon.MailingState      = acc.BillingState;
            myCon.MailingCountry    = acc.BillingCountry;
            myCon.MailingPostalCode = acc.BillingPostalCode;
        }
        update contacts;
    }
}
```

