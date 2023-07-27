using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoonBussiness.CommonBussiness.Dapper;
using MoonBussiness.CommonBussiness.File;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class FoodRepository : IFoodRepositorycs
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDataAcess _dataAccess;
        private readonly IFileService _fileService;
        public FoodRepository(DataContext dbContext, IMapper mapper, IDataAcess dataAccess, IFileService fileService)
        {
            _dbContext = dbContext;
            _dataAccess = dataAccess;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Food> CreateFood(CreateFoodRequest createFoodRequest, string host)
        {
            string img = _fileService.SaveImage(createFoodRequest.Image,host);
            var food = _mapper.Map<Food>(createFoodRequest);
            food.CreatedAt = DateTime.UtcNow;
            food.UpdatedAt = DateTime.UtcNow;
            food.Image = img;
            _dbContext.Foods.Add(food);
            await _dbContext.SaveChangesAsync();
            return food;
        }

        public async Task DeleteFood(Guid id)
        {
            var food = await _dbContext.Foods.FindAsync(id);
            if (food != null)
            {
                _dbContext.Foods.Remove(food);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Food> GetbyId(Guid id)
        {
            string query = "SELECT * FROM \"Foods\" WHERE \"Id\" = @Id;";
            var parameters = new { Id = id };
            var foods = await _dataAccess.GetData<Food, dynamic>(query, parameters);
            var food = foods.FirstOrDefault();
            return food;
        }

        public Pagination<Food> GetMenu(int currentPage, int pageSize)
        {
            var totalRecords = _dbContext.Foods.Count();
            var menus = _dbContext.Foods 
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Pagination<Food>(menus, totalRecords, currentPage, pageSize);
        }


    }
}
