
namespace MoonBussiness.Interface 
{
    public interface IStatisticalRepositpry
    {
        AccountStatistics GetAccountStatistics(DateTime? dateFilter = null);
    }
}
