using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.ResponseDTO;

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

        public OrderStatistics GetOdersStatistics(DateTime? dateFilter = null)
        {
            var statistics = new OrderStatistics();

            IQueryable<Order> ordersQuery = _dbContext.Orders;

            // Apply date filter if provided
            if (dateFilter.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.CreatedAt.Date == dateFilter.Value.Date);
            }

            statistics.TotalSales = ordersQuery.Sum(o => o.OrderItems.Sum(oi => oi.TotalPrice));

            // Find the most frequent UserId and FoodId
            var mostFrequentUserId = ordersQuery
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.OrderId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();

            var mostFrequentFoodId = ordersQuery
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.FoodId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();

            statistics.MostFrequentUserId = (Guid)mostFrequentUserId;
            statistics.MostFrequentFoodId = (Guid)mostFrequentFoodId;

            return statistics;
        }
    }
}
