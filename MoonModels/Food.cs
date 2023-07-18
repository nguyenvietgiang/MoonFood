
namespace MoonModels
{
    public class Food : BaseModel
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public FoodType Type { get; set; }
    }

    public enum FoodType
    {
        MainCourse,
        SideDish,
        FastFood,
        Beverage,
        Dessert
    }
}
