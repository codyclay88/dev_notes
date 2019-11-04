### Introduction to Global Variables and Visualforce Expressions
Visualforce pages can display data retrieved from the database or web services, data that changes depending on who is logged on and viewing the page, and so on. This dynamic data is accessed in markup through the use of global variables, calculations, and properties made available by the page's controller. Together, these are described generally as Visualforce Expressions. Use expressions for dynamic output or passing values into components by assigning them to attributes. 

A Visualforce expression is any set of literal values, variables, sub-expressions, or operators that can be resolved to a single value. Method calls aren't allows in expressions. 

The expression syntax in Visualforce is `{! expression }`

Anything inside the `{! }` delimiters is evaluated and dynamically replaced when the page is rendered or when the value is used. Whitespace is ignored. 

The resulting value can be a primitive (integer, string, and so on), a boolean, an sObject, a controller method such as an action method, and other useful results. 

### Global Variables
Use global variables to access and display system values and resources in your Visualforce markup. 

For example, Visualforce provides information about the logged-in user in a global variable called `$User`. You can access fields of the `$User` global variable (and any others) using an expression with the following form `{! $GlobalName.fieldName }`.

### Formula Expressions
Visualforce lets you use more than just global variables in the expression language. It also supports formulas that let you manipulate values. 

For example, the `&` character is the formula language operator that concatenates strings.

### Conditional Expressions
Use a **conditional expression** to display different information based on the value of the expression. 

