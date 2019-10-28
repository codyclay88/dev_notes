

### Understanding Force.com Objects
The Force.com platform provides a powerful database, with many features that make it quick and easy to create applications. Having dealt with SQL Server, you know that data is stored in tables and rows. The database in Force.com, on the other hand, uses objects to store data. Objects contain all the functionality you'd expect in a table, with additional enhancements that make them more powerful and versatile. Each object comprises a number of fields, which correspond to columns in a database. Data is stored in records of the object, which corresponds to rows in a database. 

There are two types of objects:
1. Standard Objects - these are objects that come baked-in with Salesforce. Some common CRM object include Accounts, Contacts, Opportunities, and Leads. 
2. Custom Objects - These are new objects you create to store information unique to your application. Custom objects extend the functionality that standard objects provide. For example, if you're building an application to track product inventory, you might create custom objects called Merchandise, Orders, or Invoices. 

Objects can also have relationship fields that define how records in one object relate to records in another object. These are essentially primary and foreign keys, but they are much more flexible, making it easier to design and implement your data model. 

Whether they're standard or custom, Force.com objects not only provide structure for storing data but also power the interface elements that allow users to interact with the data, such as tabs, the layout of fields on a page, and lists of related records. For standard functionality there's no need to implement an ORM, write a UI for CRUDding data or build tables. This standard functionality is provided by the platform automatically for you. Objects also have built-in support for features such as access management, validation, formulas, and history tracking. 

### Similar but Not the Same
SOQL is a query language very similar to SQL, but purpose-built for Salesforce. 

The first thing to know is that although they are both called query languages, SOQL is used **only** to perform queries with the SELECT statement. SOQL has no equivalent INSERT, UPDATE, and DELETE statements. In the Salesforce world, data manipulation is handled using a set of methods known as DML (Data Manipulation Language). 

Another big difference is that SOQL has no such thing as `SELECT *`. Because SOQL returns Salesforce data, and that data lives in a multi-tenanted environment where everyone is kind of "sharing the database", a wildcard character like `*` is asking for big trouble. 