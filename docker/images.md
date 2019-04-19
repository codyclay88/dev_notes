An Image is essentially a definition of binaries and dependencies, as well as metadata about the image and how to run it. 

An image is made up of layers. Each of these layers are stacked on top of each other to create the final image. Each layer is considered "immutable", meaning that once the layer is created, it cannot be changed or modified in any way. This means that we can reliably share layers between multiple images without having to worry about one layer changing underneath us. 

Because of this, these "layers" are stored in an image cache. This means that we can store the layers that we use so that if another image also relies on that layer then we dont need to redownload it, because we already have it. 

All images start out with only an empty layer known as "scratch", and every change that is made to that image moving forward creates a new layer. Each layer gets its own unique SHA that helps the system distinguish between different layers. 

Using the `docker image history <image_name>` command shows you a list of all the layers that make up the image through via a sort of "change-log" on each layer. 

```
$ docker image history nginx
IMAGE               CREATED             CREATED BY                                      SIZE                COMMENT
bb776ce48575        4 days ago          /bin/sh -c #(nop)  CMD ["nginx" "-g" "daemon…   0B
<missing>           4 days ago          /bin/sh -c #(nop)  STOPSIGNAL SIGTERM           0B
<missing>           4 days ago          /bin/sh -c #(nop)  EXPOSE 80                    0B
<missing>           4 days ago          /bin/sh -c ln -sf /dev/stdout /var/log/nginx…   22B
<missing>           4 days ago          /bin/sh -c set -x  && apt-get update  && apt…   54MB
<missing>           4 days ago          /bin/sh -c #(nop)  ENV NJS_VERSION=1.15.11.0…   0B
<missing>           4 days ago          /bin/sh -c #(nop)  ENV NGINX_VERSION=1.15.11…   0B
<missing>           2 weeks ago         /bin/sh -c #(nop)  LABEL maintainer=NGINX Do…   0B
<missing>           2 weeks ago         /bin/sh -c #(nop)  CMD ["bash"]                 0B
<missing>           2 weeks ago         /bin/sh -c #(nop) ADD file:4fc310c0cb879c876…   55.3MB
```

Using the `docker image inspect <image_name>` will display all the metadata about the given image, for example what ports it exposes, the OS it was built for, the system architecture it runs on, etc. 

```
$ docker image inspect nginx
[
    {
        "Id": "sha256:bb776ce48575796501bcc53e511563116132b789ab0552d520513da8c738cba2",
        "RepoTags": [
            "nginx:latest"
        ],
        "RepoDigests": [
            "nginx@sha256:50174b19828157e94f8273e3991026dc7854ec7dd2bbb33e7d3bd91f0a4b333d"
        ],
        "Parent": "",
        "Comment": "",
        "Created": "2019-04-10T21:22:15.797870505Z",
        "Container": "72c77d6abc2dc476d5ed0331239cbcab1c26019a28f6bd7941158acd55a4c4ff",
        "ContainerConfig": {
            "Hostname": "72c77d6abc2d",
            "Domainname": "",
            "User": "",
            "AttachStdin": false,
            "AttachStdout": false,
            "AttachStderr": false,
            "ExposedPorts": {
                "80/tcp": {}
            },
            "Tty": false,
            "OpenStdin": false,
            "StdinOnce": false,
            "Env": [
                "PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin",
                "NGINX_VERSION=1.15.11-1~stretch",
                "NJS_VERSION=1.15.11.0.3.0-1~stretch"
            ],
            "Cmd": [
                "/bin/sh",
                "-c",
                "#(nop) ",
                "CMD [\"nginx\" \"-g\" \"daemon off;\"]"
            ],
            "ArgsEscaped": true,
            "Image": "sha256:8ecbd4eb2e99de8e73ae47a97f843860ac7127c1c39baf150f0943fd5e4bfbc3",
            "Volumes": null,
            "WorkingDir": "",
            "Entrypoint": null,
            "OnBuild": null,
            "Labels": {
                "maintainer": "NGINX Docker Maintainers <docker-maint@nginx.com>"
            },
            "StopSignal": "SIGTERM"
        },
        "DockerVersion": "18.06.1-ce",
        "Author": "",
        "Config": {
            "Hostname": "",
            "Domainname": "",
            "User": "",
            "AttachStdin": false,
            "AttachStdout": false,
            "AttachStderr": false,
            "ExposedPorts": {
                "80/tcp": {}
            },
            "Tty": false,
            "OpenStdin": false,
            "StdinOnce": false,
            "Env": [
                "PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin",
                "NGINX_VERSION=1.15.11-1~stretch",
                "NJS_VERSION=1.15.11.0.3.0-1~stretch"
            ],
            "Cmd": [
                "nginx",
                "-g",
                "daemon off;"
            ],
            "ArgsEscaped": true,
            "Image": "sha256:8ecbd4eb2e99de8e73ae47a97f843860ac7127c1c39baf150f0943fd5e4bfbc3",
            "Volumes": null,
            "WorkingDir": "",
            "Entrypoint": null,
            "OnBuild": null,
            "Labels": {
                "maintainer": "NGINX Docker Maintainers <docker-maint@nginx.com>"
            },
            "StopSignal": "SIGTERM"
        },
        "Architecture": "amd64",
        "Os": "linux",
        "Size": 109294563,
        "VirtualSize": 109294563,
        "GraphDriver": {
            "Data": {
                "LowerDir": "/var/lib/docker/overlay2/1fec29452e6863aeb638ba10f0a0a97f2b4b689d51fdf17b84452ac642a2d1a3/diff:/var/lib/docker/overlay2/230994fb7e114a6a6d93da2fd531e469f5332bf418b1363518bb582c0474a6f9/diff",
                "MergedDir": "/var/lib/docker/overlay2/763918a222a55b8b4301087b6169a6bf41b03644fe530860af75910abbc61903/merged",
                "UpperDir": "/var/lib/docker/overlay2/763918a222a55b8b4301087b6169a6bf41b03644fe530860af75910abbc61903/diff",
                "WorkDir": "/var/lib/docker/overlay2/763918a222a55b8b4301087b6169a6bf41b03644fe530860af75910abbc61903/work"
            },
            "Name": "overlay2"
        },
        "RootFS": {
            "Type": "layers",
            "Layers": [
                "sha256:5dacd731af1b0386ead06c8b1feff9f65d9e0bdfec032d2cd0bc03690698feda",
                "sha256:b0a13438d0d39cb4d9d355a0618247f94b97a38208c8a2a4f3d7d7f06378acb2",
                "sha256:19d384dcffcccd44d9f475ed776358a81fb05e7948249bb50f8d7784e0f0f433"
            ]
        },
        "Metadata": {
            "LastTagTime": "0001-01-01T00:00:00Z"
        }
    }
]
```

When starting a container, the docker engine is creating just a single read/write layer on top of an image. 