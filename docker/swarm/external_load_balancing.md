Swarm provides two methods of load balancing external traffic into the container: Ingress Routing and Host Mode routing. 

Ingress Routing is the default, and maps the ports of the containers themselves to the outside world, allowing the users to use the IP Address of any node in the cluster, and the cluster will automatically direct the traffic to a node that is hosting that given container. 

Host mode maps the ports of the nodes themselves to the outside world. As a result, in order to access a container you must know the IP address of the node running the container that you wish to access. 