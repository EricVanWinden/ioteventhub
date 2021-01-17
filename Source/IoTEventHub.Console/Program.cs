using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;

namespace IoTEventHub.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Substribe to logging
            Common.StatisticsSingleton.Instance.StatusLogged += SendToApplicationInsights;
            Common.StatisticsSingleton.Instance.LogMessage("Subscribed to logger");
            var config = Common.StatisticsSingleton.Instance.HubConfig;

            // Create processor
            var storageContainerName = "messagehost";
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;
            var _processor = new EventProcessorHost(config.HubName, consumerGroupName, config.IotHubConnectionString, config.StorageConnectionString, storageContainerName);
            _processor.RegisterEventProcessorAsync<Common.LoggingEventProcessor>();

            System.Console.ReadLine();

            // Save data and unregister the event processor
            Common.StatisticsSingleton.Instance.Save();
            _processor.UnregisterEventProcessorAsync();
        }

        /// <summary>
        /// The logger
        /// </summary>
        /// <param name="message"></param>
        public static void SendToApplicationInsights(string message)
        {
            System.Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
