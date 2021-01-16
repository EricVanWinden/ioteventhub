namespace IoTEventHub.Common
{
    /// <summary>
    /// Single unit of statistical data
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// The key (device-year-month-day-hour)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The number ofobservations
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Average temperature (°C)
        /// </summary>
        public double AverageT { get; set; }

        /// <summary>
        /// Minimum temperature (°C)
        /// </summary>
        public double MinimumT { get; set; }

        /// <summary>
        /// Maximum temperature (°C)
        /// </summary>
        public double MaximumT { get; set; }

        /// <summary>
        /// Average relative humidity (%)
        /// </summary>
        public double AverageRH { get; set; }

        /// <summary>
        /// Minimum relative humidity (%)
        /// </summary>
        public double MinimumRH { get; set; }

        /// <summary>
        /// Maximum relative humidity (%)
        /// </summary>
        public double MaximumRH { get; set; }

        /// <summary>
        /// Average pressure (mBar)
        /// </summary>
        public double AverageP { get; set; }

        /// <summary>
        /// Minimum pressure (mBar)
        /// </summary>
        public double MinimumP { get; set; }

        /// <summary>
        /// Maximum pressure (mBar)
        /// </summary>
        public double MaximumP { get; set; }

        /// <summary>
        /// Empty constructor (used for serialization)
        /// </summary>
        public Statistics()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">The key</param>
        public Statistics(string key)
        {
            Key = key;
            Count = 0;
            AverageT = 0;
            AverageRH = 0;
            AverageP = 0;

            // Set minimum to double.MaxValue so the first observation will always be lower
            MinimumT = double.MaxValue;
            MinimumRH = double.MaxValue;
            MinimumP = double.MaxValue;

            // Set maximum to double.MinValue so the first observation will always be higher
            MaximumT = double.MinValue;
            MaximumRH = double.MinValue;
            MaximumP = double.MinValue;
        }


    }
}
