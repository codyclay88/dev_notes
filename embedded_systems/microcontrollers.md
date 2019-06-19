## What is a Microcontroller?

A Microcontroller (`MCU` for short) is a small computer on a single integrated circuit. An MCU is similar to, but less sophisticated than, a system on a chip (`SoC`); an SoC may include a microcontroller as one of its components. A microcontroller contains one or more CPUs (processor cores) along with memory and programmable I/O peripherals. Microcontrollers are designed for embedded applications, in contrast to the microprocessors used in personal computers. 

A microcontroller is a single integrated circuit, commonly with the following features:
- CPU, ranging from small and simple 4-bit processors to complex 32-bit or 64-bit processors.
- Volatile memory (RAM) for data storage
- EEPROM or Flash memory for programing and operating parameter storage
- GPIO pins
- Serial Input/Output such as serial ports (UARTs)
- Other serial communication interfaces like I2C, SPI, and CAN for system interconnectivity
- Peripherals such as timers, event counters, PWN generators, and watchdogs
- Clock generator - often an oscillator for a quartz timing crystal, resonator, or RC circuit
- Analog-to-digital converters, or sometimes digital-to-analog converters

While some embedded systems are very sophisticated, many have minimal requirements for memory and program length, with no operating system, and low software complexity. Typical input and output devices include switches, relays, solenoids, LEDs, small or custom liquid crystal displays, radio frequency devices, and sensors for data such as temperature, humidity, light level, etc. 

## How are they used? 

Microcontrollers are used in automatically controlled products and devices, such as automobile engine control systems, implantable medical devices, remote controls, appliances, and other embedded systems. 

Mixed signal microcontrollers are common, integrating analong components needed to control non-digital electronic systems. In the context of the internet of things, microcontrollers are an economical and popular means of data collection, sensing, and actuating the physical world as `edge devices`. 




## How do they work?

Some microcontrollers may use four-bit words and operate at frequencies as low as 4 kHz, for low power consumption. They generally have the ability to retain functionality while waiting for an event such as a button press or other interrupt; power consumption while sleeping (CPU clock and most peripherals off) may be just nanowatts, making many of them well suited for long lasting battery applications. Other microcontrollers may serve performance-critical roles, where they may need to act more like a digital signal processor, with higher clock speeds and power consumption. 

### Interrupts
Microcontrollers must provide real-time (predictable, though not necessarily fast) response to events in the embedded system they are controlling. When certain events occur, an interrupt system can signal the processor to suspend processing the current instruction sequence and to begin an `interrupt service routine (ISR, or interrupt handler)`, which will perform any processing required based on the source of the interrupt, before returning to the original instruction sequence. Possible interrupt sources are device dependent, and often include events such as an internal timer overflow, completing an analog to digital conversion, a logic level change on an input such as from a button being pressed, and data received on a communications link. Where power consumption is important as in battery devices, interrupts may also wake a microcontroller from a low-power sleep state where the processor is halted until required to do something by a peripheral event. 

### Programs
Typically microcontroller programs must fit in the available on-chip memory, since it would be costly to provide a system with external, expandable memory. Compilers and assemblers are used to convert both high-level and assembly language codes into a compact machine code for storage in the micro-controller's memory. 

Manufacturers have often purchased special versions of their micro-controllers in order to help the hardware and software development of the target system. 

### GPIO
Microcontrollers usually contain several to dozens of general purpose input/output pins. GPIO pins are software configurable to either an input or an output state. When GPIO pins are configured to an input state, they are often used to read sensors or external signals. Configured to the output state, GPIO pins can drive external devices such as LEDs or motors, often indirectly through external power electronics. 

Many embedded systems need to read sensors that produce analog signals. This is the purpose of the `analog-to-digital converter (ADC)`. Since processors are built to interpret and process digital data, i.e, 1s and 0s, they are not able to do anything with the analog signals that may be sent to a device. So the ADC is used to convert the incoming data into a form that the processor can recognize. A less common feature on some MCUs is a `digital-to-analog converter (DAC)` that allows the processor to output analog signals or voltage levels. 

A dedicated `pulse-width modulation (PWM)` block makes it possible for the CPU to control power converters, resistive loads, motors, etc., without using lots or CPU resources in tight timer loops. 

A `universal asynchronous receiver/transmitter (UART)` block makes it possible to receive and transmit data over a serial line with very little load on the CPU. Dedicated on-chip hardware also often includes capabilities to communicate with other devices (chips) in digital formats such as `Inter-Integrated Circuit (I2C)`, `Serial Peripheral Interface (SPI)`, `Universal Serial Bus (USB)`, and `Ethernet`. 


