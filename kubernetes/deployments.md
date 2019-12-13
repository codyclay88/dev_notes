### What is a Deployment?

To understand Deployments, it is also useful to understand "Replica Sets". 
A "Replica Set" is a declarative way to manage Pods. 

A "Deployment" is a declarative way to manage Pods using a Replica Set. Deployments are a layer on top of Replica Sets. 

Deployments and ReplicaSets ensure Pods stay running and can be used to scale Pods. 

### The role of ReplicaSets
ReplicaSets act as a Pod Controller. They provide a:
- Self-healing mechanism
- Ensure the requested number of Pods are available 
- Provide fault-tolerance
- Can be used to scale Pods 
- Relies on a Pod template
- No need to create Pods directly!

### The role of Deployments
A Deployment sits on top of ReplicaSets and provides the following benefits:
- Manages ReplicaSets, supports zero-downtime updates by creating and destroying ReplicaSets
- Provides rollback functionality
- Creates a unique label that is assigned to the ReplicaSet and generated Pods
- The YAML for a Deployment is very similar to a ReplicaSet

### Creating a Deployment
Multiple ways to create a deployment:
1. `kubectl create -f file.deployment.yml --save-config`
2. `kubectl apply -f file.deployment.yml`
   1. Creates or updates a deployment

### Get Deployments
1. `kubectl get deployments` : list all deployments
2. `kubectl get deployments --show-labels` : list all deployments and their labels
3. `kubectl get deployment -l app=nginx` : gets all information about deployments with a specific label

### Scale Pods in the Deployment
You can scape pods horizontally by updating the YAML file or by using the `kubectl scale` command.
```
kubectl scale deployment [deployment-name] --replicas=5
```