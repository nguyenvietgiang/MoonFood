﻿using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonBussiness.Repository
{
    public class StatisticalRepository : IStatisticalRepositpry
    {
        protected readonly DataContext _dbContext;

        public StatisticalRepository(DataContext context) { _dbContext = context; }
        public AccountStatistics GetAccountStatistics(DateTime? dateFilter = null)
        {
            var statistics = new AccountStatistics();

            IQueryable<Account> accountsQuery = _dbContext.Accounts;

            // Apply date filter if provided
            if (dateFilter.HasValue)
            {
                accountsQuery = accountsQuery.Where(a => a.CreatedAt.Date == dateFilter.Value.Date);
            }

            statistics.TotalAccounts = accountsQuery.Count();
            statistics.UserAccountsCount = accountsQuery.Count(a => a.Type == AccountType.User);
            statistics.AdminAccountsCount = accountsQuery.Count(a => a.Type == AccountType.Admin);
            statistics.ManagerAccountsCount = accountsQuery.Count(a => a.Type == AccountType.Manager);
            statistics.ActiveAccountsCount = accountsQuery.Count(a => a.Status == true);
            statistics.LockedAccountsCount = accountsQuery.Count(a => a.Status == false);

            // Calculate percentage of locked and active accounts
            if (statistics.TotalAccounts > 0)
            {
                statistics.PercentageLockedAccounts = (statistics.LockedAccountsCount * 100) / statistics.TotalAccounts;
                statistics.PercentageActiveAccounts = (statistics.ActiveAccountsCount * 100) / statistics.TotalAccounts;
            }

            return statistics;
        }

    }
}