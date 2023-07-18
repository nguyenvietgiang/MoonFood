using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper= mapper;
        }

        public async Task<AccountResponse> Add(CreateAccountRequest accountRequest)
        {
            var account = _mapper.Map<Account>(accountRequest);
            account.CreatedAt = DateTime.UtcNow;
            account.UpdatedAt = DateTime.UtcNow;
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccountResponse>(account);
        }

        public Task<Pagination<Account>> GetAll(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountResponse> GetByEmail(string email)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> GetById(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> GetByName(string name)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);
            return _mapper.Map<AccountResponse>(account);
        }

        public Task<AccountResponse> Update(Account account)
        {
            throw new NotImplementedException();
        }

        public Pagination<AccountResponse> GetAllAccount(int currentPage, int pageSize)
        {
            var totalRecords = _context.Accounts.Count();
            var accounts = _context.Accounts
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var accountResponses = _mapper.Map<List<AccountResponse>>(accounts);
            return new Pagination<AccountResponse>(accountResponses, totalRecords, currentPage, pageSize);
        }

        public async Task DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }
}
