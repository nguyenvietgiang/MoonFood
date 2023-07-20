using Microsoft.AspNetCore.JsonPatch;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO; 
using MoonModels.Paging; 

namespace MoonBussiness.Interface
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<AccountResponse> GetByEmail(string email);
        Task<AccountResponse> GetByName(string name);
        Task<AccountResponse> GetById(Guid id);
        Task<AccountResponse> Add(CreateAccountRequest accountRequest);
        Pagination<AccountResponse> GetAllAccount(int currentPage, int pageSize);
        AccountResponse PatchAccount(Guid id, JsonPatchDocument<Account> patchDocument);
        Task DeleteAccount(Guid id);
        Task<int> DeleteAccountsAsync(List<Guid> accountIds);
        LoginResponse Login(LoginRequest loginRequest);
        Task<bool> ResetPasswordAsync(string email);
        bool ChangePassword(Guid Id, ChangePassword model);
        Task<bool> ChangeAccountTypeById(Guid id, string newType);
        Task<bool> ToggleAccountStatus(Guid id);
        string RefreshAccessToken(string refreshToken);
    }
}
