namespace MoonModels
{
    public class Account : BaseModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public AccountType Type { get; set; }
        public List<Booking> Bookings { get; set; }
    }

    public enum AccountType
    {
        User,
        Admin,
        Manager
    }
}
