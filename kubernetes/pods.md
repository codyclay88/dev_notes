### What is a Pod?

A Pod is the basic execution unit of a Kubernetes application - the smallest and simplest unit in the Kubernetes object model that you create or deploy. 
Pods run containers, potentially more than one container. 
As a developer, you can think of Pods as a way to organize the different parts of your application. 
A Pod can have an IP Address, memory, volumes, etc, all shared across the containers it hosts. 
We can scale Pods horizontally. 

Pods are spun up inside of a Worker Node within the Kubernetes cluster. Each Pod within the Worker Node has a separate IP Address. 

### How do we create Pods?

There are several different methods:
1. kubectl run command
   1. ex. `kubectl run [podname] --image nginx:alpine`
   2. This is a quick and easy way, and is considered to be the "imperative approach". 
   3. Behind the scenes this creates a "deployment"
2. kubectl create/apply command with a yaml file
   1. ex. `kubectl create -f file.pod.yml --dry-run --validate=true`
      1. This performs a "trial create", which doesn't actually create the resources, but gives you an idea about what it would look like if you did.
      2. The `--validate=true` is on by default, which tells kubectl to validate the YAML file. 
      3. The `create` command will error if a Pod already exists that was specified in the YAML file. In this scenario you can use the `kubectl apply` command.
   2. ex. `kubectl apply -f file.pod.yml`
      1. This creates the resources as specified by the YAML or updates the configuration if some of the objects already exist. 

### Pod Networking
By default, when a Pod is brought to life it will release a "Cluster IP Address". This IP Address is only exposed to Nodes and Pods within a given cluster. 
In order for a Pod container to be called externally, you can use the following command:
```
kubectl port-forward [podname] 8080:80
# In the above command, 8080 is the external port, and 80 is the internal container port. 
```

### Useful Commands:
1. `kubectl get pods`: Can be used to retreive information about a particular pod
2. `kubectl delete pod [podname]`: Can be used to delete a pod
   1. While this will delete the pod, a new pod will just be recreated in its place, as defined by the deployment. In this scenario, you can delete the Pod by finding the deployment and using the `kubectl delete deployment` command

### Pod Health

A "Probe" is a diagnostic performed by the kubelet on a container. 

There are two types of Probes:
1. Liveness Probe: used to determine if a Pod is healthy and running as expected
2. Readiness Probe: used to determine if a Pod should receive requests

Failed Pod containers are restarted by default (restartPolicy defaults to Always)

There are a couple of "Actions" that a probe can perform:
1. ExecAction: Executes an action inside the container
2. TCPSocketAction: TCP check against the container's IP Address on a specified port
3. HttpGetAction: HTTP GET Request against the container 

Probe Actions can return the following results:
1. Success
2. Failure
3. Unknown