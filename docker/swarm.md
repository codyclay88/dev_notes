
Swarm is a clustering solution that is built inside Docker. 

You can create a swarm using the following command:
```
$ docker swarm init
Swarm initialized: current node (amnyikyszbhslj6u2tk9wfo8r) is now a manager.

To add a worker to this swarm, run the following command:

    docker swarm join --token SWMTKN-1-38feooywivl8yrh0hz119wu1ki9wye3zrnub5g0fkj2c5esqps-7klggx12cjasfax26gej0mije 192.168.65.3:2377

To add a manager to this swarm, run 'docker swarm join-token manager' and follow the instructions.
```

You can view all the nodes in your swarm with `docker node ls`:
```
$ docker node ls
ID                            HOSTNAME                STATUS              AVAILABILITY        MANAGER STATUS      ENGINE VERSION
amnyikyszbhslj6u2tk9wfo8r *   linuxkit-00155dfa9c1a   Ready               Active              Leader              18.09.2
```

In a swarm configuration, the `service` CLI command replaces the `run` command. 
You can create a new service like so:
```
$ docker service create alpine ping 8.8.8.8
n9eyl3r4afyv8adjyi8ggrp99
overall progress: 1 out of 1 tasks
1/1: running   [==================================================>]
verify: Service converged
```
In the command above we created a new container running as a service in our swarm cluster and gave it a default command of `ping 8.8.8.8`, which is just a google DNS server. 

We can see all the services running in our swarm with the following command:
```
$ docker service ls
ID                  NAME                      MODE                REPLICAS            IMAGE               PORTS
n9eyl3r4afyv        inspiring_chandrasekhar   replicated          1/1                 alpine:latest
```
We can see from above that we now have one service named "inspiring_chandrasekhar" running in our swarm. We can also see that we have 1/1 replica set up for that service. We can further inspect that service with the following command:
```
$ docker service ps inspiring_chandrasekhar
ID                  NAME                        IMAGE               NODE                    DESIRED STATE       CURRENT STATE           ERROR               PORTS
lhvo8ilvulx2        inspiring_chandrasekhar.1   alpine:latest       linuxkit-00155dfa9c1a   Running             Running 5 minutes ago
```
Which tells us the current state of the single replica that we have for that service. 

We can scale out our service to include more replicas with the following command: 
```
$ docker service update inspiring_chandrasekhar --replicas  3
inspiring_chandrasekhar
overall progress: 3 out of 3 tasks
1/3: running   [==================================================>]
2/3: running   [==================================================>]
3/3: running   [==================================================>]
verify: Service converged
```

We can stop our service (and all associated replicas) like so:
```
$ docker service rm inspiring_chandrasekhar
```
