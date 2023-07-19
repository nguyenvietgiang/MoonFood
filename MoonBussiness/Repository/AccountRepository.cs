﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MoonBussiness.CommonBussiness.Auth;
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
        private readonly IAuthService _authService;
        public AccountRepository(DataContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper= mapper;
            _authService= authService;
        }

        public async Task<AccountResponse> Add(CreateAccountRequest accountRequest)
        {
            string hashedPassword = _authService.HashPassword(accountRequest.Password);
            var account = _mapper.Map<Account>(accountRequest);
            account.Password = hashedPassword;
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

        public LoginResponse Login(LoginRequest loginRequest)
        {
            string hashedPassword = _authService.HashPassword(loginRequest.Password);

            Account account = _context.Accounts.FirstOrDefault(u => u.Name == loginRequest.Username && u.Password == hashedPassword);

            if (account == null)
            {
                return null;
            }
            if (account.Status = false)
            {
                return null;
            }    
            string token = _authService.GenerateJwtToken(account);

            LoginResponse loginResponse = _mapper.Map<LoginResponse>(account);
            loginResponse.Token = token;
            return loginResponse;
        }

        public AccountResponse PatchAccount(Guid id, JsonPatchDocument<Account> patchDocument)
        {
            Account existingAccount = _context.Accounts.FirstOrDefault(u => u.Id == id);

            if (existingAccount != null)
            {
                patchDocument.ApplyTo(existingAccount);
                _context.SaveChanges();

                return _mapper.Map<AccountResponse>(existingAccount);
            }

            return null;
        }

        public bool ChangePassword(Guid Id, ChangePassword model)
        {
            Account account = _context.Accounts.FirstOrDefault(u => u.Id == Id);

            if (account == null)
                return false;

            string hashedCurrentPassword = _authService.HashPassword(model.CurrentPassword);

            if (account.Password != hashedCurrentPassword)
                return false;

            string hashedNewPassword = _authService.HashPassword(model.NewPassword);
            account.Password = hashedNewPassword;
            account.UpdatedAt= DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> ChangeAccountTypeById(Guid id, string newType)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            if (Enum.TryParse(newType, true, out AccountType parsedType))
            {
                account.Type = parsedType;
                account.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> ToggleAccountStatus(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;
            // Đảo ngược giá trị Status
            account.Status = !account.Status;
            account.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }
    }
}
