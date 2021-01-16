namespace IoTEventHub.Web.Utils
{
    /// <summary>
    /// Api Configuration
    /// </summary>
    public class ApiConfiguration
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; set; }

        /// <summary>
        /// If true the ApiKey is checked
        /// </summary>
        public bool CheckKey { get; set; }
    }
}
