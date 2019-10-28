By default, containers within the same network can refer to each other by the named defined in the compose file of the `--name` set in the `docker run` command. 

Docker has a built-in DNS service that can map container names to IP addresses. 

Let's say that we are running in swarm mode and we have one web container named `web` and three api containers that share the name `api`. The web container can refer to the api containers through the `api` dns name, and through this the internal DNS will resolve to *one* of the three `api` containers.  

There are two options for service discovery within a swarm: `VIP` and `DNS Round Robin`.

VIP is the default, which used IPVS (which is a feature of the Linux Kernel) and provides one Virtual IP address per service. Consumers use this Virtual IP, and IPVS   handles the load-balancing to the containers. This approach uses Layer 3/4 routing. 

In DNS Round Robin, each container is given a unique IP address, and the built-in web server provides Round-robin style load balancing. This approach uses OSI Model Layer 7 routing. 

VIP is lower in the network stack and uses kernel mode load balancing, making it more efficient that DNSRR. 