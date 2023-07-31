using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;
using Microsoft.AspNetCore.Authorization;

namespace MoonFood.Controllers
{
    [ApiController]
    [Route("api/v1/oder")]
    public class OdersController : BaseController
    {
        private readonly IOderRepository _oderRepository;

        public OdersController(IOderRepository oderRepository)
        {
            _oderRepository = oderRepository;
        }

        /// <summary>
        /// create new Oder
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOder([FromForm] OderRequest oderRequest)
        {
            try
            {
                var createdOder = await _oderRepository.CreateOder(oderRequest);
                return Ok(createdOder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the oder: " + ex.Message);
            }
        }

        /// <summary>
        /// check oder  - admin,  manager
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetOders(int currentPage = 1, int pageSize = 10)
        {
            var bookings = _oderRepository.GetOder(currentPage, pageSize);
            return Ok(bookings);
        }
    }
}
