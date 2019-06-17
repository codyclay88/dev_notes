Azure Event Hubs is a Big Data streaming platform and event ingestion service, capable of receiving and processing millions of events per second. Event Hubs can process and store events, data, or telemetry produced by distributed software and devices. Data sent to an event hub can be transformed and stored by using any real-time analytics provider or batching/storage adapters. Event Hubs for Apache Kafka also enables native Kafka clients, tools, and applications such as Mirror Maker, Apache Flink, and Akka Streams to work seamlessly with Event Hubs with only configuration changes. 

The following scenarios apply to Event hubs:
- Anomaly detection
- Application logging
- Analytics pipelines, such as clickstreams
- Live dashboarding
- Archiving data
- Transaction processing
- User telemetry processing
- Device telemetry processing

Data is valuable only when there is an easy way to process and get timely insights from data sources. Event Hubs provides a distributed stream processing platform with low latency and seamless integration, with data and analytics services inside and outside Azure to build your complete big data pipeline. 

Event Hubs represents the "front door" for an event pipeline, often called an *event ingestor* in solution architectures. An event ingestor is a component or service that sits between event publishers and event consumers to decouple the production of an event stream from the consumption of those events. Event Hubs provides a unified streaming platform with time retention buffers, decoupling event producers and event consumers. 

### Key Features
#### Fully Managed PaaS
Event Hubs is a fully managed Platform-as-a-Service with little configuration or management overhead, so you focus on your business solutions. Event Hubs for Apache Kafka ecosystems gives you the PaaS Kafka experience without having to manage, configure, or run your clusters. 

#### Support for real-time and batch processing
Ingest, buffer, store, and process your stream in real time to get actionable insights. Event Hubs uses a partitioned consumer model, enabling multiple applications to process the stream concurrently and letting you control the speed of processing. 

Capture your data in near-real time in an Azure Blob Storage or Azure Data Lake Storage for long-term retention or micro-batch processing. You can acheive this behavior on the same stream you use for deriving real-time analytics. Setting up capture of event data is fast. Event Hubs allows you to focus on data processing rather than data capture. 

### Key Components
- Event Producers: any entity that sends data to an event hub. Event publishers can publish events using HTTPS or AMQP 1.0 or Apache Kafka (1.0 and above)
- Partitions: Each consumer group only reads a specific subset, or partition, of the message stream
- Consumer Groups: a view (state, position, or offset) of an entire event hub. Consumer groups enable consuming applications to each have a separate view of the event stream. They read the stream independently at their own pace and with their own offsets. 
- Throughput units: Pre-purchased units of capacity that control the throughput capacity of Event Hubs. 
- Event receivers: any entity that reads event data from an event hub. All Event Hub consumers connect via the AMQP 1.0 session. The Event Hubs serice delivers events through a session as they become available. All Kafka consumers connect via the Kafka protocol 1.0 and later. 
