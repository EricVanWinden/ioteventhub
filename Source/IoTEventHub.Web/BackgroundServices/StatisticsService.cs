using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IoTEventHub.Web
{
    /// <summary>
    /// Class that recieves the IoT data and calculate hourly statistics
    /// </summary>
    public class StatisticsService : BackgroundService
    {
        /// <summary>
        /// Executes the hourly statistics calculation
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Substribe to logging
            Common.StatisticsSingleton.Instance.StatusLogged += PrintToLog;
            Common.StatisticsSingleton.Instance.LogMessage("Subscribed to logger");
            var config = Common.StatisticsSingleton.Instance.HubConfig;

            // Create processor
            var storageContainerName = "messagehost";
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;
            var _processor = new EventProcessorHost(config.HubName, consumerGroupName, config.IotHubConnectionString, config.StorageConnectionString, storageContainerName);
            await _processor.RegisterEventProcessorAsync<Common.LoggingEventProcessor>();

            var stopwatch = new Stopwatch();
            var interval = 300000; // 5 minutes in milli seconds
            stopwatch.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (stopwatch.ElapsedMilliseconds > interval)
                {
                    Common.StatisticsSingleton.Instance.Save();
                    stopwatch.Restart();
                }
            }

            // Save data and unregister the event processor
            Common.StatisticsSingleton.Instance.Save();
            await _processor.UnregisterEventProcessorAsync();
        }

        /// <summary>
        /// The logger
        /// </summary>
        /// <param name="message"></param>
        public void PrintToLog(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}\n");
        }
    }
}
