using Microsoft.AspNetCore.Http;

namespace MoonModels.DTO.RequestDTO
{
    public class ComboRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int Price { get; set; }
        public List<CreateFoodRequest> Foods { get; set; }
    } 
    public class CreateFoodRequest
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public int Price { get; set; }
        public FoodType Type { get; set; }
    }
}
