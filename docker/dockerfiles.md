A Dockerfile is a "recipe" for creating an image. 

Each "line" in a dockerfile creates a new layer that provides infrastructure or configuration that can used by the layers on top of it. 

The first line is every Dockerfile is the `FROM` command. This specifies a "base layer" that the rest of the layers in your Dockerfile will build off of. 

```dockerfile
FROM debian:jessie

# OR if you want a pure empty starting point
FROM scratch
```
The image that is used in the `FROM` command is usually a minimal Linux/Windows distribution like debian or alpine. If you wanted to start with a pure empty container, you could use `FROM scratch`. 

Environment Variables are often set to provide configuration for your images. In many cases, existing images will require certain environment variables to be set. We can do this using the `ENV` command:
```dockerfile
ENV NGINX_VERSION 1.11.10-1~jessie
```
One reason that environment variables were chosen as the default means of performing this configuration is because they work everywhere, regardless of OS. 

In many cases you may need to run a command within your image to install some sort of software or perform some sort of configuration. You can do this using the `RUN` command. 

```dockerfile
RUN ln -sf /dev/stdout /var/log/nginx/access.log / 
    && ln -sf /dev/stderr /var/log/nginx/error.log
```
This line is a shell command that runs inside a container at build time to forward request and error logs to the docker log collector. There are actually two commands here chained together via the `&&` operator, this is important because now these two commands are run at the same time and the changes that they produce will only constitute one layer. If we had run those commands via two separate `RUN` commands then we would actually get two different layers. 

In terms of logging, the proper way to perform logging within a container is to redirect logs to stdout and stderr, respectively, and docker handles all the logging for us. 

In many cases you may need to expose a port so that the outside world can communicate with it. This can be done with the `EXPOSE` command. 
```dockerfile
EXPOSE 80 443
```
This command exposes port 80 and 443 on the docker virtual network. While this exposes the ports to the docker VNET, you still need to map ports on the host to these ports inside the container via the -p or -P flags. 

The `CMD` command is the final command that will be run everytime you launch a new container from the image or restart a stopped container. 
```dockerfile
CMD ["nginx", "-g", "daemon off;"]
```

Once you have a completed Dockerfile, you can build it by using the `docker image build` command.
```
$ docker image build -t customnginx .
``` 
This command builds an image, tags the image with the name "customnginx" (via the `-t` flag), and builds the image from the current directory (as seen via the `.` at the end of the line). 