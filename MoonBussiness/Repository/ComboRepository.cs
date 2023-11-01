using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoonBussiness.CommonBussiness.File;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class ComboRepository : IComboRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IFoodRepositorycs _foodRepositorycs;
        public ComboRepository(DataContext dataContext, IMapper mapper, IFileService fileService, IFoodRepositorycs foodRepositorycs ) 
        {
            _dbContext= dataContext;
            _mapper= mapper;
            _fileService= fileService;
            _foodRepositorycs= foodRepositorycs;
        }
        public async Task<Combo> CreateCombo(ComboRequest comboRequest, string host)
        {
            string img = _fileService.SaveImage(comboRequest.Image, host);
            var combo = _mapper.Map<Combo>(comboRequest);
            combo.Image = img;
            combo.CreatedAt= DateTime.UtcNow;
            combo.UpdatedAt= DateTime.UtcNow;
            var foods = new List<Food>();
            foreach (var foodRequest in comboRequest.Foods) 
            {
                var food = await _foodRepositorycs.CreateFood(foodRequest, host);
                foods.Add(food);
            }
            combo.Foods = foods;
            _dbContext.Combos.Add(combo);
            await _dbContext.SaveChangesAsync();
            return combo;
        }

        public Pagination<Combo> GetMenuCombo(int currentPage, int pageSize)
        {
            var totalRecords = _dbContext.Combos.AsNoTracking().Count();
            var menus = _dbContext.Combos.AsNoTracking()
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Pagination<Combo>(menus, totalRecords, currentPage, pageSize);
        }
    }
}
