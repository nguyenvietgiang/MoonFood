using MoonModels.Paging;

namespace MoonBussiness.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<Pagination<T>> GetAll(int currentPage, int pageSize);
    }
}
