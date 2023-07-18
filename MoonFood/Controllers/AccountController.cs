using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;

namespace MoonFood.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository; 

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountResponse>> GetAccountById(Guid id)
        {
            var account = await _accountRepository.GetById(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AccountResponse>> CreateAccount(CreateAccountRequest accountRequest)
        {
            var account = await _accountRepository.Add(accountRequest);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }

        [HttpGet("find-by-email")]
        public IActionResult GetAccountByEmail(string email)
        {
            var account = _accountRepository.GetByEmail(email);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAccounts(int currentPage =1, int pageSize = 20)
        {
            var pagination = _accountRepository.GetAllAccount(currentPage, pageSize);
            return Ok(pagination);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountRepository.DeleteAccount(id);
            return NoContent();
        }

    }
}
