using System.ComponentModel.DataAnnotations;

namespace MoonModels.Paging
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
