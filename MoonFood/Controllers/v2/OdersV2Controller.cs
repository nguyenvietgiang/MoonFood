using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using Microsoft.AspNetCore.Authorization;

namespace MoonFood.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/oder")]
    [ApiVersion("2.0")]
    public class OdersV2Controller : BaseController
    {
        private readonly IOderRepository _oderRepository;

        public OdersV2Controller(IOderRepository oderRepository)
        {
            _oderRepository = oderRepository; 
        }

        /// <summary>
        /// Check order - admin, manager
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetOrders(int currentPage = 1, int pageSize = 10)
        {
            try
            {
                var orders = _oderRepository.GetOder(currentPage, pageSize);
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Orders retrieved successfully", Orders = orders });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { StatusCode = StatusCodes.Status500InternalServerError, Message = "An error occurred while retrieving orders: " + ex.Message });
            }
        }
    }
}
