using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonModels.DTO.RequestDTO;

namespace MoonFood.Controllers
{
    [ApiController]
    [Route("api/v1/booking")]
    public class BookingController : BaseController
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        /// <summary>
        /// current user booking table
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddBooking(Guid tableId)
        {
            var AccountId = GetUserIdFromClaim();
            try
            {
                var booking = _bookingRepository.AddBooking(AccountId, tableId);
                return Ok(booking);
            }
            catch 
            {
                return BadRequest("Đàn đã được đặt trước đó rồi!");
            }
        }

        /// <summary>
        /// get booking infomation - admin,  manager
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        /// <summary>
        /// delete booking  - admin,  manager
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            await _bookingRepository.DeleteBooking(id);
            return NoContent(); 
        }

        /// <summary>
        /// get booking list  - admin,  manager
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetBookings(int currentPage = 1, int pageSize = 10)
        {
            var bookings = _bookingRepository.GetBooking(currentPage, pageSize);
            return Ok(bookings);
        }
    }
}

