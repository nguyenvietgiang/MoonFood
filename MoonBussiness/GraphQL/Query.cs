using HotChocolate.AspNetCore.Authorization;
using MoonBussiness.Interface;
using MoonModels;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.GraphQL
{
    public class Query
    {
        private readonly IFoodRepositorycs foodRepositorycs;
        private readonly IAccountRepository accountRepository;
        public Query(IFoodRepositorycs foodRepositorycs, IAccountRepository accountRepository)
        { 
            this.foodRepositorycs = foodRepositorycs;
            this.accountRepository = accountRepository;
        }
        // get all food
        public Pagination<Food> GetFoods(int currentPage, int pageSize)
        {
            var page = foodRepositorycs.GetMenu(currentPage, pageSize);
            return page;
        }
        // get all account
        [Authorize]
        public Pagination<AccountResponse> GetAccounts(int currentPage, int pageSize)
        {
            var page = accountRepository.GetAllAccount(currentPage, pageSize);
            return page;
        }

        
    }
}
