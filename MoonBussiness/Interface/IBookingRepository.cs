using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.Interface
{
    public interface IBookingRepository
    {
        Pagination<BookingResponse> GetBooking(int currentPage, int pageSize);
        Task<BookingResponse> GetBookingByIdAsync(Guid id);
        BookingResponse AddBooking(Guid AccountId, Guid tableId);
        Task DeleteBooking(Guid id);
        Task<List<BookingResponse>> GetBookingsForTable(Guid tableId);
        Task<List<BookingResponse>> GetBookingsForUser(string username);
        Task<List<BookingResponse>> GetMyBookings(Guid userId);
    } 
}
