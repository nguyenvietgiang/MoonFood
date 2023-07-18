namespace MoonModels
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}