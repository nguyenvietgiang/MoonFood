﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using System.Data;

namespace MoonFood.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/excel-file")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Manager")]
    public class FileExcelController : Controller
    {
        private readonly IExelRepository _exelRepository;
        public FileExcelController(IExelRepository exelRepository)
        {
            _exelRepository = exelRepository;
        }

        /// <summary>
        /// get account template - admin, manager
        /// </summary>
        [HttpGet("template/{templateName}")]
        public IActionResult GetExcelTemplate(string templateName)
        {
            try
            {
                byte[] templateData = _exelRepository.GetExcelTemplate(templateName);
                return File(templateData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", templateName + "Template.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve Excel template: " + ex.Message);
            }
        }


    }
}
