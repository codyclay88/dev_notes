# Routing

## What is Routing?
The process of mapping an incoming request to an endpoint.

## Two different types of Routing
1. Conventional Routing: uses application wide patterns to match a URL to a Controller Action Method/Razor Page, etc. 
2. Attribute Routing: Implemented through Attributes directly to a Controller or Action Method, or Page or Page Handler

## Conventional Routing
Conventional Routes are registered during startup and consumed by Routing Middleware. Routes use patterns that map to URL segments and values. 

```csharp
app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
```

## Attribute Routing
Attribute Routing allows us to explicitly define routing information for an individual controller or page. 

```csharp
[Route("product")] // provide a base URL for the controller
public class ProductController : Controller {
    [Route("edit/{id}")] // allow for dynamic parameters
    public IActionResult Edit(int id) {
        return View();
    }

    [Route("landing")]
    public IActionResult Index() {
        return View();
    }
}
```

Also, it is important to know that once a route has been registered via Attribute Routing, it is no longer eligible for Conventional Routing. 

## Understanding Route Execution Order
The app first checks if the Endpoint we are trying to reach has any Route Attributes applied. 
If no Attributes apply for this Endpoint, then the app will evaluate the Conventional Route we
registered in our Startup file, in the order that they were registered.

