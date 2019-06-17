The Snitch serves two functions:
- It teaches Cassandra enough about your network topology to route requests efficiently. 
- It allows Cassandra to spread replicas around your cluster to avoid correlated failures. It does this by grouping machines into "datacenters" and "racks". Cassandra will do its best not to have more than one replica on the same "rack" (which may not be an actual physical location).

