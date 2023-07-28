using MoonDataAccess;
using MoonModels;

namespace MoonBussiness.GraphQL
{
    public class FoodQuery
    {
        private readonly DataContext _dbContext;
        public FoodQuery(DataContext dbContext)
        { 
            _dbContext = dbContext; 
        }

        public List<Food> GetFoods()
        {
            return _dbContext.Foods.ToList();
        }
    }
}
