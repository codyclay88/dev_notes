
New containers are attached to the "bridge" network driver by default. 

You can create new networks using the `docker network create` command, which allows you to create a private network to attach containers to. 
```
$ docker network create my_app_net
```
We can list all of the networks on our docker engine with the `docker network ls` command:
```
$ docker network ls
NETWORK ID          NAME                DRIVER              SCOPE
55490654f663        bridge              bridge              local
91519e89e8be        host                host                local
b0799287cb1d        my_app_net          bridge              local
4b8610e0ca07        none                null                local
```
This shows that by default we get 3 network drivers - bridge, host, and null. Our network that we created above, `my_net_app` also shows up here, but using the "bridge" driver.  

You can attach new containers to our `my_net_app` network using the `--network` switch with the `docker container run` command:
```
$ docker container run -d --name new_nginx --network my_app_net nginx
```

You can connect existing containers to networks using the `docker network connect` command:
```
$ docker network connect my_app_net some_container
```
This will add the network to `some_container`, meaning that if `some_container` was already attached to one network, it is now connected to two. 

You can similarly disconnect with the `docker network disconnect` command.

Two containers within the same network can access any ports that are exposed by the containers, no explicit `-p` flags are needed for intra-network communications. 



