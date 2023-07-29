
namespace MoonModels.DTO.ResponseDTO
{
    public class OderResponse
    {
        public Guid id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TableId { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }

    public class OrderItemResponse
    {
        public Guid id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? FoodId { get; set; }
        public Guid? ComboId { get; set; }
        public Guid? OrderId { get; set; }
        public int TotalPrice { get; set; }
    }
}
