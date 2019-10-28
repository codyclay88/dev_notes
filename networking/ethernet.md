
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

## Modern Ethernet
Not long after 10 Mbps Ethernet, users were demanding faster speeds. It wasn't long before 100 Mbps Ethernet was introduced. At was at this point that Hubs became more and more obselete, because Hubs are, by their nature, limited to 10 Mbps. Also because of this speed boost, we started to see `Full Duplex` networking, where both computers involved in a conversation could converse at the same time, as opposed to `Half Duplex`, where only a computer could only transmit OR receive at one moment in time. In modern ethernet, Full Duplex is the standard.

`100BaseT` was one of the first standards agreed upon in the era of Modern Ethernet. It was operated at 100 Mbps, allowed 1024 nodes, ran for 100 meters, used Cat 5e cabling, only used 2 of the 4 twisted pairs in the cable, and allowed for Full Duplex communications. 

Also around this time we began to see fiber solutions. In particular we saw `100BaseFX`, we used a multi-mode fiber-optic connection that allowed for 2 km of cable between the node and the switch. 

In the 1990s the Ethernet folks came out with `Gigabit Ethernet`. When we talk about gigabit ethernet, we mean 1000Base*something*. 

There are 4 gigabit ethernet standards that we need to be concerned with: 
1. 1000BaseCX
    - Is a bit strange because it uses a special type of coaxial cable called `Twinax` and it only allows for 25 meters between the switch and the nodes. 
2. 1000BaseSX
    - Uses multimode fiber-optic cable with distances of about 500 meters. 
3. 1000BaseLX
    - Uses single-mode fiber-optic cable with distances of about 5 kilimeters. 
4. 1000BaseT
    - Uses unshielded twisted pair with Cat 6, with 100 meters distance. 

Even more recently, we have `10 Gigabit Ethernet`. This provides us with some strange standards, that are built to run on a different networking technology called `SONET`. These types of standards are made to work with either traditional Ethernet LANs, or SONET based networks. 
1. 10GBaseT
    - Designed to work with CAT 6 (for up to 55 meters) or CAT 6a (this is more encouraged, provides 100 meters).
2. 10GBaseSR
    - Multimode fiber cable, variable length depending on external conditions, but can run anywhere from 26 meters to 400 meters. 
3. 10GBaseLR
    - Runs on 1310 nanometer single-mode fiber, provides a range of 10 kilometers
4. 10GBaseER
    - Runs on 1550 nanometer single-mode fiber, provides a range of 40 kilometers

Each of these last three standards, 10GBaseSR, 10GBaseLR, and 10GBaseER, have a special version called 10GBaseSR/SW, 10GBaseER/EW, and 10GBaaseLR/LW, respectively. These special versions have the exact same specifications as their counterparts, but are intended to work with SONET networks. 

## Connecting Switches

You can expand your broadcast domain by connecting multiple switches together. When interfacing switches together, you will use what are called `patch cables`. There are two different kinds of patch cables that you will see, `straight-through` and `crossover`. In the old days you would connect two switches together using a crossover cable, and you can plug it into any port on either switch. Newer switches provide an `uplink port`, which is a port that is already wired for crossing over the connection. When using an uplink port you can you a straight-through cable. Some switches that have an uplink port have a toggle button that will toggle that uplink port between "uplink mode" and "normal mode". Most new switches however, have `auto-sensing ports`, which automatically recognize that they are connected to another swtich rather than a host. Because of this, in most cases you will use straight-through cables that can be plugged into any port on either switch, and these ports will automatically determine whether they are plugged into a switch or a host. 

## Transceivers
When working with Fiber connections there is a bit of a problem in terms of physical standards that we are working with. When working with copper we can almost always assume that we are going to be using RJ-45s, but we don't have the same story with fiber connections. However, all the network manufacturers got together and came up with something called `Multisource Agreement (MSA)` transcievers. These devices act like adapters, allowing you to use one switch with just about any fiber connector in existence. The first generation of these transceivers was called a `Gigabit Interface Converter (GBIC)`. Designed more for SC and ST connectors. Later on the `Small form-factor pluggable (SFP)` came into existence, which allowed for smaller types of connectors like LC. Nowadays we also have `SFP+`, which is much more common than SFP today. We also have `Quad small form-factor pluggable (QSFP)` which is designed to work with 40 gigabit ethernet. 

## Scenarios that Arise when Connecting Ethernet

### Loop Issues
When connecting switches, you cannot create a "loop". You can think of a loop as a physical circle of connections between switches. When doing this, data will start swirling around infinitely and can take down the entire broadcast domain. Most switches nowadays implement the `Spanning Tree Protocol (STP)` which is specifically designed to avoid this scenario. In an STP network, the switches will designate a root switch automatically, where the root switch will turn off one of its ports when it detects a bridging loop. 

We can have some problems when we have a bad actor that is flooding your network with data (usually done for DDOS or man-in-the-middle attack). In these types of situations, we need a special type of switch that has a `Flood guard`. These act similar to STP in that they turn off ports. 

### Mismatched Switch Issues
