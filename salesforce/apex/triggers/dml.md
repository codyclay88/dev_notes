### Data Manipulation Language

DML statements are statements that we can use to manipulate data directly in the Salesforce database. We can do this by using the `insert`, `update`, and `delete` statements with either single instances of `SObject` classes, or collections of `SObject` classes. 

Examples:
```apex
Case newCase = new Case();
newCase.Subject = 'Silly Example';
insert newCase; // This is the dml statement
```

```apex
List<Case> cases = new List<Case>();
cases.add(myCase);
cases.add(yourCase);
delete cases; // This is the dml statement, deleting a collection of SObjects
```