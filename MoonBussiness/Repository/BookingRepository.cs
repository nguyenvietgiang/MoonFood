using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MoonBussiness.CommonBussiness.Dapper;
using MoonBussiness.Interface;
using MoonDataAccess;
using MoonModels;
using MoonModels.DTO.ResponseDTO;
using MoonModels.Paging;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoonBussiness.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDataAcess _dataAccess;
        public BookingRepository(DataContext dbContext, IMapper mapper, IDataAcess dataAccess)
        {
            _dbContext = dbContext;
            _dataAccess = dataAccess;
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
            return _mapper.Map<BookingResponse>(booking);
        }

        public async Task<List<BookingResponse>> GetBookingsForTable(Guid tableId)
        {
            // postgre sẽ tự tìm theo chữ cái viết thường
            string query = "SELECT * FROM \"Bookings\" WHERE \"TableId\" = @TableId;";
            var parameters = new { TableId = tableId };
            var bookings = await _dataAccess.GetData<Booking, dynamic>(query, parameters);
            var bookingResponses = _mapper.Map<List<BookingResponse>>(bookings.ToList());
            return bookingResponses;
        }

        public async Task<List<BookingResponse>> GetBookingsForUser(string username)
        {
            string query = @"
               SELECT 
                b.*, 
                ba.*
               FROM 
                ""Bookings"" b
                INNER JOIN 
                   ""Accounts"" ba ON b.""AccountId"" = ba.""Id""
                WHERE 
                ba.""Name"" = @Username;
             ";
            var parameters = new { Username = username };
            var bookings = await _dataAccess.GetData<Booking, dynamic>(query, parameters);
            var bookingResponses = _mapper.Map<List<BookingResponse>>(bookings);
            return bookingResponses;
        }

        public async Task<List<BookingResponse>> GetMyBookings(Guid userId)
        {
            string query = "SELECT * FROM \"Bookings\" WHERE \"AccountId\" = @AccountId;";
            var parameters = new { AccountId = userId }; 
            var bookings = await _dataAccess.GetData<Booking, dynamic>(query, parameters);
            var bookingResponses = _mapper.Map<List<BookingResponse>>(bookings.ToList());
            return bookingResponses;
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
