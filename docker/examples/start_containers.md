
- start nginx container from command line:
```
$ docker container run --name myNginx --publish 80:80 -d nginx 
```
Here we are saying to run the `nginx` container, which an alias of `myNginx`, and to publish it on public port 80 and to map that to the private port 80 within the container. 

- view logs for nginx container that has name "myNginx"
```
$ docker logs myNginx
```

- start mysql container:
```
$ docker container run --name my_mysql -p 3306:3306 -d -e MYSQL_RANDOM_ROOT_PASSWORD=yes mysql
```
This will create a random root password (providing some form of password is required in this case), the password will be displayed in the logs after the container is created. 

