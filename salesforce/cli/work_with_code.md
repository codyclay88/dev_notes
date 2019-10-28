### What Are We Building?
We'll build our feature - our geolocation app - by writing code locally, and then synchronizing it to our scratch org, where we can test it. We'll work our way through Apex and several Aura components. 

### Create the Account Search Apex Controller
In this step, you create an Apex controller that lets your Aura component retrieve a list of accounts with their location informaion. Apex classes are stored in a folder called `classes` in `force-app/main/default`. You can use the CLI to quickly scaffold a new Apex class. 
1. From within the geolocation project directory, run this command from the root of your project.
   ```
   sfdx force:apex:class:create -n AccountSearchController -d force-app/main/default/classes
   ```
2. Open `AccountSearchController.cls` and replace the scaffold code with the following:
   ```java
    public with sharing class AccountSearchController {
        @AuraEnabled
        public static List<Account> searchAccounts( String searchTerm ) {
            List<Account> accounts = new List<Account>();
            if ( String.isNotBlank( searchTerm ) ) {
                List<List<SObject>> searchResults = [
                    FIND :searchTerm
                    RETURNING Account(
                        Id, Name, Phone, Website,
                        BillingStreet, BillingCity,
                        BillingState, BillingPostalCode
                        ORDER BY Name
                        LIMIT 10
                    )
                ];
                accounts = searchResults[0];
            }
            return accounts;
        }
    }
   ```
3. Now push (synchronize) your new code to the scratch org.
   ```
   sfdx force:source:push
   ```
   Your scratch org is now updated with the AccountSearchController!

### Create the Accounts Loaded Aura Event
Using the single responsibility principle, we delegate the display of the search results by firing an event that is handled by the Account Map and Account List components we will create in later steps. 

1. Create the even in the aura folder
   ```
   sfdx force:lightning:event:create -n AccountsLoaded -d force-app/main/default/aura
   ```
2. Open `AccountsLoaded.evt` and replace the contents with this code.
   ```
   <aura:event type="APPLICATION">
    <aura:attribute name="accounts" type="Account[]">
   </aura:event>
   ```
3. Push your new code to the scratch org
   ```
   sfdx force:source:push
   ```

### Create the Account Search Aura Component
1. Create the component in the aura folder
   ```
   sfdx force:lightning:component:create -n AccountSearch -d force-app/main/default/aura
   ```
2. Open `AccountSearch.cmp` and replace its contents with the following code.
   ```xml
    <aura:component controller="AccountSearchController">
        <aura:registerEvent name="accountsLoaded" type="c:AccountsLoaded"/>
        <aura:handler name="init" value="{!this}" action="{!c.onInit}"/>
        <aura:attribute name="searchTerm" type="String" default="San Francisco"/>
        <lightning:card title="Account Search" iconName="standard:search">
            <div class="slds-form slds-p-around_x-small">
                <lightning:input
                    label="Search"
                    variant="label-hidden"
                    value="{!v.searchTerm}"
                    placeholder="Search by name, phone, website, or address"
                    onchange="{!c.onSearchTermChange}"/>
            </div>
        </lightning:card>
    </aura:component>
   ```
   This component has an input field for the user to enter search terms, such as account name or address, and registers event handlers when the component is initialized or the search term changes. 
3. Open `AccountSearchController.js` and replace its contents with the following code.
   ```javascript
    ({
        onInit: function( component, event, helper ) {
            // proactively search on component initialization
            var searchTerm = component.get( "v.searchTerm" );
            helper.handleSearch( component, searchTerm );
        },
        onSearchTermChange: function( component, event, helper ) {
            // search anytime the term changes in the input field
            var searchTerm = component.get( "v.searchTerm" );
            helper.handleSearch( component, searchTerm );
        }
    })
   ```
   The client-side controller handles the component initialization event and when the search term changes. It calls the helper files to perform the search based on the user's input. 
4. Open `AccountSearchHelper.js` and replace its contents with the following code.
   ```javascript
    ({
        // code in the helper is reusable by both
        // the controller.js and helper.js files
        handleSearch: function( component, searchTerm ) {
            var action = component.get( "c.searchAccounts" );
            action.setParams({
                searchTerm: searchTerm
            });
            action.setCallback( this, function( response ) {
                var event = $A.get( "e.c:AccountsLoaded" );
                event.setParams({
                    "accounts": response.getReturnValue()
                });
                event.fire();
            });
            $A.enqueueAction( action );
        }
    })
   ``` 
5. Push the new source to the scratch org:
    ```
    sfdx force:source:push
    ```

### Create the Account List Aura Component
Next, we create an Aura component that displays accounts in a data table. To know what data to show, it subscribes to the `c:AccountsLoaded` event that we created in the previous step.
1. Create the component in the aura folder
   ```
   sfdx force:lightning:component:create -n AccountList -d force-app/main/default/aura
   ```
2. Open `AccountList.cmp` and replace its contents with the following code.
   ```xml
    <aura:component>
        <aura:handler event="c:AccountsLoaded" action="{!c.onAccountsLoaded}"/>
        <lightning:navigation aura:id="navigation"/>
        <aura:attribute name="rows" type="Map[]"/>
        <aura:attribute name="cols" type="Map[]"/>
        <lightning:card title="Account List" iconName="standard:account">
            <lightning:datatable
                data="{!v.rows}"
                columns="{!v.cols}"
                keyField="Id"
                hideCheckboxColumn="true"
                showRowNumberColumn="true"
                onrowaction="{!c.onRowAction}"/>
        </lightning:card>
    </aura:component>
   ```
3. Open `AccountListController.js` and replace its contents with the following
   ```javascript
    ({
        onAccountsLoaded: function( component, event, helper ) {
            var cols = [
                {
                    'label': 'Name',
                    'fieldName': 'Name',
                    'type': 'text'
                },
                {
                    'label': 'Phone',
                    'fieldName': 'Phone',
                    'type': 'phone'
                },
                {
                    'label': 'Website',
                    'fieldName': 'Website',
                    'type': 'url'
                },
                {
                    'label': 'Action',
                    'type': 'button',
                    'typeAttributes': {
                        'label': 'View details',
                        'name': 'view_details'
                    }
                }
            ];
            component.set( 'v.cols', cols );
            component.set( 'v.rows', event.getParam( 'accounts' ) );
        },
        onRowAction: function( component, event, helper ) {
            var action = event.getParam( 'action' );
            var row = event.getParam( 'row' );
            if ( action.name == 'view_details' ) {
                var navigation = component.find( 'navigation' );
                navigation.navigate({
                    'type': 'standard__recordPage',
                    'attributes': {
                        'objectApiName': 'Account',
                        'recordId': row.Id,
                        'actionName': 'view'
                    }
                });
            }
        }
    })
   ```
   The client-side controller's `onAccountLoaded` function transforms the event data into the format expected by the `<lightning:datatable>` component. The `onRowAction` function navigates to the account record of the row the user interacted with.
4. Push the new code to the scratch org
   ```
   sfdx force:source:push
   ```

Now lets build the final component of our geolocation app, which brings all the features together. We'll create an Aura component that visualizes account addresses as markers on a map, and create a custom tab so we can navigate to our app. We'll use a permission set to grant users access to our app, too. 

### Create the Account Map Aura Component
1. Create the `AccountMap` component in the aura folder.
   ```sfdx force:lightning:component:create -n AccountMap -d force-app/main/default/aura
   ```
2. Open `AccountMap.cmp` and replace its contents with the following
   ```xml
    <aura:component>
        <aura:handler event="c:AccountsLoaded" action="{!c.onAccountsLoaded}"/>
        <aura:attribute name="mapMarkers" type="Map[]"/>
        <lightning:card title="Account Map" iconName="action:map">
            <lightning:map mapMarkers="{!v.mapMarkers}"/>
        </lightning:card>
    </aura:component>
   ```
   This component listens for the AccountsLoaded event and displays the event data as map markers. 
3. Open `AccountMapController.js` and replace its contents with the following code
  ```javascript
    ({
        onAccountsLoaded: function( component, event, helper ) {
            var mapMarkers = [];
            var accounts = event.getParam( 'accounts' );
            for ( var i = 0; i < accounts.length; i++ ) {
                var account = accounts[i];
                var marker = {
                    'location': {
                        'Street': account.BillingStreet,
                        'City': account.BillingCity,
                        'PostalCode': account.BillingPostalCode
                    },
                    'title': account.Name,
                    'description': (
                        'Phone: ' + account.Phone +
                        '<br/>' +
                        'Website: ' + account.Website
                    ),
                    'icon': 'standard:location'
                };
                mapMarkers.push( marker );
            }
            component.set( 'v.mapMarkers', mapMarkers );
        }
    })
  ``` 
  The client-side controller's `onAccountsLoaded` function transforms the event data into the format expected by the `<lightning:map>` component.
4. Push your new changes to the scratch org
   ```
   sfdx force:source:push
   ```

### Create the Account Locator Aura Component
This is our main component, which we use to display our app to end users in Lightning pages, Salesforce mobile, and custom tabs. It's also the last component we create for this project!

1. Create the `AccountLocator` component in the aura folder
   ```
   sfdx force:lightning:component:create -n AccountLocator -d force-app/main/default/aura
   ```
2. Open `AccountLocator.cmp` component in the aura folder and replace with the following contents
   ```xml
    <aura:component implements="force:appHostable,flexipage:availableForAllPageTypes">
        <lightning:layout horizontalAlign="space" multipleRows="true">
            <lightning:layoutItem size="12"
                                mediumDeviceSize="12"
                                padding="around-small">
                <c:AccountSearch/>
            </lightning:layoutItem>
            <lightning:layoutItem size="12"
                                mediumDeviceSize="6"
                                padding="around-small">
                <c:AccountList/>
            </lightning:layoutItem>
            <lightning:layoutItem size="12"
                                mediumDeviceSize="6"
                                padding="around-small">
                <c:AccountMap/>
            </lightning:layoutItem>
        </lightning:layout>
    </aura:component>
   ```
   This component is comprised of the other components you created throughout this project. Two powerful features of the Lightning Component Framework are component encapsulation and component reusability. Composing fine-grained components in a larger component enables you to build more interesting components and applications.
3. Push your changes to the scratch org
   ```
   sfdx force:source:push
   ```

### Create the Account Locator Custom Tab
An easy way to display Aura components in both Lightning Experience desktop and Salesforce mobile is with a custom tab. After this step, you access your app by navigating to this custom tab. 
1. Open your scratch org
   ```
   sfdx force:org:open
   ```
2. From Setup, enter `Tabs` in the Quick Find box, then click Tabs. 
3. In the Lightning Component Tabs section, click **New** and enter the details:
    - Lightning Component: **c.AccountLocator**
    - Tab Label: **Account Locator**
    - Tab Name: **Account_Locator**
    - Tab Style: Click the magnifier icon and select **Map** as the tab icon
4. Click **Next**
5. For Apply one tab visibility to all profiles, choose **Tab Hidden**
6. Click **Save**

### Create the Geolocation Permission Set
To control who has access to your geolocation app, create a permission set and grant it visibility to the Account Locator tab. 
1. From Setup, enter `Permission Sets` in the Quick Find box, then click **Permission Sets**.
2. Click **New** and enter the details
   - Label: **Geolocation**
   - API Name: **Geolocation**
   - Description **Grants access to the Account Geolocation API**
3. Click **Save**
4. Under Apps, click **Object Settings**
5. Click **Account Locator**
6. Click **Edit**
7. For Tab Settings, select both **Available** and **Visible**, then click **Save**

Next, assign the permission set to yourself. We'll see in a later step how to automate assigning permission sets using Salesforce CLI. 
1. Click **Manage Assignments**, then **Add Assignments**
2. Select the checkbox next to the scratch org username (User, User), click **Assign**, then click **Done**

### Metadata Magic: Pull Changes into Your Project
Until recently, you've been working locally and pushing metadata into your scratch org. In this step, you made some changes directly in that scratch org. Now the magic happens. With one single command, you pull all the metadata that you changed in the scratch org into your local project. 

Before we do that tough, we want to configure our .forceignore file to ignore syncing some metadata. When you created the Account Locator tab and assigned its visibility to all the profiles, the automatic change-tracking in the scratch org noted that the profiles have changed. Naturally, the next time you pull metadata from the scratch org to sync with your local project, the CLI will want to pull down profile metadata. Since the profiles are not pertnent to your geolocation app and not something to track in your source control repository for this project, we need to tell Salesforce CLI to ignore the profile changes. 

In your geolocation project directory, open the .forceignore file and add `**/profiles` as a new line to the file, then save the file. 

Now you're ready to sync metadata. In the command window, sync the changes you made in the scratch org with your local project.
```
sfdx force:source:pull
```

### Validate your App
While you can absolutely use the same scratch org you've used during development to perform your testing, we recommend that you always start with a fresh scratch org. A fresh scratch org ensure you've properly externalized all your source from the org. 
1. Create a new scratch org
   ```
   sfdx force:org:create -f config/project-scratch-def.json -a GeoTestOrg
   ```
2. Push your local source and metadata to the new scratch org.
   ```
   sfdx force:source:push -u GeoTestOrg
   ```
3. Assign your permission set
   ```
   sfdx force:user:permset:assign -n Geolocation -u GeoTestOrg
   ```
4. Load your sample data into the org
   ```
   sfdx force:data:tree:import -f data/Account.json -u GeoTestOrg
   ```
5. Open your org
   ```
   sfdx force:org:open -u GeoTestOrg
   ```
6. Test the Account Locator tab, and verify that it works as expected. Click the **App Launcher**, then **Account Locator**

Congrats, you've successfully built and tested a new app with scratch orgs and Salesforce CLI. Don't forget to add everything to your source control repository.