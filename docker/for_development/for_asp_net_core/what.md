So pretty much everyone agrees that Docker is awesome. It is famous for how easy it makes deploying our applications, based off the idea that if it builds and runs on your local machine, then it will run exactly the same in production. 

What I have been seeing more and more people talk about though is using Docker in the development phases of the project, rather than just the deployment and production phases. 

The idea here is that we can set up a development environment that consists solely of configuration files and source code, and from there we can spin up and tear down an entire system locally without having to install all kinds of database engines and language runtimes that we may never use again. 

So let's break down the requirements a little bit more, *What are we trying to achieve here?*

We want to be able to setup our development environment in such a way that our other team members don't need to have anything installed on their machine, no .NET Core Runtime, no Visual Studio, no SQL Server Express, they don't even have to be running the same operating system - the only thing they need is Docker, a text editor, and a terminal. This will enable us to onboard new developers very quickly, while also ensuring that everyone is using the exact same versions of all the dependencies in the development phase, as well as in production. Meaning that if it runs on my machine, then it'll run the exact same on their machine. 

Let's get started. 

I'll be using VS Code in this article, with Git Bash as the default terminal. 

Let's go ahead and open up 

Run the following command at a terminal:

```bash
$ docker run --rm -it -v $(pwd -W)/web-api:/app mcr.microsoft.com/dotnet/core/sdk:2.2-stretch bash
```

This command starts a new container from the `mcr.microsoft.com/dotnet/core/sdk:2.2-stretch bash` image provided by Microsoft.

After running this command (And possibly after installation of that Docker image, which may take a moment) you will eventually get a prompt:
```
root@dfe9713a3013:/#
```
This is a prompt for the bash shell running *inside the container*. This is really pretty awesome, if you stop and think about it. 

Okay, from here lets `ls` to see what we have inside our running container: 
```
root@dfe9713a3013:/# ls
app  bin  boot  dev  etc  home  lib  lib64  media  mnt  opt  proc  root  run  sbin  srv  sys  tmp  usr  var
```
This looks like standard Linux stuff, but we also have this `app` directory. Let's `ls` into it:
```
root@dfe9713a3013:/# ls app
README.md
```
So we have a README file in there, let's see what it says:
```
root@dfe9713a3013:/# cat app/README.md
This is a sample ASP.NET Core application by Cody that uses Docker.
``` 
Wha??? But that is the file that we created on our local machine. How did it get here?

Well let's go back to our `docker run` command and break it down a bit more. 

```
docker run --rm -it -v $(pwd -W)/web-api:/app mcr.microsoft.com/dotnet/core/sdk:2.2-stretch bash
```
The `-v $(pwd -W)/web-api:/app` portion of the command is what made this possible. This is called a "bind mount" in the Docker world, and what we are saying is that we want to bind the contents of the `$(pwd -W)/web-api` directory (the $(pwd -W) portion just tells the command to start at our current directory), to the `/app` directory inside the running container. So the contents of our `web-api` folder locally gets sent to the `/app` directory inside the container. Since we are humble developers, let's update the local version of our README.md file to say the following:
```
This is a sample ASP.NET Core application by Cody that uses Docker.
And it's the coolest, most original app ever.
``` 
And if we run the `cat app/README.md` command again, we now see the updates contents of that file. Pretty awesome!

But even more awesome is that this is a two-way street. Any changes we make inside the container to the `/app` directory also gets synchronized with our local `/web-api` folder. To prove it, lets run the following command:
```
root@dfe9713a3013:/# touch app/TEST
```
And you will now see a file named `TEST` in your local file system! Pretty awesome again! (Let's delete that `TEST` file just to clean things up a bit.)

But, yet again, even more awesome, we can use this to scaffold out our web-api project. 

Let's run the following command in the terminal of the container:
```
root@dfe9713a3013:/# dotnet new webapi -o app
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on app/app.csproj...
  Restore completed in 453.87 ms for /app/app.csproj.

Restore succeeded.
```
If you were paying attention, you probably just noticed that a ton of files were just popped into our local `web-api` directory. Thats because in our container we ran the `dotnet new` command to scaffold a new project, and because we have our local `web-api` directory mounted to the `/app` directory within the container, we now have a fully scaffolded ASP.NET Core Web API project on our local file system, without having any ASP.NET Core tools installed locally! 

We can actually build our project from within the container using the `dotnet build` command and we can see that it builds successfully. 

Let's go ahead and run this thing from within our container:
```
root@dfe9713a3013:/app# dotnet watch run
watch : Polling file watcher is enabled
watch : Started
: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using '/root/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[58]
      Creating key {a0e8db74-1803-45ea-b682-2d17de70ff5f} with creation date 2019-04-18 15:09:20Z, activation date 2019-04-18 15:09:20Z, and expiration date 2019-07-17 15:09:20Z.
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {a0e8db74-1803-45ea-b682-2d17de70ff5f} may be persisted to storage in unencrypted form.
info: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[39]
      Writing data to file '/root/.aspnet/DataProtection-Keys/key-a0e8db74-1803-45ea-b682-2d17de70ff5f.xml'.
warn: Microsoft.AspNetCore.Server.Kestrel[0]
      Unable to bind to https://localhost:5001 on the IPv6 loopback interface: 'Cannot assign requested address'.
warn: Microsoft.AspNetCore.Server.Kestrel[0]
      Unable to bind to http://localhost:5000 on the IPv6 loopback interface: 'Cannot assign requested address'.
Hosting environment: Development
Content root path: /app
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```
Okay, awesome! We've got some pretty nice output here, and the application is running! 

However, if we go to `https://localhost:5001/api/values`, we won't get anything back... Lame, right? 

Wrong, we just have to map our local port 5001 to port 5001 inside the container. And while we're at it, let's also do port 5000 as well. 

To do this, let's kill our current container. Press `CTRL+C` to stop our application and then the `exit` command to break out of bash and kill the container. 

Notice that we still have our files locally that we just created within the container, so killing the container does not delete the local files. 

Let's modify our original `docker run` command just a bit to allow us to access those ports from within the container:
```
docker run --rm -it -p 5000:5000 -p 5001:5001 -v $(pwd -W)/web-api:/app mcr.microsoft.com/dotnet/core/sdk:2.2-stretch bash
```

Here we added the `-p 5000:5000 -p 5001:5001` flags to map ports 5000 and 5001 locally to those same ports within the container. 

If we try to hit `http://localhost:5000/api/values` now though, we still get an error.. What's going on? Well, `localhost` from within the Docker container is not the same `localhost` that we have on our host machine. In order to fix this, the `CreateWebHostBuilder` method within our `Program.cs` file needs to look like this: 
```csharp
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001")
                .UseStartup<Startup>();
```
Here we are overriding the default localhost ports to instead use `0.0.0.0` so that we can access the application from the outside. Now if we refresh the browser we will be able to access it. Note that we didn't have to stop and rebuild our container, or even stop the server running in the container, because our watcher with the `dotnet watch run` command automatically picked up that change and rebuilt the app for us, even though we changed the file locally!

So here we have a pretty good development workflow:
1. We scaffold a new project
2. We run the project
3. We edit the code
4. The file watcher automatically recompiles the app
5. Repeat from 3 until complete.

The downside here is that requires a somewhat intimate knowledge of Docker in order to know the right commands to run in order to make things work properly. 

I think we can do better. Let's introduce the *Dockerfile*. 

Earlier in the article we used the `mcr.microsoft.com/dotnet/core/sdk:2.2-stretch` image from Microsoft in order to scaffold, build, and run our source code. The way we did this was by interacting with the terminal within the container itself. This got the job done, but not only does that require knowing how docker works, but it also requires your users to know how the dotnet CLI works. 

Instead of interacting directly with the image from Microsoft, we can build our own image to give us a more structured way of working with our application. 

Within the `web-api` directory, create a new file called `Dockerfile` (no file extension necessary).

**NOTE: I am not going to provide an in depth discussion of Dockerfiles here. The goal of this article is to explain how to leverage Docker in the development phase of a project, not to introduce Docker itself**

Add the following contents to our new `Dockerfile`:
```Dockerfile
# Set up our development context
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS dev

# set our working directory
WORKDIR /app 

# copy our .csproj file into it
COPY app.csproj .

# restore our dependencies
RUN dotnet restore 

# copy the rest of the code in
COPY . .

# build our project
RUN dotnet build

# expose the necessary ports
EXPOSE 5000 5001

# execute `dotnet watch run` within the container
CMD ["dotnet", "watch", "run"]
```

This Dockerfile performs the same steps that we were doing manually within the container (with the exception of scaffolding the project for us). 

We can build our own image from this Dockerfile, and we'll name it `docker-sample-app`

```
$ docker build -t docker-sample-app .
Sending build context to Docker daemon  1.134MB
Step 1/8 : FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS dev
 ---> e268893be733
Step 2/8 : WORKDIR /app
 ---> Using cache
 ---> 1b077709cd23
Step 3/8 : COPY app.csproj .
 ---> Using cache
 ---> d7b3479d6636
Step 4/8 : RUN dotnet restore
 ---> Using cache
 ---> 9e4ea8603bb9
Step 5/8 : COPY . .
 ---> f84a14c38eb4
Step 6/8 : RUN dotnet build
 ---> Running in 8ea03d31fc97
Microsoft (R) Build Engine version 16.0.450+ga8dc7f1d34 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 34.11 ms for /app/app.csproj.
  app -> /app/bin/Debug/netcoreapp2.2/app.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.70
Removing intermediate container 8ea03d31fc97
 ---> 21e70b2bf161
Step 7/8 : EXPOSE 5000 5001
 ---> Running in fe2233b418d9
Removing intermediate container fe2233b418d9
 ---> 871cdd79206a
Step 8/8 : CMD ["dotnet", "watch", "run"]
 ---> Running in 0590c9abfa1d
Removing intermediate container 0590c9abfa1d
 ---> 635c4c6255da
Successfully built 635c4c6255da
Successfully tagged docker-sample-app:latest
SECURITY WARNING: You are building a Docker image from Windows against a non-Windows Docker host. All files and directories added to build context will have '-rwxr-xr-x' permissions. It is recommended to double check and reset permissions for sensitive files and directories.
```

As we can see from the output, this ran through each line in our Docker file and displayed the corresponding output. Pretty awesome!! 

We can now start a container off of this new image using the following command:
```
$ docker run --name myapp -it --rm -p 5000:5000 -p 5001:5001 -v $(pwd -W):/app docker-sample-app
```

And now we have the same environment that we had before!

This is a little better, but it still requires the user to know which ports need to be mapped to and what volumes need to be created, as well as some extra flags that I've thrown in there. 

We can go one step further to make this process even easier, using `docker-compose`. 

Go into the root of the project and create a new file called `docker-compose.yml` and paste the following contents in:
```yml
version: '3.5'

services:
  web-api:
    build:
      context: ./web-api
      dockerfile: Dockerfile
    ports:
      - 5000:5000
      - 5001:5001
    volumes:
      - ./web-api:/app
```
We can see here that those extra volumes and ports from our `docker run` statement are now explicity declared. 

Now lets ensure that we are in the root of our project and say:
```
$ docker-compose up
```
Which does all sorts of docker wizardry and ultimately provides us with the same environment that we acheived the previous two ways. 

I hope you think this is as awesome as I do. 

Now that we have docker-compose, we can now locally orchestrate other containers to work with our sample app container, like a database, for example?






