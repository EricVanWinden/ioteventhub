using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;

namespace IoTEventHub
{
    class LoggingEventProcessor : IEventProcessor
    {
        public Task OpenAsync(PartitionContext context)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor opened, processing partition: {context.PartitionId}");
            return Task.CompletedTask;
        }

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor closing, partition: {context.PartitionId}, reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor error, partition: {context.PartitionId}, error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var payload = Encoding.ASCII.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);          
                var observation = JsonConvert.DeserializeObject<Observation>(payload);
                observation.Device = eventData.SystemProperties["iothub-connection-device-id"].ToString();
                observation.EnqueuedTime = eventData.SystemProperties["iothub-enqueuedtime"] as DateTime?;
                StatisticsSingleton.Instance.AddObservation(observation);                 
            }
            return context.CheckpointAsync();
        }
    }
}
