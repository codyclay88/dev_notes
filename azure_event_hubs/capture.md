Azure Event Hubs enables you to automatically capture the streaming data in Event Hubs in an Azure Blob Storage or Azure Data Lake storage account of your choice, with the added flexibility of specifying a time or size interval. Setting up Capture is fast, there are no administrative costs to run it, and it scales automatically with Event Hubs throughput units. Event Hubs Capture is the easiest way to load streaming data into Azure, and enables you to focus on data processing rather than on data capture. 

### How it works
Event Hubs is a time-retention durable buffer for telemetry ingress, similar to a distributed log. The key to scaling in Event Hubs is the partitioned consumer model. Each partition is an independent segment of data and is consumed independently. Over time this data ages off, based on the configurable retention period. As a result, a given event hub never gets "too full". 

Event Hubs Capture enables you to specify your own Blob Storage account and container, or Data Lake Store account, which are used to store the captured data. These accounts can be in the same region as your event hub or in another region. 

Captured data is written in Apache Avro format: a compact, fast, binary format that provides rich data structures with inline schema. This format is widely used in the Hadoop ecosystem, Stream analytics, and Azure Data Factory. 

### Windowing
Event Hubs Capture enables you to set up a window to control capturing. This window is a minimum size and time configuration with a "first wins policy", meaning that the first triger encountered causes a capture operation. If you have a 15 minute, 100MB capture window and send 1 MB per second, the size window triggers before the time window. Each partition captures independently and writes back a completed block blob at the time of capture, named for the time at which the capture interval was encountered. The storage naming convention is as follows:
```
{Namespace}/{EventHub}/{PartitionId}/{Year}/{Month}/{Day}/{Hour}/{Minute}/{Second}
```
Note that the date values are padded with zeroes; an example filename might be:
```
https://mystorageaccount.blob.core.windows.net/mycontainer/mynamespace/myeventhub/0/2017/12/08/03/03/17.avro
```