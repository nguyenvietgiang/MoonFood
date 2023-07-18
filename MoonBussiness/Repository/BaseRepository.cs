using Microsoft.EntityFrameworkCore;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Pagination<T>> GetAll(int currentPage, int pageSize)
        {
            var totalRecords = await _context.Set<T>().CountAsync();

            var content = await _context.Set<T>()
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<T>(content, totalRecords, currentPage, pageSize);
        }
    }
}
