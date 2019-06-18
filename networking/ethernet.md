
# Ethernet Basics
Ethernet is the standard defined by IEEE Committee `802.3`. The Ethernet frame is the only thing about the `802.3` standard that has stayed the same since February 1980. 

The Ethernet Frame consists of source and destination MAC addresses, some amount of data (in the form of 1s and 0s), and a CRC (or a frame check sequence). This frame is normally never more than 1500 bytes. Because this frame never changes, this allows us to, for instance, make newer network cards backwards compatible by slowing down the network card, because the frames that they work with have been the same since 1980. Also we can connect an ethernet network using fiber-optic to another ethernet network using unshielded twisted pair because the frame doesn't change, regardless of the physical medium. 

Different versions of Ethernet have different nomenclatures that describe them. 
- ex. 10Base5
    - *10* 
        - describes the speed in Mbps (in this case 10)
    - *Base* 
        - either `Base` or `Broad`
            - `Broad` means that it is using `Broadband`, meaning that the network has many 'bands' or 'channels' running through it
            - `Base` means that there is only one 'channel' and all traffic goes though that one channel. 
                - `Base` is by far more common
    - *5*
        - a somewhat legacy concept
        - before the days of switches, this was the length (in 100 meters) of cable that was used as the bus of the network that other cables would join into.\
        - Generally all you see today is `T` meaning that it uses Unshield Twisted Pair cabling with a switch in the middle. 

## Frames
An Ethernet frame is made up of ordered blocks, in this order:

`Preamble`:`Destination_MAC`:`Source_MAC`:`Data_Type`:`Data`:`FCS`

- Preamble
    - The preamble is basically just a sporadic ordering or 0s and 1s that let the network card know that a frame is incoming.
- Destination MAC
    - The MAC address of the NIC that the data is intended for
- Source MAC
    - The MAC address of the NIC that the data is coming from
- Data Type (or Ether Type)
    - 2 bytes long
    - Lets us know what kind of data we are hauling
- Data
    - The data itself
    - Their is a minimum (64 bytes) of maximum (1522 bytes) of data that can be sent in a frame
    - If data is less than the minimum amount, then we are going to include a `pad`, which pushes the data length to the minimum size
    - If data is more than the maximum size, then we break the data into smaller pieces and send multiple frames. 
    - The maximum amount of data that can be transmitted in a frame is also called the Maximum Transmission Unit (or MTU)
    - There also exists a `Jumbo Frame` which allows one frame to haul up to 9000 bytes
        - Typically only used in high speed scenarios
- FCS (Frame Check Sequence)
    - a 32 bit cyclic redundancy check
    - provides a way to ensure that the data sent in the packet has not been modified or corrupted in transmission
    - done through some fancy mathematics

## The Early Days of Ethernet

Before Hubs and Switches were invented, Network Engineers used 10Base5 cables, which were huge yellow cables that typically ran through the ceilings of buildings, and whenever you had a new computer that you wanted to add to the network you would inject a "vampire connector" tranceivers (not sure what that is actually called) into the cable above where the computer would be sitting, and then you'd plug an ethernet cable into that vampire connector, which would then *drop* out of the ceiling and plug into the computer.

10Base5 is rarely used anymore, but the concept of "Segmented Ethernet" still exists, where rather than a switch or a hub, you still have one long cable that runs though the ceiling. In this scenario, in order for multiple computers to talk at one time, we have to know about `CSMA/CD` (Carrier Sense Multiple Access / Collision Detection). When a computer would try and send data, they first needed to be sure that no other computers on the bus were sending data. If no one else was active then they would send their frames. These frames would travel down both ends of the network, and when they hit the end of the bus, they would *reflect*, which causes problems, so they would put `Terminating Resistors` at the ends of the bus to eat the frames so they wouldn't bounce back into the network after they hit the end of the bus. In the instance that a collision is detected, the two computers would wait a random number of milliseconds and try again.  

10Base2 is an exception to this type of Ethernet, which would use what is called `BNC Connectors`, which included a `T Connector` (which looked like a T) to connect multiple networks cards together directly, without needing the vampire connector tranceivers. This method also used Terminating Resistors at the ends of the network, and would support up to 30 NICs on the Network. 

## 10BaseT
IBM eventually came out with a competitor to Ethernet called `Token Ring`, and rather than use a bus, they would supply these *boxes* that were originally called `Multistation Access Units (MASU)` that you would plug your computers into. This technology was very popular, but it was also proprietary. 

In order to stay competitive, the Ethernet folks came out with the idea of a `hub`, where they would shrink down the bus architecture and put it in a little box using Unshielded Twisted Pair. This formed the basis 10BaseT Ethernet. 
Important points about 10BaseT are:
1. 10 Mbps 
2. 100 meters between switch and node
3. Maximum of 1024 nodes per switch
4. Cat 3 cable or better