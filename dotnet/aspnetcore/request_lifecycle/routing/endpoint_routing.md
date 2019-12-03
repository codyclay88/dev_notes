# Endpoint Routing

Endpoint Routing is new as of ASP.NET Core 2.2. 

## What is an Endpoint?
An Endpoint is a class that contains a Request Delegate and other metadata used to generate a response. 

A generalized `Endpoint` class can be seen below:
```csharp
public class RouteEndpoint : Endpoint
{
    public string DisplayName { get; set; }

    public string Pattern { get; set; }

    public EndpointMetadata Metadata { get; set; }

    public RequestDelegate RequestDelegate { get; set; }
}
```

There are now two Middlewares involved in the routing process.
1. Endpoint Routing Middleware: Decides with Endpoint should handle the request. 
2. Endpoint Middleware: Executes the selected Endpoint to generate a response.

As the request comes in, it gets processed by the **Endpoint Routing Middleware** early in the pipeline which resolves which endpoint will be executed. After this middleware completes, the request continues on to other middlewares in the pipeline, which can now utilize the endpoint information in its processing of the request. After all other middlewares have completed, the **Endpoint Middleware** executes the selected endpoint to generate the response, which is then sent back down the middleware pipeline. 



