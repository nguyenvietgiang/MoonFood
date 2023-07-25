using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;


namespace MoonFood.Controllers
{
    [ApiController]
    [Route("api/v1/statistical")]
    [Authorize(Roles = "Manager")]
    public class StatisticalController : BaseController
    {
        private readonly IStatisticalRepositpry _statisticalRepositpry;

        public StatisticalController (IStatisticalRepositpry statisticalRepositpry)
        {
            _statisticalRepositpry = statisticalRepositpry;
        }

        /// <summary>
        /// get statistics - manager
        /// </summary>
        [HttpGet("accounts")]
        public ActionResult<AccountStatistics> GetAccountStatistics(DateTime? dateFilter = null)
        {
            var statistics = _statisticalRepositpry.GetAccountStatistics(dateFilter);

            return Ok(statistics);
        }
    }
}
