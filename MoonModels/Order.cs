

namespace MoonModels
{
        public class Order : BaseModel
        {
            public Guid TableId { get; set; } 
            public Table Table { get; set; } 
            public List<OrderItem> OrderItems { get; set; } 
        }
    }

