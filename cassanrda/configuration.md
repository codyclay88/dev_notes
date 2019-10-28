

## `cluster_name`
The name of the cluster. This is mainly used to prevent machines in one logical cluster from joining another. 

*Default Value: Test Cluster*

## `num_tokens`
This defines the number of tokens randomly assigned to this node on the ring. The more tokens, relative to other nodes, the larger the proportion of data that this node will store. You probably want all nodes to have the same number of tokens assuming they have equal hardware capacity. 
*Default Value: 256*

