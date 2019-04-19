What is a base image?

When building a Dockerfile, we have to start with a *base image*. The base image that we choose is the argument to the `FROM` command, which must be the first instruction in our Dockerfile. A base image can be one of two things:

1. `scratch` - representing a purely empty image. 
2. Any other image 

`alpine` is a very small Linux distribution with very few programs pre-installed on it, which provides us the Linux kernel and a package manager, but without some of the extra baggage that comes with other distribution. This makes it a very good candidate as a base image. 