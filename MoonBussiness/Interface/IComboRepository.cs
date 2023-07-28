using MoonModels.DTO.RequestDTO;
using MoonModels;
using MoonModels.Paging;

namespace MoonBussiness.Interface
{
    public interface IComboRepository
    {
        Task<Combo> CreateCombo(ComboRequest comboRequest, string host);
        Pagination<Combo> GetMenuCombo(int currentPage, int pageSize);
    }
}
