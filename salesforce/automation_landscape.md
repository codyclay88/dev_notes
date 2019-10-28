# Salesforce Automation

There are many Salesforce Automation tools, but there are two types of types:
- Declarative: Point and Click Admins
- Programmatic: Tools of the Gods

It's important to know when you should write code, and when you shouldn't. 

There are multiple Declarative tools that we can use:
- Workflow Rules
    - Same object field updates
    - Email notifications
- Process Builder
    - Expands on Workflow rules by providing related object updates, and the ability to create a record. 
- Visual Flow
    - Unrelated object updates
    - Variables and loops
    - Drag and Drop editor

The coding tools are:
- Back-end code
    - automation that happens at a database level
    - Includes triggers, classes, and SOQL
- Front-end code
    - operates at the UI level
    - Visualforce
    - Lightning

## When do you write code, and when do you not write code?
The idea is to get the job done using the least amount of complexity possible. With this in mind...

### Don't write code unless you have to!



## What is a trigger?
Salesforce, in a nutshell, is just a fancy database. A database with a UI on top and some automation tools on top. 

Salesforce is a Relational Database. Like most relational databases, salesforce provides "triggers", which is an action that automatically execute when in response to certain events.

Some of these events are:
- Insert: when a new record is created in a table
- Update: when a field in an existing record in a table is changed
- Delete: when a record is deleted

Triggers are by far the most commonly used coding tool. 

Two things are required when deploying a trigger to production:
- the trigger itself
- a test class that ensures that the trigger is running properly