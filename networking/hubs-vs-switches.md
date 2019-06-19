
## What is a Hub?

A `Hub` is a *multiport repeater*. Meaning that when a computer on the network sends a frame to the hub, it creates multiple copies of that frame (one for every computer on the network). Once the frame has been copied, it sends those frames to the other computers on the network. 

**A Hub is not an amplifier, it is a repeater**.  

There are some downsides to a Hub, for example, lets say we have a 10BaseT Hub. This Hub, by it's definition, has a maximum bandwidth of 10 Mbps. If you have four computers on your network, A, B, C, and D, and A is talking to B at the same time that C is talking to D, then these conversations get half the available bandwidth, meaning that the conversation between A and B get 5 Mbps, and the same goes for C and D. This is only for two conversations, but imagine if we had 7 or 8 conversations going on simultaneously, that bandwidth would be halved for each individual conversation. **With a Hub, you get a signal degradation when you have lots of conversations going on simultaneously.**

When multiple conversations are happening simultaneously, Hubs use `CSMA/CD` (Carrier Sense Multiple Access / Collision Detection). Within a network using a Hub, there can actually only be one frame that is being repeated at one moment in time. When two or more frames arrive at the same time, a "collision" occurs. When a collision occurs with `CSMA/CD`, the network cards that detected the collision generate a random number, and each network card waits that random number of milliseconds to resend their frame. The set of computers that can *collide* with each other at any point is called a `Collision Domain`. When you hear the term "collision domain", we are generally talking about Hubs.

## What is a Switch?
A `Switch`, like a Hub, is also a multiport repeater, but a Switch keeps a table (called a `MAC Table`) of the MAC addresses at each port. Because of this, when a Switch receives a frame, it doesn't need to make a copy of the frame for each computer on the network, it knows exactly which port it needs to send the message to by correlating the destination MAC address in the from with it's internal MAC Table, so it's just sends the message to it's destination. With a switch we are able to send data from point-to-point, rather than broadcast the message to every computer in the network as you would with a Hub. 

One interesting thing that can happen is when you try to send a *broadcast message* (a frame with the destination MAC address of FF-FF-FF-FF-FF-FF) through the network. In this case, a Switch acts exactly like a Hub. It will create multiple copies of that frame for each computer on the network and send that frame to each one. This leads us to the idea of a `Broadcast Domain`, which is the set of computers that exist on a network that can receive broadcast messages from each other. When you hear the term "broadcast domain", we are generally talking about Switches. 

