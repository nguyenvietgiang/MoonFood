using AutoMapper;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly DataContext _dbContext;
         private readonly IMapper _mapper;
        public TableRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Table CreateTable(TableRequest tableRequest)
        {
            var table = _mapper.Map<Table>(tableRequest);
            table.CreatedAt = DateTime.UtcNow;
            table.UpdatedAt = DateTime.UtcNow;
            _dbContext.Tables.Add(table);
            _dbContext.SaveChanges();

            return table;
        }

        public bool DeleteTable(Guid tableId)
        {
            Table table = _dbContext.Tables.FirstOrDefault(u => u.Id == tableId);

            if (table == null)
                return false;

            _dbContext.Tables.Remove(table);
            _dbContext.SaveChanges();

            return true;
        }

        public Pagination<Table> GetTable(int currentPage, int pageSize)
        {
            var totalRecords = _dbContext.Tables.Count();
            var tables = _dbContext.Tables
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Pagination<Table>(tables, totalRecords, currentPage, pageSize);
        }
    }
}
