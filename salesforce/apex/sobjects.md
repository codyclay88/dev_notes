Because Apex is tightly integrated with the database, you can access Salesforce records and their fields directly from Apex. Every record is natively represented as an sObject in Apex. For example, the Acme account record corresponds to an Account sObject in Apex. The fields of an Acme record that you can view and modify in the user interface can be read and modified directly on the sObject as well. 

Each Salesforce record is represented as an sObject before it is inserted into Salesforce. Likewise, when persisted records are retrieved from Salesforce, they are stored in an sObject variable. 

The name of sObjects correspond to the API names of the corresponding standard or custom objects. Similarly, the names of sObject fields correspond to the API names of the corresponding fields. 

API names of objects and fields can differ from their labels. For example, the Employees field has a label of Employees and appears on the account record page as Employees but its API name is NumberOfEmployees. To access this field in Apex, you'll need to use the API name, NumberOfEmployees. 

For custom objects and fields, the API name always ends with the __c suffix. For custom relationship fields, the API name ends with the __r suffix. 

For example:
- A custom object with a label of Merchandise has an API name of Merchandise__c
- A custom field with a label of Description has an API name of Description__c
- A custom relationship field with a label of Items has an API name of Items__r

### Create sObjects and Adding Fields
Before you can insert a Salesforce record, you must create it in memory first as an sObject. Like with any other object, sObjects are create with the `new` operator. 

```java
Account acct = new Account();
```

The API object name becomes the data type of the sObject variable in Apex. In this example, `Account` is the data type of the `acct` variable. 

The account referenced by the `acct` variable is empty because we haven't populated any of its fields yet. There are two ways to add fields: through the constructor or by using dot notation. 

The fastest way to add fields is to specify them as name-value pairs inside the constructor. For example, this statement creates a new account sObject and populates its Name field with a string value

```java
Account acct = new Account(Name='Acme');
```

The Name field is the only required field for Accounts, which means that it has to be populated before being able to insert a new record. 

Alternatively, you can use the dot notation to add fields to an sObject. 

```java
acct.Phone = '(415)555-1212';
acct.NumberOfEmployees = 100;
```

### Working with the Generic sObject Data Type
Typically, you use the specific sObject data type, such as Account for a standard object or Book__c for a custom object called Book, when working with sObjects. However, when you don't know the type of sObject your method is handling, you can use the generic sObject data type. 

Variables that are declared with the generic sObject data type can reference any Salesforce record, whether it is a standard or custom object record. 

This example shows how the generic sObject variable can be assigned to any Salesforce object: an account and a custom object called Book__c. 

```java
sObject sobj1 = new Account(Name='Trailhead');
sObject sobj2 = new Book__c(Name='Workbook 1');
```

In contrast, variables that are declared with the specific sObject data type can reference only the Salesforce records of the same type. 

### Casting Generic sObjects to Specific sObject Types
When you're dealing with generic sObjects, you sometimes need to cast your sObject variable to a specific sObject type. One of the benefits of doing so is tobe able to access fields using dot notation, which is not available on the generic sObject. Since sObject is a parent type for all specific sObject types, you can cast a generic sObject to a specific sObject. 

```java
Account acct = (Account)myGenericSObject;
String name = acct.Name;
String phone = acct.Phone;
```

Unlike specific sObject types, generic sObjects can be created only through the `newSObject()` method. Also, the fields of a generic sObject can be accessed only through the `put()` and `get()` methods. 