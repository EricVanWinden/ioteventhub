namespace IoTEventHub.Common
{
    /// <summary>
    /// The configuration of the IoT hub
    /// </summary>
    public class HubConfig
    {
        /// <summary>
        /// The hub name
        /// </summary>
        public string HubName { get; set; }

        /// <summary>
        /// The IoT hub connection string
        /// </summary>
        public string IotHubConnectionString { get; set; }

        /// <summary>
        /// The azure storage connection string
        /// </summary>
        public string StorageConnectionString { get; set; }
    }
}
