
namespace MoonModels
{
    public class Table : BaseModel
    {
        public string? Name { get; set; }
        public Guid? AccountId { get; set; } 
        public Account account { get; set; }
        public List<Order> Orders { get; set; }
    }
}
