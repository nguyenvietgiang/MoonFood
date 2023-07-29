

namespace MoonModels
{
    public class OrderItem : BaseModel
    {
        public Guid? FoodId { get; set; } 
        public Food? Food { get; set; } 
        public Guid? ComboId { get; set; } 
        public Combo? Combo { get; set; } 
        public Guid? OrderId { get; set; } 
        public Order? Order { get; set; }
        public int TotalPrice { get; set; }
    }
}
