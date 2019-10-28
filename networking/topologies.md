
How does the data actually move back and forth between the nodes in a network? This is defined in the `topology`, which defines the organization of how the frames move from host to host. 

There are two different classifications of topologies, physical and logical. 

Let's discuss a few topologies:

### Bus Topology

```
Host <----> Host <-----> Host <----> Host
```
All the hosts are connected to a single host of cable called a `bus`. It is generally considered obselete, but there are still some use cases for it today. 

### Ring Topology
A Ring topology is similar to a bus but there is no beginning or end like in the Bus topology. Again, considered to be legacy with the exception of a few use cases. 

### Star Topology
```
            Host
             |
Host ------ Hub ------- Host
             |
            Host
```
A Star topology is made up of individual hosts that are individually connected to a central hub. This is considered a *physical topology*, meaning that we could actually represent this topology with the physical components of our network. Also considered legacy. 

### Star-Bus Topology

In this topology, we take a traditional Bus topology and we shrink it down into a little box, typically called a hub or a switch, so that it physically looks like a star, but acts like a bus topology. 

*This is considered a hybrid topology, because it **physically** looks like a Star topology, but it behaves **logically** like a bus topology*

***The Star-Bus Topology is the primary topology that we use today***

### Mesh Topology
```
        Host
      /  |  \
     /   |   \
  Host--------Host
     \   |   /
      \  |  /
        Host
```
This topology is unique to wireless networks. Computers that are connected in this topology are all connected to each other. This is why Meshes are typically used in wireless scenarios, because running a cable from each machine to another is not practical. 

When every node in the network has a bidirectional connection to every other node in the network that is called a `Fully Meshed` topology. But we can make things more interesting by creating a `Partially Meshed` topology, where one or more nodes may be connected to only one node.  
```
        Host
      /  |  \
     /   |   \
  Host--------Host------Host
     \   |   /
      \  |  /
        Host
```
This is very common with wireless networks. 