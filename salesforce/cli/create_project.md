### Create a Salesforce DX Project
A Salesforce DX project is a local copy of your package metadata, a group of related code and customizations. It also contains the core assets required to synchronize local project course and metadata with your scratch orgs. Create the project on the same machine where you installed Salesforce CLI, then sync this project with your VCS repository. 

In this module, we create a simple geolocation application with Aura Components. 

1. In a command window, navigate to where you want your project located.
2. Create the project:
   ```
   sfdx force:project:create -n geolocation
   ```
   This command creates a folder called geolocation, and scaffolds a new project with all assets in the proper folder structure. 

### Create a Scratch Org
Now that you understand the power of Scratch orgs, here's the basic workflow of when you use them in the Package Deployment Model.
1. Push your local source and metadata to a scratch org
2. Pull any changes you make in the scratch org back to your local project
3. Sync this project with your source control repo

The first step is to create a scratch org. 
1. In the command window, change to the geolocation project directory:
   ```
   cd geolocation
   ```
2. Create a scratch org with the alias GeoAppScratch
   ```
   sfdx force:org:create -s -f config/project-scratch-def.json -a GeoAppScratch
   ```
   We used these options when launching the command:
   - The `s` option indicates that you want this scratch org to be the default org for this project when running Salesforce CLI commands. To use a different org on a per command basis, you can specify the `-u` argument and specify another alias. 
   - The `-f` option is the path to the proejct scratch org configuration file. 

### Create Sample Data
Scratch orgs come with some standard data based on the edition you choose. However, its important to ad some sample data that's more relevant to the app or package you're building. We can use the Salesforce CLI to create some sample accounts to test with our geolocation app.
Examples:
```
sfdx force:data:record:create -s Account -v "Name='Marriott Marquis' BillingStreet='780 Mission St' BillingCity='San Francisco' BillingState='CA' BillingPostalCode='94103' Phone='(415) 896-1600' Website='www.marriott.com'"

sfdx force:data:record:create -s Account -v "Name='Hilton Union Square' BillingStreet='333 O Farrell St' BillingCity='San Francisco' BillingState='CA' BillingPostalCode='94102' Phone='(415) 771-1400' Website='www.hilton.com'"

sfdx force:data:record:create -s Account -v "Name='Hyatt' BillingStreet='5 Embarcadero Center' BillingCity='San Francisco' BillingState='CA' BillingPostalCode='94111' Phone='(415) 788-1234' Website='www.hyatt.com'"
```

Salesforce also provides commands to easily grab data from the scratch org and pull it into your project. You can then commit that data to your source control repository, so you can reload it if you, or another developer, spins up a new scratch org. 

Whatever source control system you use, Salesforce recommends that you configure it to exclude the .sfdx folder from being added to the repository. This folder folds temporary information for your scratch orgs, so you don't have to save it for posterity. 

In your Salesforce DX geolocation project, create a directory called `data`:
```
mkdir data
```

Export some sample data:
```
sfdx force:data:tree:export -q "SELECT Name, BillingStreet, BillingCity, BillingState, BillingPostalCode, Phone, Website FROM Account WHERE BillingStreet != NULL AND BillingCity != NULL and BillingState != NULL" -d ./data
```

This provides you sample data that you can import in the future with the command:
```
sfdx force:data:tree:import --sobjecttreefiles data/Account.json
```