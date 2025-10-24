using AppForSEII2526.API.DTOs.MaintenancesDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        //used to enable your controller to access the database
        private readonly ApplicationDbContext _context;
        //usde to log any information when your system is running
        private readonly ILogger<BookingsController> _logger;


        public BookingsController(ApplicationDbContext context, ILogger<BookingsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookingDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetBooking(int id)
        {
            if (_context.Bookings == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }

            var booking = await _context.Bookings
                .Where(b =>b.Id ==id)
                    .Include(ap => ap.ApplicationUser)
                    .Include(b => b.BookingItems)

                        .ThenInclude(bi => bi.Maintenance)
                            .ThenInclude(maintenance => maintenance.MaintenanceType)

                .Select(b => new BookingDetailDTO(b.Id, b.Date, b.ApplicationUser.Name, b.ApplicationUser.Surname, b.ApplicationUser.Address,
                (PaymentMethodTypes)b.PaymentMethod, b.BookingItems 
                .Select(bi => new BookingItemDTO(bi.Maintenance.Id,bi.Maintenance.Name, bi.Maintenance.NumberOfDays, bi.Maintenance.Price, bi.Comment,bi.Maintenance.MaintenanceType.Type)).ToList<BookingItemDTO>()))
                .FirstOrDefaultAsync();

                if (booking == null)
                {
                    _logger.LogError($"Error: Booking with id {id} does not exist");
                    return NotFound();
                }

                return Ok(booking);



        }







    }
}
