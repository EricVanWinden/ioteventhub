using System.Collections.Generic;
using System.Linq;

namespace IoTEventHub.Common
{
    /// <summary>
    /// Singleton constuction to load, collect and save statistics of the IoT data
    /// </summary>
    public class StatisticsSingleton
    {
        #region Singleton

        /// <summary>
        /// The lock object to prevent multithreading issues
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// The private instance (Singleton)
        /// </summary>
        private static StatisticsSingleton _instance;

        /// <summary>
        /// The private constructor (Singleton)
        /// </summary>
        private StatisticsSingleton()
        {
            HubConfig = SynchronizationExtensions.GetHubConfig();
            _statistics = SynchronizationExtensions.GetStatistics();
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
        /// The last statistic
        /// </summary>
        public Statistics LastStatistic
        {
            get
            {
                return _statistics.Last();
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
                SynchronizationExtensions.PutStatistics(_statistics, "Statistics.json");
            }
        }

        /// <summary>
        /// The config
        /// </summary>
        public HubConfig HubConfig { get;  }
    }
}
