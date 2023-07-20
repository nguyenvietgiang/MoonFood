using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;
using System.Data;

namespace MoonFood.Controllers
{
    [ApiController]
    [Route("api/v1/tables")]
    public class TableController : BaseController
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        /// <summary>
        /// create new table - manager
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult CreateTable(TableRequest tableRequest)
        {
            var createdTable = _tableRepository.CreateTable(tableRequest);
            return Ok(createdTable);
        }

        /// <summary>
        /// delete table - manager
        /// </summary>
        [HttpDelete("{tableId}")]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteTable(Guid tableId)
        {
            var result = _tableRepository.DeleteTable(tableId);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        /// <summary>
        /// get all table - no auth
        /// </summary>
        [HttpGet]
        public IActionResult GetTable(int currentPage = 1, int pageSize = 10)
        {
            var pagination = _tableRepository.GetTable(currentPage, pageSize);
            return Ok(pagination);
        }
    }
}
