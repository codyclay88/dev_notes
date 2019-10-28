
The actual data that is being sent across the wire is really just 1's and 0's. These two things can also be thought of as "on" and "off" and when data is sent from one endpoint to another, whether in the form of a copper wire sending low or high signals or a fiber-optic cable turning light on or off, all of this really just represents the binary code that can be put together to represent bigger things. 

This data is not sent as one continguous streams, it is instead sent as discrete chunks that are called "frames" or "packets". A single frame can contain up to 1500 bytes of data. Frames are actually created from within the Network Card (NIC), and also received from the Network Card. 

Frames also contain two MAC addresses, the MAC Address on it's own NIC (the source address), but also the MAC address of the NIC that it is sending the data to (the destination address). 

Let's take a look at a MAC address:
`70-4D-7B-2D-77-99`

This is a 48 bit number that is broken up into 6 pieces to make it easier to read. The first three pieces (70, 4D, and 7B) are called the OEM numbers (Original Equipment Manufacturer), and are unique to a given manufacturer, designated to them by *the internet folks*. A manufacturer may be Intel. The final three pieces (2D, 77, and 99) is called the Unique ID, and is burned into that individual card, and uniquely identifies a given NIC card from another one. 

Frames also contain a `CRC (cyclic redundancy check)` which is used to verify that the data within the frame has maintained its integrity while in transmission. 

## Broadcast vs Unicast

Unicast is when one sender has one intended recipient, and it appends the recipient's MAC address onto the frame that it is sending.  

A NIC could also send a frame as Broadcast, where it puts a special MAC address called a "Broadcast Address" (typically is `FF-FF-FF-FF-FF-FF`) in place of the recipient MAC address, and any NIC that receives the message knows that it can send it up the stack. 

Broadcasts can be very important. In many cases computers may not know the MAC address of all the computers on the network, so they sent out a broadcast frame saying "Hey, if you know who 'Cody's PC' is, send me a frame with the MAC address". 

This brings up the concept of a `Broadcast Domain`, which comprises all the computers that can accept the broadcast messages of another computer.  
