### Service Definition
Like many RPC systems, gRPC is based around the idea of defining a service, specifying the methods that can be called remotely with their parameters and return types. By default, gRPC uses protocol buffers as their Interface Definition Language (IDL) for describing both the service interface and the structure of the payload messages. It is possible to use other alternatives if desired. 

gRPC let's you define four kinds of service methods:
- Unary RPCs where the client sends a single request to the server and gets a single response back, just like a normal function call.
  ```
  rpc SayHello (HelloRequest) returns (HelloResponse) {}
  ```
- Server streaming RPCs where the client sends a request to the server and gets a stream to read a sequence of messages back. The client reads from the returned stream until there are no more messages. gRPC guarantees message ordering within an individual RPC call. 
  ```
  rpc LotsOfReplies (HelloRequest) returns (stream HelloResponse) {}
  ```
- Client streaming RPCs where the client writes a sequence of messages and sends them to the server, again using a provided stream. Once the client has finished writing the messages, it waits for the server to read them and return its response. Again gRPC guarantees message ordering within an individual RPC call
  ```
  rpc LotsOfGreetings (stream HelloRequest) returns (HelloResponse) {}
  ```
- Bidirectional streaming RPCs where both sides send a sequence of messages using a read-write stream. The two streams operate independently, so clients and servers can read and write in whatever order they like: for example, the server could wait to receive all the client messages before writing its responses, or it could alternatively read a message then write a message, or some combination of reads and writes. The order or messages in each stream is preserved.
  ```
  rpc BidiHello (stream HelloRequest) returns (stream HelloResponse) {}
  ```
We'll look at the different types of RPC in more detail in the RPC life cycle section below. 

### Using the API surface
Starting from a service definition in a .proto file, gRPC provides protocol buffer compiler plugins that generate client- and server-side code. gRPC users typically call these APIs on the client side and implement the corresponding API on the server side. 
- On the server side, the server implements the methods declared by the service and runs a gRPC server to handle client calls. The gRPC infrastructure decodes incoming requests, executes service methods, and encodes service responses. 
- On the client side, the client has a local object known as *stub* that implements the same methods as the service. The client can then just call those methods on the local object, wrapping the parameters for the call in the appropriate protocol buffer message type - gRPC looks after sending the request(s) to the server and returning the server's protocol buffer response(s). 

### Synchronous vs. Asynchronous
Synchronous RPC calls that block until a response arrives from the server are the closest approximation to the abstraction of a procedure call that RPC aspires to. On the other hand, networks are inherently asynchronous and in many scenarios it's useful to be able to start RPCs without blocking the current thread. 

The gRPC programming surface in most languages comes in both synchronous and asynchronous flavors. 

## RPC Lifecycle
Now let's take a closer look at what happens when a gRPC client calls a gRPC serer method. 

### Unary RPC
- Once the client calls the method on the stub/client object, the server is notified that the RPC has been invoked with the client's metadata for this call, the method name, and the specified deadline if applicable. 
- The server can then either send back its own initial metadata (which must be sent before any response) straight away, or wait for the client's request message - which happens first is application-specific. 
- Once the server has the client's request message, it does whatever work is necessary to create and populate its response. The response is then returned (if successful) to the client together with status details (status code and optional status message) and optional trailing metadata. 
- If the status is OK, the client then gets the response, which completes the call on the client side. 

### Server Streaming RPC
A server-streaming RPC is similar to our simple example, except the server sends back a stream of responses after getting the client's request message. After sending back all its responses, the server's status details (status code and optional status message) and optional trailing metadata are sent back to complete on the server side. The client completes once it has all the server's responses. 

### Client streaming RPC
A client streaming RPC is also similar to our simple example, except the client sends a stream of requests to the server instead of a single request. The server sends back a single response, typically but not necessarily after it has received all the client's requests, along with its status details and optional trailing metadata. 

### Bidirectional streaming RPC
In a bidirectional streaming RPC, again the call is initiated by the client calling the method and the server receiving the client metadata, method name, and deadline. Again, the server can choose to send back its initial metadata or wait for the client to start sending requests. 

What happens next depends on the application, as the client and server can read and write in any order - the streams operate completely independently. So, for example, the server could wait until it has received all the client's messages before writing its responses, or the server and client could "ping-pong": the server gets a request, then sends a response, then the client sends anopther request based on the response, and so on. 

### Deadlines/Timeouts
gRPC allows clients to specify how long they are willing to wait for an RPC to complete before the RPC is terminated with the error `DEADLINE_EXCEEDED`. On the server side, the server can query to see if a particular RPC has timed out, or how much time is left to complete the RPC. 

How the deadline or timeout is specified varies from language to language - for example, not all languages have a default deadline, some language APIs work in terms of a deadline (a fixed point in time) and some language APIs work in terms of timeouts (durations of time). 


