### Data Manipulation Language

DML statements are statements that we can use to manipulate data directly in the Salesforce database. We can do this by using the `insert`, `update`, `delete`, `upsert`, `undelete`, and `merge` statements with either single instances of `SObject` classes, or collections of `SObject` classes. 

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

The `upsert` DML operation create new records and updates sObject records within a single statement, using a specified field to determine the presence of existing objects, or the ID field if no field is specified. 

The `merge` statement merges up to three records of the same sObject type into one of the records, deleting the others, and re-parenting any related records. 

### ID Field Auto-Assigned to New Records
When inserting records, the system assigns an ID for each record. In addition to persisting the ID value in the database, the ID value is also autopopulated on the sObject variable that you used as an argument in the DML call. 

### Bulk DML
You can perform DML operations either on a single sObject, or in bulk on a list of sObjects. Performing bulk DML operations is the recommended way because it helps avoid hitting governor limits, such as the DML limit of 150 statements per Apex transaction. This limit is in place to ensure fair access to shared resources in the Lightning Platform. Performing a DML operation on a list of sObjects counts as one DML statement, not as one statement for each sObject. 

This example inserts contacts in bulk by inserting a list of contacts in one call. The sample then updates those contacts in bulk too

```java
List<Contact> conList = new List<Contact> {
    new Contact(FirstName='Joe',LastName='Smith',Department='Finance'),
    new Contact(FirstName='Kathy',LastName='Smith',Department='Technology'),
    new Contact(FirstName='Caroline',LastName='Roth',Department='Finance'),
    new Contact(FirstName='Kim',LastName='Shain',Department='Education')
};

insert conList;

List<Contact> listToUpdate = new List<Contact>();

for (Contact con : conList) {
    if(con.Department == 'Finance') {
        con.Title = 'Financial analyst';
        listToUpdate.add(con);
    }
}

update listToUpdate;
```

### Upserting Records
If you have a list containing a mix of new and existing records, you can process insertions and updates to all records in the list by using the `upsert` statement. Upsert helps avoid the creation of duplicate records and can save you time as you don't have to determine which records exist first. 

```
upsert sObject|sObject[]
upsert sObject|sObject[] field
```

Example
```java
upsert sObjectList Account.Fields.MyExternalId;
```

Upsert uses the sObject record's primary key (the ID), an idLookup field, or an external ID field to determine whether it should create a new record or update an existing one:
- If the key is not matched, a new object record is created
- If the key is matched once, the existing object record is updated
- If the key is matched multiple times, an error is generated and the object record is neither inserted or updated