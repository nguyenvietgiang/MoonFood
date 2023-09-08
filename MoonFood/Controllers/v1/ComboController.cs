using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;

namespace MoonFood.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/menu/combo")]
    [ApiVersion("1.0")]
    public class ComboController : BaseController
    {
        private readonly IComboRepository _comboRepository;

        public ComboController(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;
        }

        /// <summary>
        /// add new combo to menu - manager
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateCombo([FromForm] ComboRequest comboRequest)
        {
            try
            {
                string host = Gethost();
                var createdCombo = await _comboRepository.CreateCombo(comboRequest, host);
                return Ok(createdCombo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the combo: " + ex.Message);
            }
        }

        /// <summary>
        /// get menu combo - no auth
        /// </summary>
        [HttpGet]
        public IActionResult GetMenu(int currentPage = 1, int pageSize = 10)
        {
            var pagination = _comboRepository.GetMenuCombo(currentPage, pageSize);
            return Ok(pagination);
        }
    }
}
