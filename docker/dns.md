Containers should not rely on IP's for inter-communication. Containers are destroyed and recreated with new IPs all the time so referring to another container by its IP is not reliable. 

#### fortunately...

The Docker daemon has a built-in DNS server that containers use by default. 

Containers on the same network are able to communicate with each other through their container names.



