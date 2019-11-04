Salesforce provides several Declarative Automation tools, such as Process Builder and Workflow Builder, but this section will focus mostly on Flow Builder. 

Workflow 
- The Good
  - Easy to use
  - Mature
  - Approval processes are a first cousin
  - Automate many business processes
- The Bad
  - Four actions only, cannot create or delete records
  - Invoked only when record changes
  - No control over execution order and supports only one IF/THEN statement
  - Limited span - only access some fields on a related master object

Process Builder
- The Good
  - Far more actions than Workflow, and supports versioning
  - Control order of execution and supports multiple IF/THEN statements
  - Invoked by record changes, another process, or a platform event
  - Update any related object record
- The Bad
  - Creates records, but you can't grab the ID of the newly created record
  - Does not support user interaction
  - Cannot delete records
  - Scheduled actions cannot as yet be tracked back to the record

Flow Builder
- The Good
  - Supports user interaction
  - Invoked by custom buttons, links, or processes
  - Creates, updates, or deletes ANY records
  - Can call other flows
- The Bad
  - Complexity, Coders code, and Admins run away
  - Learning curve is long

Flow Builder can do all the actions of WF/PB do, and more

### Flow Terms
- Lightning Flow: Includes tools for building, managing, and running flows and processes. Process Builder and Flow share the same code base
- Flow Builder: Point-and-click tool for building Flows
- Flow: an application that automates business processes by collecting data and doing something in Salesforce or external system

### Parts of a Flow
- Elements: Screens, logic, record actions dragged to the canvas
- Connectors: Connect elements and set the logic order
- Resources: Containers for values, such as object fields or record IDs

### Two Main Types of Flows
- Screen Flow: User interaction, launch with buttons, links
- Autolaunched: Launched by processes

### Top Use Cases for Flows
- Deleting Records
- User Interaction
- Fast Access of Multiple Records
- Multiple Object Access

### Distributing Flows
- Custom Button and Links: allows us to pass in information to the Flow
- Processes: a flow can be invoked from Process Builder
- Lightning Pages: we can use a standard flow component in a Lightning Page

There are a couple "Gotchas"
- All distributions depend on the "Run Flows" permission
- Only admins with the "Manage Flow" can run inactive flows
- Ensure Lightning runtime is enabled
