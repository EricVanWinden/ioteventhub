using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoTEventHub.Web
{
    /// <summary>
    /// Controller for statistics
    /// </summary>
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        /// <summary>
        /// Returns the last recorded statistic
        /// </summary>
        /// <returns>The last recorded statistic</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Common.Statistics> Get()
        {
            return Ok(Common.StatisticsSingleton.Instance.LastStatistic);
        }
    }
}
