using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels;
using MoonModels.DTO.RequestDTO;

namespace MoonFood.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/menu/foods")]
    [ApiVersion("1.0")]
    public class FoodController : BaseController
    {
        private readonly IFoodRepositorycs _foodRepositorycs;

        public FoodController(IFoodRepositorycs foodRepositorycs)
        {
            _foodRepositorycs = foodRepositorycs;
        }

        /// <summary>
        /// add new food to menu - manager
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateFood([FromForm] CreateFoodRequest createFoodRequest)
        {
            try
            {
                string host = Gethost();
                var createdFood = await _foodRepositorycs.CreateFood(createFoodRequest, host);
                return Ok(createdFood);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the food: " + ex.Message);
            }
        }

        /// <summary>
        /// get menu - no auth
        /// </summary>
        [HttpGet]
        public IActionResult GetMenu(int currentPage = 1, int pageSize = 10)
        {
            var pagination = _foodRepositorycs.GetMenu(currentPage, pageSize);
            return Ok(pagination);
        }

        /// <summary>
        /// get detail - no auth
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFoodById(Guid id)
        {
            var food = await _foodRepositorycs.GetbyId(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        /// <summary>
        /// delete food - admin, manager
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            await _foodRepositorycs.DeleteFood(id);
            return NoContent();
        }
    }
}
