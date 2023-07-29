
namespace MoonModels.DTO.RequestDTO
{
    public class OderRequest
    {
        public Guid TableId { get; set; }
        public List<OderItemRequest> OrderItems { get; set; }
    }

    public class OderItemRequest
    {
        public Guid? FoodId { get; set; }
        public Guid? ComboId { get; set; }
        public Guid? OrderId { get; set; }
        public int TotalPrice { get; set; }
    }
}
