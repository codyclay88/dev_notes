Event Hubs provides a Kafka endpoint that can be used by your existing Kafka based applications as an alternative to running your own Kafka cluster. Event Hubs supports Apache Kafka protocol 1.0 and later, and works with your existing Kafka applications, including Mirror Maker. 

The Event Hubs for Kafka feature provides a protocol heap on top of Azure Event Hubs that is binary compatible with Kafka versions 1.0 and later for both reading from and writing to Kafka topics. You may start using the Kafka endpoint from your applications with no code change but a minimal configuration change. You update the connection string in your configurations to point to the Kafka endpoint exposed by your event hub instead of pointing to your Kafka cluster. Then, you can start streaming events from your applications that use the Kafka protocol into Event Hubs. 

Conceptually, Kafka and Event Hubs are nearly identical, they are both partitioned logs built for streaming data. The following table maps concepts between Kafka and Events Hubs:

|Kafka Concept|Event Hubs Concept|
|-------------|------------------|
|Cluster|Namespace|
|Topic|Event Hub|
|Partition|Partition|
|Consumer Group|Consumer Group|
|Offset|Offset|

