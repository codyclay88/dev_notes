## Salesforce Object Query Language (SOQL)

SOQL provides you a programmatic way to access records that aren't in Trigger.new. 

This allows us to programmatically access/modify anything we want inside of Salesforce. 

SOQL also allows us to write "assertions" in our test classes, making them MUCH more useful. 

### Why is SOQL so Powerful?
Using only the declarative tools, we are limited to only modifying base records, parent records, and limited relationship records. With the power of SOQL however, we have the ability to update any record in the system from any trigger, and we can also update org level settings. 

### Where do we write SOQL queries?
- Salesforce Workbench
- Developer Console


### How to write a SOQL Query
The first word is always `SELECT`, which is then followed by a series of fields, then `FROM`, then the name of the object we are querying. 

You can filter queries using `WHERE` clauses. 

Example:
```soql
SELECT   Id, 
         FirstName, 
         Phone, 
         Title
  FROM   Contact
 WHERE   Department IN ('Executive Team'
                      'Finance'
                      'Technology')
   AND   DoNotCall = FALSE
   AND   Phone != null
   AND   Title LIKE '%VP%'
ORDER BY FirstName DESC
  LIMIT    10
```

SOQL can also take advantage of relationships between Objects. 

Example:
```soql
SELECT Id,
       Amount,
       StageName,
       Account.Name,
       Account.Industry,
       Account.Website,
       Account.Owner.Username
FROM Opportunity
WHERE Account.Industry = 'Energy'
  AND Account.AnnualRevenue > 5000
  AND CreatedBy.Email != null
```
In the scenario above, every Opportunity has a "Parent" Account. You can access the parent Account's fields by using dot notation with the `Account` field. You can even access the parent Account's parent Owner field by through `Account.Owner`. 

When using custom objects you can do the same, but the method is a little bit different
```soql
SELECT Id,
       Amount, 
       StageName,
       Sales_Plan__r.Goal__c,
       Sales_Plan__r.Sponsor__c,
       Sales_Plan__r.Qtr__r.Id,
       Account.Industry
  FROM Opportunity
 WHERE Sales_Plan__r.Target__c > 0
```
Note that relationships to custom objects are suffixed with `__r`, to denote that it is a relationship to a custom field. 

You can also query child data, but it is a little bit more involved:
```soql
SELECT Id,
       Amount,
       StageName,
       Account.Name,
       (SELECT Role, 
               Contact.FirstName,
               Contact.Email 
          FROM OpportunityContactRoles
         WHERE Role != null
           AND Role != 'Influencer')
  FROM Opportunity
```

You can also group SOQL results, as shown below:
```soql
SELECT StageName,
       SUM(Amount),
       MAX(CloseDate),
       MIN(ExpectedRevenue),
       AVG(Amount),
       COUNT(Amount)
  FROM Opportunity
 WHERE Amount != null
GROUP BY StageName
```
This query above Calculates the SUM of all Amounts for each stage of all Opportunities. In order for a `GROUP BY` query to work, you must perform a form of aggregation on at least one field in the select list, and the only fields allowed in the select lists are one that you are performing an aggregation on, with the exception of the grouped field. 