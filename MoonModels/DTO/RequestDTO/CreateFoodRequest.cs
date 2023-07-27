using Microsoft.AspNetCore.Http;

namespace MoonModels.DTO.RequestDTO
{
    public class CreateFoodRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public int Price { get; set; }
        public FoodType Type { get; set; }
    }
}
