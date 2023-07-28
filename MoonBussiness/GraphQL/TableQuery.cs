using MoonDataAccess;

namespace MoonBussiness.GraphQL
{
    public class TableQuery
    {
        private readonly DataContext _dbContext;
        public TableQuery(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

      
    }
}
