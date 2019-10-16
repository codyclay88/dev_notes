
Create a database
```sql
CREATE DATABASE SampleDb
```

Rename a database
```sql
ALTER DATABASE SampleDb MODIFY Name = AwesomeDb

# Using a system stored procedure
EXECUTE sp_renameDB 'SampleDb', 'AwesomeDb'
```

Drop a database 

```sql
# Note that you can only drop a database that is not in use
DROP DATABASE SampleDb

# If database is currently in use
ALTER DATABASE SampleDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE
# now drop the database
DROP DATABASE SampleDb

# This will roll back any incomplete transactions and close the connection to the database
```

Create a table 
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

Create foreign key constraint between two tables
```sql
# Here we are marking the 'GenderID' column on the 'tblPerson' table as a Foreign Key to the `ID` column on the `tblGender` table
ALTER TABLE tblPerson 
ADD CONSTRAINT tblPerson_GenderID_FK 
FOREIGN KEY (GenderID) REFERENCES tblGender (ID) 
```

Create default constraint on column
```sql
# Here we are saying that if no GenderID is specified, default to the value of 3
ALTER TABLE tblPerson 
ADD CONSTRAINT DF_tblPerson_GenderID
DEFAULT 3 FOR GenderID
```

Drop constraint
```sql
ALTER TABLE tblPerson
DROP CONSTRAINT DF_tblPerson_GenderID
```

Insert row into table
```sql
INSERT INTO tblPerson (ID, Name, Email) 
Values (7, 'Cody', 'codyclay88@gmail.com')
```
