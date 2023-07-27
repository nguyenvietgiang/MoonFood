using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.Paging;

namespace MoonBussiness.Interface
{
    public interface IFoodRepositorycs
    {
        Pagination<Food> GetMenu(int currentPage, int pageSize);
        Task<Food> CreateFood(CreateFoodRequest createFoodRequest, string host);
        Task<Food> GetbyId(Guid id);
        Task DeleteFood(Guid id);

    }
}
