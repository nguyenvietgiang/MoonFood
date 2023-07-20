
namespace MoonModels
{
    public class Table : BaseModel
    {
        public string? Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
