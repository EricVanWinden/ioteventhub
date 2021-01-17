using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoTEventHub.Web
{
    /// <summary>
    /// Controller for statistics
    /// </summary>
    [Route("api/[controller]")]
    public class ObservationController : Controller
    {
        /// <summary>
        /// Returns the last recorded statistic
        /// </summary>
        /// <returns>The last recorded statistic</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Common.Observation> Get()
        {
            return Ok(Common.StatisticsSingleton.Instance.LastObservation);
        }
    }
}
