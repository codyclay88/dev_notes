
## What is a Model?
An idealized and simplified representation of a real-world thing. 

There are two models that are particularly important when it comes to networking:
1. OSI Seven-Layer Model
2. TCP/IP Model

These models are used to represent how networks function. 

## OSI Seven-Layer Model
1. Physical 
    - What type of cables am I using?
2. Data Link 
    - MAC Address, network cards, switches
3. Network 
    - Logical Addresses, IP, routers
4. Transport 
    - TCP/UDP, Ensures that the raw data gets assembled and disassembled to and from packets correctly
5. Session 
6. Presentation
    - Reframes data to convert data into various formats
7. Application
    - Smarts that are built into an application that make them network aware


## TCP/IP Model
1. Network Interface (Link)
    - Physical cabling, MAC addresses
2. Internet
    - IP Addresses, routers
3. Transport
    - Assembly and disassembly of packets, TCP, UDP
4. Application
    - OSI Session, Presentation, and Application
    - Looks at the Application layer as actual applications


### Incoming Data
The lowest levels (think Layers 1 and 2 on OSI and Layer 1 on TCP/IP) can be thought of like the Network Card. 

The network card waits around until it receives an (Ethernet) packet of data, at which point it verifies the MAC address and the checksum to make sure that the packet is a) for him, and b) is in good shape. After these two things have been verified, it will remove the MAC address and the checksum and send the data (which is now an IP packet) further up the stack. The Network Card will more than likely cache that MAC address for later use in case it needs to reply back to the sender. 

Next the packet gets sent to next layer, which is the Network Layer in the OSI model or the Internet layer on the TCP/IP Model. These respective layers are primarily concerned with IP addresses, and are responsible for stripping off the IP addresses from the packet (and potentially caching the IP addresses for later use), leaving us with a Layer 4 frame, which is more than likely a `TCP segment`. This segment then gets sent up to next layer. 

The next layer is the Transport layer, which is exactly the same on both the OSI and the TCP/IP models. This layer's job is to assemble and disassemble data. For outgoing data, this layer will break up big pieces of data into smaller chunks that can be sent individually to the receiver. For incoming data, this layer's job is to reassemble multiple segments into one bigger piece of data based on the `sequencing number`. Finally, after the data has been reassembled, it sends the data with the port numbers up to the next layer. 

Finally this data gets sent to the topmost layers, which in the TCP/IP model is the Application layer, and in the OSI model is the Session, Presentation, and Application layers. 

The Session layer (in the OSI model) is designed to connect a server to a client on the remote system. 

The Presentation layer (in the OSI model) is considered legacy and isnt really needed any more, but was originally designed to restructure data into a given format that made sense for a particular application. 

The Application layer (in both models) is responsible for mapping the ports to the correct application that needs the data. This layer will strip the ports that came from the layers below it and route the data to the application that is listening on that port, while typically caching the "return port" in case we need it later. 

### Outgoing data
Let's now look at this model for when data is being sent from our computer to a remote machine. 

An application wants to send out some data, so the Application layer will send the data and the ports down to the next layer. 

The next layer down is the Transport layer, which in the case of a large amount of data, will break down the data into smaller chunks and give sequencing numbers to the smaller chunks so that the receiver knows the order of the incoming data. These smaller chunks that have a sequence number are called "TCP segments", which are sent down to the next layer. 

The next layer down is the Network (OSI) or Internet (TCP/IP) layer, which is responsible for appending the IP Addresses of the senders and receivers to the TCP Segment, which is now considered an IP packet. Which is now sent down to the bottom-most layers. 

These bottom layers are the Data-Link/Physical layers (OSI) or Network Interface (A.k.a Link) (TCP/IP model). This layer is concerned with Ethernet frames, so they take the IP packet from the layer above and append the MAC addresses along with a frame check sequence. 

Finally that data is sent across the wire to the receiver. 