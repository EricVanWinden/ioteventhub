using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IoTEventHub.Web
{
    /// <summary>
    /// Controller for statistics
    /// </summary>
    [Route("api/[controller]")]
    public class ObservationController : Controller
    {
        /// <summary>
        /// Send events, metrics and other telemetry to the Application Insights service.
        /// </summary>
        private readonly TelemetryClient _telemetry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telemetry"></param>
        public ObservationController(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        /// <summary>
        /// Returns the last recorded statistic
        /// </summary>
        /// <returns>The last recorded statistic</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Common.Observation> Get()
        {
            _telemetry.TrackEvent("GetObservation", new Dictionary<string, string> { { "machine", System.Environment.MachineName } });
            return Ok(Common.StatisticsSingleton.Instance.LastObservation);
        }
    }
}
