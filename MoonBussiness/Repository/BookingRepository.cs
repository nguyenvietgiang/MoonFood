using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;

namespace MoonBussiness.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public BookingRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookingResponse AddBooking(Guid AccountId, Guid tableId)
        {
            var table = _dbContext.Tables.FirstOrDefault(t => t.Id == tableId);
            if (!table.Status)
            {
                return null;
            }
            var booking = new Booking();
            booking.AccountId = AccountId;
            booking.TableId = tableId;
            booking.CreatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;
            booking.Status = true;
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
            UpdateTableStatus(tableId, false);
            return _mapper.Map<BookingResponse>(booking);
        }

        public async Task DeleteBooking(Guid id) 
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking);
                UpdateTableStatus(booking.TableId, true);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Pagination<BookingResponse> GetBooking(int currentPage, int pageSize)
        {
            var totalRecords = _dbContext.Tables.Count();
            var booking = _dbContext.Bookings
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var bookingResponses = _mapper.Map<List<BookingResponse>>(booking);
            return new Pagination<BookingResponse>(bookingResponses, totalRecords, currentPage, pageSize);
        }
 

        public async Task<BookingResponse> GetBookingByIdAsync(Guid id)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<BookingResponse>(booking); ;
        }

        private void UpdateTableStatus(Guid tableId, bool status)
        {
            var table = _dbContext.Tables.FirstOrDefault(t => t.Id == tableId);
            if (table != null)
            {
                table.Status = status;
                table.UpdatedAt = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
        }
    }
}
