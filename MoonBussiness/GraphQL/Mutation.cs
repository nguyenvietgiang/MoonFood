using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;

namespace MoonBussiness.GraphQL
{
    public class Mutation
    {
        private readonly IAccountRepository _accountRepository;
        public Mutation(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse> AddAccount(CreateAccountRequest accountRequest)
        {
            return await _accountRepository.Add(accountRequest);
        }
    }
}
