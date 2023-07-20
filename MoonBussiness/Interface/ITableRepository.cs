using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.Paging;
using Newtonsoft.Json;

namespace MoonBussiness.Interface
{
    public interface ITableRepository
    {
        Pagination<Table> GetTable(int currentPage, int pageSize);
        Table CreateTable(TableRequest tableRequest);
        bool DeleteTable(Guid tableId);
    }
}
