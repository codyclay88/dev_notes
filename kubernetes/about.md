### What is Kubernetes?
Kubernetes is a portable, extensible, open-source platform for managing containerized workloads and services, that facilitates both declarative configuration and automation. It has a large, rapidly growing ecosystem. Kubernetes services, support, and tools are widely available. 

Containers provide a good way to bundle and run your applications. In a production environment, you need to manage the containers than run the applications and ensure there is no downtime. 

Kubernetes provides you with:
- Service discovery and load balancing: Kubernetes can expose a container using the DNS name or using their own IP address. If traffic to a container is high, Kubernetes is able to load balance and distribute the network traffic so that the deployment is stable. 
- Storage orchestration: Kubernetes allows you to automatically mount a storage system of your choice, such as local storages, public cloud providers, and more. 
- Automated rollouts and rollbacks: You can describe the desired state for your deployed containers using Kubernetes, and it can change the actual state to the desired state at a controlled rate. For example, you can automate Kubernetes to create new containers for your deployment, remove existing containers and adopt all their resources to the new container. 
- Automatic bin packing: 

### What can Kubernetes do for you?
With modern web services, users expect applications to be available 24/7, and developers expect to deploy new versions of those applications several times a day. Containerization helps package software to serve those goals, enabling applications to be released and updated in an easy and fast way without downtime. Kubernetes helps you make sure those containerized applications run where and when you want, and helps them find the resources and tools they need to work. Kubernetes is a production-ready, open-source platform designed with Google's accumulated experience in container orchestration, combined with best-of-breed ideas from the community. 

### What is a Kubernetes Cluster?
Kubernetes coordinates a highly available cluster of computers that are connected to work as a single unit. The abstractions in Kubernetes allow you to deploy containerized applications to a cluster without tying them specifically to individual machines. To make use of this new model of deployment, applications need to be packaged in a way that decouples them from individual hosts: they need to be containerized. Containerized applications are more flexible and available than in past deployment models, where applications were installed directly onto specific machines as packages deeply integrated into the host. 
Kubernetes automates the distribution and scheduling of application containers across a cluster in a more efficient way. 

A Kubernetes cluster consists of two types of resources:
- A **Master** coordinates the cluster
- **Nodes** are the workers that run applications

**The Master is responsible for managing the cluster.** The master coordinates all activities in your cluster, such as scheduling applications, maintaining applications' desired state, scaling out applications, and rolling out new updates. 

**A node is a VM or a physical computer that serves as a worker machine in a Kubernetes cluster.** Each node has a Kubelet, which is an agent for managing the node and communicating with the Kubernetes master. The node should also have tools for handling container operations, such as Docker or rkt. A Kubernetes cluster that handles production traffic should have a minimum three nodes. 

When you deploy applications on Kubernetes, you tell the master to start the application containers. The master schedules the containers to run on the cluster's nodes. **The nodes communicate with the master using the Kubernetes API**, which the master exposes. End users can also use the Kubernetes API directly to interact with the cluster. 

A Kubernetes cluster can be deployed on either physical or virtual machines. 
