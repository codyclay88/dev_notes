We're not going to tell you that debugging on the Lightning Platform is just as easy as it is in Visual Studio. To be perfectly honest, it's not even close. The good news is that in this area Salesforce has made enormous strides recently, and more is coming with every release. 

### Your Friend, the Debug Log
In the world of the Lightning Platform, the debug log is where you find most of what you need to debug and analyze your code. You can write to the debug log like so: 
```java
System.debug('My Debug Message');
```

You can also specify one or more of the following log levels:
- NONE
- ERROR
- WARN
- INFO
- DEBUG
- FINE
- FINER
- FINEST

The levels run from lowest to highest and are cumulative. So if you pick the finest level, you get all messages that are logged as error, warn, info, and so on. There are also several debug log categories, and the amount of information logged depends on the log level. 

This is important to know because of limits. Each debug log must be 20 MB or smaller. If it exceeds this amount, you won't see everything you need. Additionally, each org can retain up to 1000 MB of debug logs. The oldest logs are overwritten. 

### Use the Log Inspector
Developer Console has grown quite a bit in the last few releases. One of its more useful features is the Log Inspector. 

### Set Checkpoints
As a .NET developer, you're used to setting breakpoints in your applications. But in a multi-cloud, multi-tenanted environment where everyone is sharing resources, allowing everyone to halt execution and keep database connections open is disastrous. 

Checkpoints are similar to breakpoints in that they reveal a lot of detailed execution information about a line of code. They just don't stop execution on that line. 