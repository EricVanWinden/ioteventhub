using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;

namespace IoTEventHub.Common
{
    /// <summary>
    /// Implementation of Microsoft.Azure.EventHubs
    /// </summary>
    public class LoggingEventProcessor : IEventProcessor
    {
        /// <summary>
        /// Called by processor host to initialize the event processor.
        /// </summary>
        /// <param name="context">Information about the partition that this event processor will process events from.</param>
        /// <returns></returns>
        public Task OpenAsync(PartitionContext context)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor opened, processing partition: {context.PartitionId}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called by processor host to indicate that the event processor is being stopped.
        /// </summary>
        /// <param name="context">Information about the partition.</param>
        /// <param name="reason">Reason why the event processor is being stopped.</param>
        /// <returns></returns>
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor closing, partition: {context.PartitionId}, reason: '{reason}'.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the underlying client experiences an error while receiving. EventProcessorHost
        /// will take care of recovering from the error and continuing to pump messages,
        /// so no action is required from your code. This method is provided for informational purposes.
        /// </summary>
        /// <param name="context">Information about the partition.</param>
        /// <param name="error">The error that occured.</param>
        /// <returns></returns>
        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            StatisticsSingleton.Instance.LogMessage($"LoggingEventProcessor error, partition: {context.PartitionId}, error: {error.Message}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called by the processor host when a batch of events has arrived.
        /// This is where the real work of the event processor is done.
        /// </summary>
        /// <param name="context">Information about the partition.</param>
        /// <param name="messages">The events to be processed.</param>
        /// <returns></returns>
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
