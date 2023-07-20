
namespace MoonModels
{
    public class Booking : BaseModel
    {
        public Guid AccountId { get; set; }
        public Guid TableId { get; set; }
        public Account Account { get; set; } 
        public Table Table { get; set; } 
    }
}
