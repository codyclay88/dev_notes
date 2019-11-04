### About Visualforce

Visualforce is a web development framework that enables developers to build sophisticated, custom user interfaces for mobile and desktop apps that can be hosted in the Lightning Platform. You can use Visualforce to build apps that align with the styling of Lightning Experience, as well as your own completely custom interface. 

Visualforce enables developers to extend Salesforce's built-in features, replace them with new functionality, and build completely new apps. Use powerful built-in standard controller features, or write your own custom business logic in Apex. You can build functionality for your own organization, or create apps for sale in AppExchange. 

Visualforce app development is familiar to anyone who has built web apps. Developers create Visualforce pages by composing components, HTML, and optional styling elements. Visualforce can integrate with any standard web technology or JavaScript framework to allow for a more animated and rich user interface. Each page is accessible by a unique URL. When someone accesses a page the server performs any data processing required by the page, renders the page into HTML, and returns the results to the browser for display. 

Here's a short example of a working Visualforce page:
```xml
<apex:page standardController="Contact">
    <apex:form>
        <apex:pageBlock title="Edit Contact">
            <apex:pageBlockSection columns="1">
                <apex:inputField value="{!Contact.FirstName}">
                <apex:inputField value="{!Contact.LastName}">
                <apex:inputField value="{!Contact.Email}">
                <apex:inputField value="{!Contact.Birthdate}">
            <apex:pageBlockSection>
        </apex:pageBlock>
        <apex:pageBlockButtons>
            <apex:commandButton action="{!save}" value="Save" />
        </apex:pageBlockButtons>
    </apex:form>
</apex:page>
```

In just over a dozen lines of code, this page does a lot. 
- It connects to the Visualforce standard controller, a part of the Visualforce framework that provides automatic data access and modification, standard actions, and more
- When accessed without a record ID, the page displays a blank data entry form. When you click **Save**, a new record is created from the form data.
- When accessed with a new record ID, the page looks up the data for that contact record and displays it in an editable form. When you click **Save**, your changes for the contact are saved back to the database
- Each input field is smart about how it presents it value.
  - The email field knows what a valid email address looks like, and displays an error if an invalid email is entered
  - The date field displays a date widget when you click into the field to make entering a date easier
- The **Save** button calls the `save` action method, one of a number of standard actions provided by the standard controller. 

### Where You Can Use Visualforce
Salesforce provides a range of ways that you can use Visualforce within your organization. You can extend Salesforce's built-in features, replace them with new functionality, and build completely new apps. 

#### Open a Visualforce Page from the App Launcher
Your Visualforce apps and custom tabs are all available from the App Launcher.

#### Add a Visualforce Page to the Navigation Bar
As described in the preceding example, you can add Visualforce tabs to an app and they display as items in the app's navigation bar. 

#### Display a Visualforce Page within a Standard Page Layout
Extend your page layouts by embedding Visualforce pages on them to display completely custom content on a standard page. 

#### Add a Visualforce Page as a Component in the Lightning App Builder
When you create a custom app page in the Lightning App Builder, you can add a Visualforce page to the page by using the Visualforce component. 

#### Launch a Visualforce Page as a Quick Action
Although their placement in the Lightning Experience user interface is quite different from Salesforce Classic, the process of adding quick actions is much the same. Add them to the appropriate publisher area on the object's page layout. 

#### Display a Visualforce Page by Overriding Standard Buttons or Links
You can override the actions available on an object with a Visualforce page. When the user clicks a button or link that has been overridden, your page displays instead of the standard page.

#### Display a Visualforce Page using Custom Buttons or Links
You can create new actions for your objects, in the form of buttons and links, by defining them on an object. JavaScript buttons and links aren't supported in Lightning Experience, but Visualforce (and URL) items are. 