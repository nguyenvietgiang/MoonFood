using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MoonModels.Paging;
using MoonModels;

namespace MoonFood.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/tables")]
    [ApiVersion("1.0")]
    public class TableController : BaseController
    {
        private readonly ITableRepository _tableRepository;
        private readonly IDistributedCache _cache;

        public TableController(ITableRepository tableRepository, IDistributedCache cache)
        {
            _tableRepository = tableRepository;
            _cache = cache;
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
        public async Task<IActionResult> GetTable(int currentPage = 1, int pageSize = 10)
        {
            string cacheKey = $"GetTable_{currentPage}_{pageSize}";
            string cachedData = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cachedData))
            {
                var pagination = _tableRepository.GetTable(currentPage, pageSize);
                var serializedData = JsonSerializer.Serialize(pagination);

                await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });

                return Ok(pagination);
            }
            else
            {
                var pagination = JsonSerializer.Deserialize<Pagination<Table>>(cachedData);
                return Ok(pagination);
            }
        }
    }
}

