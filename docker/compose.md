Docker compose allows us to work with multiple containers at once and define relationships between them. 

Docker compose is made up of two things:
1. YAML file that descriptions our solution in terms of containers, networks, volumes, etc. 
2. CLI-tool called docker-compose used for local dev/test automation with those YAML files. 

We can now use docker-compose in production with Swarm. 

`docker-compose.yaml` is the default filename, but you can specify an alternative name. 

Docker compose files typically follow a template similar to below:
```yml
version: '3.1' # will default to V1 if no version specified. 

services:   # containers, such as docker run
    servicename1: # friendly name, will also be the DNS name inside of the network 
        image: # Optional if you use build:
        command: # Optional, replace the default CMD specified by the image
        environment: # Optional, same as -e in docker run
        volumes: # Optional, same as -v in docker run
    servicename2:
        ...
    
volumes: # Optional, same as docker volume create

networks: # Optional, same as docker network create
```

The docker-compose CLI is great for local development/test, but is not really meant to be a production grade tool. 

The two most common commands are:
- `docker-compose up` which sets up all volumes and networks and starts all containers
- `docker-compose down` which stops all containers and removes networks and volumes that are no longer needed. 

