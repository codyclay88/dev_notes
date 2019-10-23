
# Database-Level Operations

### Create a database
```sql
CREATE DATABASE SampleDb
```

### Rename a database
```sql
ALTER DATABASE SampleDb MODIFY Name = AwesomeDb

# Using a system stored procedure
EXECUTE sp_renameDB 'SampleDb', 'AwesomeDb'
```

### Drop a database 

```sql
# Note that you can only drop a database that is not in use
DROP DATABASE SampleDb

# If database is currently in use
ALTER DATABASE SampleDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE
# now drop the database
DROP DATABASE SampleDb

# This will roll back any incomplete transactions and close the connection to the database
```

# Table-Level Operations
### Create a table 
```sql
# Ensure that we are using the correct database
USE SampleDb

CREATE TABLE tblPerson 
(
    ID int NOT NULL PRIMARY KEY,
    Name nvarchar(50) NOT NULL,
    Email nvarchar(50) NOT NULL,
    GenderID int,
)

CREATE TABLE tblGender
(
    # Create ID column as primary key
    ID int NOT NULL PRIMARY KEY,
    Gender nvarchar(50) NOT NULL,
)
```

### Create foreign key constraint between two tables
```sql
# Here we are marking the 'GenderID' column on the 'tblPerson' table as a Foreign Key to the `ID` column on the `tblGender` table
ALTER TABLE tblPerson 
ADD CONSTRAINT tblPerson_GenderID_FK 
FOREIGN KEY (GenderID) REFERENCES tblGender (ID) 
```

### Create default constraint on column
```sql
# Here we are saying that if no GenderID is specified, default to the value of 3
ALTER TABLE tblPerson 
ADD CONSTRAINT DF_tblPerson_GenderID
DEFAULT 3 FOR GenderID
```

### Add Column to existing table
```sql
ALTER TABLE tblPerson
ADD Age int NULL
```

### Drop constraint
```sql
ALTER TABLE tblPerson
DROP CONSTRAINT DF_tblPerson_GenderID
```


### Create Cascading Referential Integrity constraint

There are several Cascading Referential Integrity Constraints that we can use
- The default behavior when deleting data is "No Action", meaning that if an attempt is made to delete or update a row with a key referenced by foreign keys in existing rows in other tables, an error is raised and the DELETE or UPDATE is rolled back.

- A second option is to "Cascade", which specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys in other tables, all rows containing those foreign keys are also deleted or updated

- A third option is to "Set Null", meaning that if an attempt is made to delete or update a row with a key referenced by foreign keys in existing rows in other tables, all rows containing those foreign keys are set to NULL

- The final option is to "Set Default", which specifies that if an attempt is made to delete or update a row with a key referenced by foreign keys in existing rows or other tables, all rows containing those foreign keys are set to default values

### Create Check Constraint
Check constraints can be used to limit the range of the values that can be entered for a column.
```sql

``` 

### Insert row into table
```sql
INSERT INTO tblPerson (ID, Name, Email) 
Values (7, 'Cody', 'codyclay88@gmail.com')
```
