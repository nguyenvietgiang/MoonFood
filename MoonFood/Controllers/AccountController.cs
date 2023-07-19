using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels;
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

        /// <summary>
        /// user login to app 
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = _accountRepository.Login(loginRequest);

            if (loginResponse == null)
                return Unauthorized();

            return Ok(loginResponse);
        }
        /// <summary>
        ///  get current user info 
        /// </summary>
        [HttpGet("my-info")]
        [Authorize]
        public async Task<ActionResult<AccountResponse>> GetMyInfo()
        {
            var Id = GetUserIdFromClaim();
            var accountResponse = await _accountRepository.GetById(Id);

            if (accountResponse == null)
                return NotFound();

            return Ok(accountResponse);
        }

        /// <summary>
        /// get user by id - no auth
        /// </summary>
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
        /// <summary>
        /// register new account - no auth
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<AccountResponse>> CreateAccount(CreateAccountRequest accountRequest)
        {
            var account = await _accountRepository.Add(accountRequest);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }

        /// <summary>
        /// get user by email - admin, manager
        /// </summary>
        [HttpGet("find-by-email")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<AccountResponse>> GetAccountByEmail(string email)
        {
            var account = await _accountRepository.GetByEmail(email);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        /// <summary>
        /// get list user - admin, manager
        /// </summary>
        [HttpGet("get-all")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAccounts(int currentPage =1, int pageSize = 20)
        {
            var pagination = _accountRepository.GetAllAccount(currentPage, pageSize);
            return Ok(pagination);
        }

        /// <summary>
        /// delete user - admin, manager
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountRepository.DeleteAccount(id);
            return NoContent();
        }

        /// <summary>
        /// current user change info
        /// </summary>
        [HttpPatch]
        [Authorize]
        public IActionResult PatchAccount([FromBody] JsonPatchDocument<Account> patchDocument)
        {
            var userId = GetUserIdFromClaim();
            var patchedUser = _accountRepository.PatchAccount(userId, patchDocument);

            if (patchedUser == null)
            {
                return NotFound();
            }

            return Ok(patchedUser);
        }

        /// <summary>
        /// current user change password
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword(ChangePassword model)
        {
            var Id = GetUserIdFromClaim();
            bool result = _accountRepository.ChangePassword(Id, model);

            if (!result)
            {
                return BadRequest("Invalid current password or user not found.");
            }

            return Ok("Password changed successfully.");
        }

        /// <summary>
        /// change account permistion - manager
        /// </summary>
        [HttpPut("{id}/change-permistion")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ChangeAccountType(Guid id,[FromBody] string newTypeInput)
        {
            var isTypeChanged = await _accountRepository.ChangeAccountTypeById(id, newTypeInput);
            if (isTypeChanged)
            {
                return Ok("Account Type changed successfully.");
            }
            return NotFound("Account not found or invalid Type input.");
        }

        /// <summary>
        /// ban, unban account - admin, manager
        /// </summary>
        [HttpPut("{id}/togglestatus")]
        public async Task<IActionResult> ToggleAccountStatus(Guid id)
        {
            var isStatusToggled = await _accountRepository.ToggleAccountStatus(id);
            if (isStatusToggled)
            {
                return Ok("Account Status toggled successfully.");
            }
            return NotFound("Account not found.");
        }
    }
}
