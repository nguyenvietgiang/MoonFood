namespace MoonModels.DTO.ResponseDTO
{
    public class BookingResponse: BaseModel
    {
        public Guid AccountId { get; set; }
        public Guid TableId { get; set; } 
    }
}
