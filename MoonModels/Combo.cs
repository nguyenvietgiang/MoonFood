
namespace MoonModels
{
    public class Combo : BaseModel
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public List<Food> Foods { get; set; } 
    }
}
