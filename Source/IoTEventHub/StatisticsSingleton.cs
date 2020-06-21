using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IoTEventHub
{
    /// <summary>
    /// Singleton constuction to load, collect and save statistics on the IoT data
    /// </summary>
    class StatisticsSingleton
    {
        #region Singleton

        /// <summary>
        /// The lock object to prevent multithreading issues
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// The path for reading or writing the IoT data.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The private instance (Singleton)
        /// </summary>
        private static StatisticsSingleton _instance;

        /// <summary>
        /// The private constructor (Singleton)
        /// </summary>
        private StatisticsSingleton()
        {
            var root = @"C:\Eric\GitHub\ioteventhub\Data";

            // config
            var path = Path.Combine(root, "HubConfig.json");
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                HubConfig = JsonConvert.DeserializeObject<HubConfig>(text);
            }

            // data
            _path = Path.Combine(root, "Statistics.json");
            if (File.Exists(_path))
            {
                string text = File.ReadAllText(_path);
                _statistics = JsonConvert.DeserializeObject<List<Statistics>>(text);
            }
            else
            {
                _statistics = new List<Statistics>();
            }
        }

        /// <summary>
        /// The public instance (Singleton)
        /// </summary>
        /// <returns></returns>
        public static StatisticsSingleton Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_lock)
                {
                    if (_instance != null)
                        return _instance;

                    _instance = new StatisticsSingleton();
                    return _instance;
                }
            }
        }

        #endregion

        /// <summary>
        /// Delegate to handle logging
        /// </summary>
        /// <param name="message">The message to log</param>
        public delegate void LogHandler(string message);

        /// <summary>
        /// The instance of LogHandler
        /// </summary>
        public event LogHandler StatusLogged;

        /// <summary>
        /// The list of statistics
        /// </summary>
        private readonly List<Statistics> _statistics;

        /// <summary>
        /// The list of statistics converted to Array
        /// </summary>
        public Statistics[] Statistics
        {
            get
            {
                var array = _statistics.ToArray();
                LogMessage($"Returning {array.Length} data points");
                return array;
            }
        }

        /// <summary>
        /// Send the messsage to the log handler
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogMessage(string message)
        {
            StatusLogged?.Invoke(message);
        }

        /// <summary>
        /// Adds an observation to the statistics
        /// </summary>
        /// <param name="observation"></param>
        public void AddObservation(Observation observation)
        {
            if (observation.EnqueuedTime == null)
            {
                LogMessage("No EnqueuedTime, cannot generate key");
                return;
            }

            if (string.IsNullOrEmpty(observation.Device))
            {
                LogMessage("Null or empty device, cannot generate key");
                return;
            }

            lock (_lock)
            {
                var key = $"{observation.Device}-{observation.EnqueuedTime.Value.Year}-{observation.EnqueuedTime.Value.Month}-{observation.EnqueuedTime.Value.Day}-{observation.EnqueuedTime.Value.Hour}";
                var current = _statistics.FirstOrDefault(s => s.Key == key);
                if (current == null)
                {
                    current = new Statistics(key);
                    _statistics.Add(current);
                    LogMessage($"Started analyzing {key}");
                }

                current.AverageT = ((current.Count * current.AverageT) + observation.Temperature) / (current.Count + 1);
                current.AverageRH = ((current.Count * current.AverageRH) + observation.Humidity) / (current.Count + 1);
                current.AverageP = ((current.Count * current.AverageP) + observation.Pressure) / (current.Count + 1);
                current.Count++;

                if (observation.Temperature < current.MinimumT) current.MinimumT = observation.Temperature;
                if (observation.Temperature > current.MaximumT) current.MaximumT = observation.Temperature;
                if (observation.Humidity < current.MinimumRH) current.MinimumRH = observation.Humidity;
                if (observation.Humidity > current.MaximumRH) current.MaximumRH = observation.Humidity;
                if (observation.Pressure < current.MinimumP) current.MinimumP = observation.Pressure;
                if (observation.Pressure > current.MaximumP) current.MaximumP = observation.Pressure;
            }
        }

        /// <summary>
        /// Saves the statistics to file
        /// </summary>
        public void Save()
        {
            lock (_lock)
            {
                var text = JsonConvert.SerializeObject(_statistics);
                File.WriteAllText(_path, text);
                LogMessage($"Saved {_statistics.Count} data points to {_path}");
            }
        }

        /// <summary>
        /// The config
        /// </summary>
        public HubConfig HubConfig { get;  }
    }
}
