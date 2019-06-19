
# Types of Cabling

## Coaxial
Coaxial cables consist of a copper conductor, followed by an insulator, followed by another conductor. This is where the name "Coaxial" comes from, the two conductor materials share the same center and the same axis. 

There are different types of Coaxial cable identied by their `Radio Grade` (or RG) rating. This defines the thickness of the conductors, insulators, and shielding, and also the Ohms that are exhibited in the cable. 

Not really used in Networking anymore, aside from `Cable Modems`. 

- RG-58 
    - 50 Ohms
    - One of the oldest Coaxial cables used in networking
- RG-59
    - 75 Ohms
    - Not particularly robust
    - Uses a threaded F-Type connector
- RG-6
    - 75 Ohms
    - Thicker than RG-59
    - More well used than RG-59

## Twisted Pair
Twisted Pair cabling is predominantly used out in the wild. It is called twisted pair because wires come in pairs and these pairs are twisted together, which extends the range significantly. 

Unshielded Twisted Pair wires are dirt cheap, but don't contain any extra shielding around the pairs, so you have to be careful about where you deploy it. These cables can have any number of twisted pairs within the cable, but in most cases there are only going to be four twisted pairs. 

Shielded Twisted Pair cables have a shield that surround the internal twisted pairs, and are preferred over unshielded in cases where you are going to be around electric motors or other objects that can interfere with the signal. 

The most typically connector that you see with twisted pair cabling is called an `RJ-45` connector. 

Within an individual wire in a twisted pair cable you will have either `solid core` or `stranded` copper. In solid core you just have one solid copper wire, but in stranded wire it is made up of much smaller copper wires. 

There are two standards out there for connecting a the wires in a twisted-pair cable to an RJ-45 connector; 568A and 568B. 

Unshielded Twisted Pair cables have been around for a while, and throughout that time several improvements have been made to it. One of which is the ability to work with faster and faster networks. In order to determine whether a cable will work with a faster network, we use a CAT rating. CAT rating has nothing to do with the number of pairs, but the different ratings have different numbers of twists in the wires.

- `CAT 3` (10 Mbps)
- `CAT 5` (100 Mbps @ 100 meters)
- `CAT 5e` (100 Mbps @ 100 meters, improved version of CAT 5)
- `CAT 6` (1 Gbps @ 100 meters)
- `CAT 6a` (10 Gbps @ 100 meters)
- `CAT 7` (10 Gbps @ 100 meters, shielded, uses a different type of connector, so does not have wide adoption)

### Terminated Twisted Pair
A piece of Unshielded Twisted Pair that has connectors on both sides of the wire is called `Terminated Twisted Pair`. 

The act of putting the connectors on the ends of a spool of cable is called `crimping`. To do this, you need a `crimper`, which is specifically designed to crimp RJ-45 connectors onto cable. The first step is to cut a portion of the outer 'jacket' off of the cable. After that, you must pull apart the twisted pairs and lay them out in the order that you will be installing them into the RJ-45 connector. The order for TIA-568A is as follows:
|Position|Color|
|-|------------|
|8| Brown|
|7| Brown striped|
|6| Orange|
|5| Blue striped|
|4| Blue|
|3| Orange striped|
|2| Green|
|1| Green striped |
Once they individual wires have been placed into this order, you want to squeeze them all together tightly in a broom shape. After that, some crimps will provide a "collar" that you put the wires into that make it easier to put into the connector. Others require you to place them directly into the connector itself. After that, you use the crimper and squeeze the connector with the wires inside as tightly as possible to crimp the wires inside the connector. 

A `straight through cable` (which is the most common) aligns the colors on the end connectors exactly the same, meaning that if I used 568A on one end then I use 568A on the other. The opposite would be a `crossover cable`, where you make one end 568A and the other 568B. This is done to cross the transmit and receive lines on one end to the other. Crossover cables can be useful when you want to connect two computers together so that they can transit and receive with each other. 

## Fiber Optic Cabling
A Fiber Optic cable is made up of a Core, which actually carries the light, surrounded by a Cladding, which is what the light is reflecting off of as it bounces off the fiber, and then a Cable Jacket which protects the integrity of the cable.

There are two types of fiber-optic cable; `multimode` and `single-mode`. Multimode cables carry LED signals, and are almost always orange. Single-mode cables are designed to carry laser signals, and is designed to go really long distances, and is almost always yellow. In a fiber-optic network card, they always provide two connectors, and the cables themselves provide two wires. This is called Duplex, which is how the majority of fiber-optic cables work. 

There are many types of fiber-optic connectors out there but the big ones are:
- ST - very early type of connector, round, push in and twist
- SC - square, punch in and pull out (old)
- FC - similar to ST, but screws in (old)
- LC - Two connectors built into one (high-density)
- MT-RJ - Also two connectors built into one, very small (high-density)

## Fire Ratings

Different types of cables have different fire ratings. There are three important fire ratings:
1. Plenum-rated
    - The Plenum area is the gap between the bottom of a drop ceiling and the actual ceiling. 
    - This is the highest fire rating that we have
    - Two to three times more expensive than PVC cabling
2. Riser-rated
    - Designed to run between floors in a building
3. PVC (or non-plenum)
    - Provides no fire-protection
    - Still difficult to catch on fire, but can still create smoke and noxious fumes

## Legacy Network Connections

- Serial Ports
    - The oldest I/O connection in the world of computers
    - Use a protocol called RS-232 to communicate between two devices
    - Two types of connections:
        - DB-9, provides 9 pins in the connection
        - DB-25, provides 25 pins in the connection
- Parallel Ports
    - Used primarily for printers back in the day, but could be used for primitive bus type connections. 
    - Typically will have a female connector rather than a male.

For very high-end routers, you will have an extra connector called a "Console" port, that looks like an RJ-45 but it is actually a Serial connection, and this provides us with the ability to connect to the device at a very low-level. This type of cable is called a `Yost (or rollover)` cable. 

## IEEE Standards

In February 1980 the IEEE Committee setup a group of standards called the `802 Committees`.
| IEEE Committee | Focus |
|----------------|-------|
| 802.1          | Higher Layer LAN protocols |
| 802.3          | Ethernet |
| 802.11         | Wireless LAN (WiFi) |
| 802.15         | Wireless Personal Area Network (WPAN) |
| 802.18         | Radio Regulatory Technical Advisory Group |
| 802.19         | Wireless Coexistence Working Group |
| 802.20         | Mobile Broadband Wireless Access (MBWA) |
| 802.21         | Media Independent Handover Services |
| 802.22         | Wireless Regional Area Networks |