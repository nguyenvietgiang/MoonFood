

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
        Task<AccountResponse> Update(Account account);
        Pagination<AccountResponse> GetAllAccount(int currentPage, int pageSize);
        Task DeleteAccount(Guid id);
    }
}
