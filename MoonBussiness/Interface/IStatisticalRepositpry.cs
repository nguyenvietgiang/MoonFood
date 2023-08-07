
using MoonModels.DTO.ResponseDTO;

namespace MoonBussiness.Interface 
{
    public interface IStatisticalRepositpry
    {
        AccountStatistics GetAccountStatistics(DateTime? dateFilter = null);
        OrderStatistics GetOdersStatistics(DateTime? dateFilter = null); 
    }
}
