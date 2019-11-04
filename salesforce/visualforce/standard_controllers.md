### Visualforce Standard Controller
Visualforce uses the traditional MVC paradigm, and includes sophisticated built-in controllers to handle standard actions and data access, providing simple and tight integration with the Lightning Platform database. 

The MVC design pattern makes it easy to separate the view and its styling from the underlying database and logic. In MVC, the view (the Visualforce page) interacts with a controller, and the controller provides the functionality for the page. For example, the controller can contain the logic to be executed when a button is clicked. A controller also typically interacts with the model (the database)-- making available data that the view might want to display, or pushing changes back to the database. 

Most standard and all custom objects have standard controllers that can be used to interact with the data associated the object, so you don't need to write the code for the controller yourself. You can extend the standard controllers to add new functionality, or create custom controllers from scratch. 

