using Newtonsoft.Json;
using System;

namespace IoTEventHub
{
    /// <summary>
    /// Class to store a single observation
    /// </summary>
    public class Observation
    {
        /// <summary>
        /// The temperature (°C)
        /// </summary>
        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        /// <summary>
        /// The relative humidity (percentage)
        /// </summary>
        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        /// <summary>
        /// The pressure (mBar)
        /// </summary>
        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        /// <summary>
        /// The time this obervation was enqueued
        /// </summary>
        public DateTime? EnqueuedTime { get; set; }

        /// <summary>
        /// The device that made this observation
        /// </summary>
        public string Device { get; set; }
    }
}
