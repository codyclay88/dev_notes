## What is Branching?

Branching is a tool within a VCS that allows multiple developers at a time to separate and manage different units of work. This means that multiple developers can work on mutliple features simultaneously without worrying as much about stepping on each other's toes. 

### What is a Branching Strategy?

When working in a team of developers, it can be very helpful to define a strategy for how the developers in the team will employ branching. 

**Release Branching** - Planning over a long period of time and large number of features that are all combined together into a single release. This method follows along well with a waterfall type of approach. 

**Feature/Task Branching** - Each feature can be developed, tested, and deployed independently of another. This works well with Agile types of approaches. This typically allows developers to move much faster in their development cycle because each team can develop a feature independently of another team. 

### Sample Branching Structure
```bash
git checkout -b staging master
git checkout -b uat master
git checkout -b qa master
git checkout -b develop master
```
Above shows 4 git commands for setting up a branching structure. We are setting up different environments here. 
All of these environments are based off of `master`.
- `staging` - the branch that is closest to the master branch. 
- `uat` - the branch that will be used for *user acceptance testing*. Once the UAT has been completed, those changes will be merged into the `staging` branch. 
- `qa` - the branch that will be used for *quality assurance testing*. Once the QA process has been completed, those changes will be merged into the `uat` branch.
- `develop` - the branch that will be used as a baseline for developing new features. Each new feature will be implemented within a new branch, which will be created from the `develop` branch. Shown below we are simulating the action of creating three new feature branches, each of which are based on the `develop` branch, and because of this, each of the three branches are independent of each other moving forward. 
    ```bash
    git checkout -b task-featureA develop
    git checkout -b task-featureB develop
    git checkout -b task-featureC develop
    ```
    As soon as one of these feature branches is complete, it can be merged into the `develop` branch and moved up the development lifecycle. 