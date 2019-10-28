Originally developed at Facebook in 2008. 

Founded on the principles from Google's BigTable and Amazon's DynamoDB. 

A Cassandra cluster is made up of nodes, where all nodes have the same responsibilities, there is no "master" node, which provides no single point of failure. 

Each record in Cassandra has an associated "token", and then token can have a huge number of possible values, ranging from -2^63 to 2^63.
When there is only one node in a given cluster, that node is responsible for all those maintaining token values. 
When a second node is introduced, it splits the load of those token values with the original node, and so on and so forth. 

As of Cassandra 1.2, a concept of "virtual nodes" (AKA vnodes) was added, and each physical node is responsible for maintaining a group of vnodes that further segment the range of tokens that the physical node is responsible for. 

To get a single node running in Docker:
```
$ docker run --name cassn1 --network cass-network -d cassandra
```
**NOTE: starting the container with an explicit network makes it easier to create a cluster later on**
You can view the status of your cluster by running the `nodetool` utility with the `status` parameter. `nodetool` is installed by default on any cassandra image.
```
$ docker exec -it cassn1 nodetool status
Datacenter: datacenter1
=======================
Status=Up/Down
|/ State=Normal/Leaving/Joining/Moving
--  Address     Load       Tokens       Owns (effective)  Host ID                               Rack
UN  172.17.0.2  103.67 KiB  256          100.0%            1277f86b-3dbd-4acc-ac15-d112129d448e  rack1
```
This tells us that our node is "UN" meaning "Up" and "Normal", that it is running 256 Tokens, which actually means that it is running 256 vnodes. 

Using the `nodetool ring` command gives us more information about the vnodes in our cluster:
```
$ docker exec -it cassn1 nodetool ring

Datacenter: datacenter1
==========
Address     Rack        Status State   Load            Owns                Token
                                                                           9150538845786020893
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             -9181037891716884629
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             -9160827813611371776
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             -9111978338024982395
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             -9077916366320825944
... (omitted for brevity)
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             9045713537378684871
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             9148678648717504715
172.17.0.2  rack1       Up     Normal  103.67 KiB      100.00%             9150538845786020893
```
This command displays information about each vnode in the cluster, displaying it's IP Address, status, and the last column tells us the last token that the vnode is responsible for within it's token range. 

We can check out our configuration for our node by starting a bash session in the running container
```
$ docker exec -it cassn1 bash
root@df98485f3533:/# cat /etc/cassandra/cassandra.yaml
```
The `cassandra.yaml` configuration file is localted at `/etc/cassandra/` for this particular image. 
This configuration file is huge, and provides a lot of methods for configuration. 

To add a new node to our cluster, we can create a new container on the same network as the original node, while also specifying the `CASSANDRA_SEEDS` environment variable, like below:
```
$ docker run --name cassn2 -d --network cass-network -e CASSANDRA_SEEDS=cassn1 cassandra
```

Now when we check our status we see this:
```
$ docker exec -it cassn1 nodetool status
Datacenter: datacenter1
=======================
Status=Up/Down
|/ State=Normal/Leaving/Joining/Moving
--  Address       Load       Tokens        Owns (effective)  Host ID                               Rack
UN  192.168.48.2  108.58 KiB  256          100.0%            7209ab53-0425-4c1b-9885-8bd97d40b83b  rack1
UJ  192.168.48.3  88.91 KiB   256          ?                 00ea254a-9b30-4b31-96bc-3baca6716333  rack1
```
This new node will initially have a status of "UJ", the "J" meaning that it is still in the process of joining the cluster. After a moment it will display the status of "UN". 













