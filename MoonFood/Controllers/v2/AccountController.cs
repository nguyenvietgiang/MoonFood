using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;


namespace MoonFood.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiVersion("2.0")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public AccountController(IAccountRepository accountRepository, IBackgroundJobClient backgroundJobClient)
        {
            _accountRepository = accountRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        /// <summary>
        /// get list user - admin, manager (Version 2)
        /// </summary>
        [HttpGet("get-all")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAccounts(int currentPage = 1, int pageSize = 5, string search = null)
        {
            var pagination = _accountRepository.GetAllAccount(currentPage, pageSize, search);
            return Ok(pagination);
        }
    }
}

