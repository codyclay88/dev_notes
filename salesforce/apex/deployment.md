### Deploying Code

*What is a deployment?*
A process that copies components from one org to another. Typically from a Sandbox Org to a Production Org. However, you can also deploy code from Production to Sandbox.

You can deploy code, Custom Objects and Fields, Validation Rules, Page Layouts, Dashboards and Reports, and Workflow Rules.

Deployment Best Practices:
- Each developer will typically have their own Sandbox
- Once a developer has finished their code and passed all their test classes, they deploy their code to a "Full Sandbox" org, which contains the same data that the production environment has. 
- Ideally, a QA person will ensure everything is working correctly in the Full Sandbox \
- After everything has been retested in the Full Sandbox environment, code will be deployed to Production. 

*What happens during deployment?*
1. Choose the components to be deployed
2. Salesforce will check for any missing dependencies
3. Salesforce will run all Apex test classes
4. Salesforce will return your results

*How do we deploy*
There are multiple options to deploy our code. 
1. Change Sets: basically simple Point and Click wizards for deploying code. Can be very slow however.
2. Coding Editor: editors have built-in tools that make it easier to deploy code. 
3. Programmatic: Ant Migration tool, SOAP API, allows users to automate their Salesforce deployments

