# TCP/IP Introduction

## IP Addresses
Each computer on a TCP/IP network must have a unique IP address. A typical IPv4 IP Address would look something like this: 207.35.60.114. When transmitted over the wire, IP Addresses are really just 1s and 0s. The "dots" in an IP address really don't exist, they are just separators. This particular way of describing an IP address is called `Dotted Decimal Notation`. 

## ARP (Address Resolution Protocol)
ARP is used to resolve MAC addresses from IP addresses. Essentially, when a computer knows the IP address (which is required for an IP Packet) of a computer but not its MAC address (which is required for an Ethernet frame), then the sender will broadcast a message saying "Who has 192.168.15.163 (the destination IP address)? Tell 192.168.15.23 (the sending IP Address)". All the computers on the network will get this message, but only the computer with the requested IP address will respond, and it will respond with something like "I am 192.168.15.163! My MAC address is AA-BB-CC-DD-EE-FF". Rather than continuously sending these ARP messages every time your computer needs to send a message to another computer, your computer will cache the IP and MAC addresses of the computers on the network for whenever you need it later. In Windows, you can view this ARP cache by typing:
```
C:\> arp -a
``` 
At a command prompt. This will return to you a table defining relationships between IP addresses and MAC addresses on your network. It also defines the type of relationship between the IP and MAC address, either dynamic or static. This cache is refreshed every few minutes. 

## Subnet Masks
A `Subnet Mask` defines the range of IP addresses that are in the same LAN. 