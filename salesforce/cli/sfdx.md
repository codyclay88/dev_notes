### Get Started with Source-Driven Development

Salesforce DX adds new tools that streamline the entire development life cycle. It improves team development and collaboration, facilitates automated testing and continuous integration, and makes the release cycle more efficient and agile. 

But Salesforce DX is so much more than just a new set of tools! It provides an alternative to change set development, and shifts the source of truth from the org to your version control system. It shifts your development focus from org development to package development. 

### What's a Scratch Org?
Many of the new tools enable you to use a new type of org called a scratch org. A scratch org is a dedicated, configurable, and short-term Salesforce environment. Scratch orgs drive developer productivity and collaboration during the development process, and facilitate automated testing and continuous integration. You can use the CLI or Salesforce Extensions for VS Code to open your scratch org in a browser without logging in. You can spin up a new scratch org when you want to:
- Start a new project
- Start a new feature branch
- Test a new feature
- Start automated testing
- Perform development tasks directly in an org
- Start from "scratch" with a fresh new org

Although scratch orgs are meant to be disposable, the scratch org configuration files contain the real brawn. Through the configuration file, you can configure the scratch org with different Salesforce editions and with just the features and settings you want. And you can share the scratch org configuration file with other team members. 

### Do Scratch Orgs Replace Sandboxes?
No. Scratch orgs aren't meant to be replications of sandboxes or production orgs. Due to their ephemeral nature (and maximum 30-day lifespan), scratch orgs are perfect for developing a new feature or customization or package. ANd they work great to unit testing and continuous integration. Sandboxes, which contain all the metadata of your production org, are still necessary for final user-acceptance testing, continuous delivery, and staging. 

### Enable Dev Hub
A Dev Hub provides you and your team with the ability to create and manage scratch orgs. Scratch orgs are temporary Salesforce environments where you do the bulk of your development work in this new source-driven development paradigm. 

To get started with scratch orgs, you choose an org to function as your Dev Hub. While you can enable Dev Hub in any paid org, it's always best to practice somewhere other than production. Instead, go ahead and enable Dev Hub in a Developer Edition org or Trailhead Playground to use with this module. 

### Log In to the Dev Hub 
To get started, log in to the Dev Hub using the CLI, so you're authorized to create scratch orgs. You can use `sfdx force:auth:web:login` to log in to various orgs, and we've provided some options to help you manage those orgs. 
1. To authorize the Dev Hub, use the web login flow:
```
sfdx force:auth:web:login -d -a DevHub
```
Adding the `-d` flag sets this org as the default Dev Hub. Use the `-a` to set an alias for the org (something catchy like DevHub). An alias is much easier to remember than the unique Dev Hub username. 

1. Log in with your credentials. Once successful, the CLI securely stores the token along with the alias for the org, in this example, DevHub. You can close the Dev Hub org at any time. 

You can close the Dev Hub and still create scratch orgs. However, if you want to open the Dev Hub org to look at active scratch orgs or your namespace registry, the alias comes in quite handy:

```
sfdx force:org:open -u DevHub
```

### A Bit More on Org Management
It's likely you have many orgs, including sandboxes and your production org. With the CLI, you can also log in to them using these commands. When you log in to an org using the CLI, you add that org to the list of orgs that the CLI can work with in the future. 

### Log In to Sandboxes
If you create an alias for the sandbox (`-a` option), you can reference it by this alias instead of this long and often unintuitive username. For example:
```
sfdx force:auth:web:login -r https://test.salesforce.com -a FullSandbox
sfdx force:auth:web:login -r https://test.salesforce.com -a DevSandbox
```

### The Power of Aliasing
As you might imagine, aliasing is a powerful way to manage and track your orgs, and we consider it a best practice. Why? Let's look at scratch org usernames as an example. A scratch org username looks something like `test-7emanxeksl@example.com`. Not easy to remember. So when you issue a command that requires the org username, using an alias for the org that you can remember can speed things up. 
```
sfdx force:org:open -u FullSandbox
sfdx force:org:open -u MyScratchOrg
sfdx force:limits:api:display -y DevSandbox
```

### View All Orgs
At any point, you can run the command `sfdx force:org:list` to see all the orgs you've logged in to. Adding the `--verbose` option provides you even more info. 