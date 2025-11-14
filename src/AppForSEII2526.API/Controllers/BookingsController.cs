using AppForSEII2526.API.DTOs.MaintenancesDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
                _logger.LogError("Error: Bookings table does not exist");
                return NotFound();
            }

            var booking = await _context.Bookings
                .Where(b => b.Id == id)
                    .Include(ap => ap.ApplicationUser)
                    .Include(b => b.BookingItems)

                        .ThenInclude(bi => bi.Maintenance)
                            .ThenInclude(maintenance => maintenance.MaintenanceType)

                .Select(b => new BookingDetailDTO(b.Id, b.Date, b.ApplicationUser.UserName,b.ApplicationUser.Name, b.ApplicationUser.Surname, b.ApplicationUser.Address,
                (PaymentMethodTypes)b.PaymentMethod, b.ApplicationUser.PhoneNumber, b.BookingItems
                .Select(bi => new BookingItemDTO(bi.Maintenance.Id, bi.Maintenance.Name, bi.Maintenance.NumberOfDays, bi.Maintenance.Price, bi.Comment, bi.Maintenance.MaintenanceType.Type)).ToList<BookingItemDTO>()))
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                _logger.LogError($"Error: Booking with id {id} does not exist");
                return NotFound();
            }

            return Ok(booking);



        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BookingDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateBooking(BookingForCreateDTO bookingForCreate)
        {
            if (bookingForCreate.BookingItems.Count == 0) //Si no se mete ninguna reserva salta error
                ModelState.AddModelError("ErrorBookingItems", "Error! You must include at least one maintenance to be booked");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == bookingForCreate.CustomerUserName);
            if (user == null) //Si no exite el usuario salta el error
                ModelState.AddModelError("BookingApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0) //Si hay algun error se devuelve una BadRequest
                return BadRequest(new ValidationProblemDetails(ModelState));

            var maintenanceNames = bookingForCreate.BookingItems.Select(bi => bi.Name).ToList<string>();

            var maintenances = _context.Maintenances.Include(m => m.BookingItems)
                .ThenInclude(bi => bi.Booking)
                .Where(m => maintenanceNames.Contains(m.Name))
                .Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.NumberOfDays,
                    m.Price


                })
                .ToList();

            Booking booking = new Booking(DateTime.Now, (PaymentMethodTypes)bookingForCreate.PaymentMethod,
                new List<BookingItem>(), user);

            foreach (var item in bookingForCreate.BookingItems)
            {
                var maintenance = maintenances.FirstOrDefault(m => m.Name == item.Name);

                if (maintenance == null)
                {
                    ModelState.AddModelError("BookingItems", $"Error! Maintenance named '{item.Name}' is not available for being booked");

                }
                else
                {
                    booking.BookingItems.Add(new BookingItem(maintenance.Id, booking, item.Comment));
                    booking.TotalPrice += maintenance.Price;
                    booking.TotalNumberOfDays += maintenance.NumberOfDays;


                }

                

            }
            




            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(booking);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Booking", $"Error! There was an error while saving your booking, plese, try again later");
                return Conflict("Error" + ex.Message);

            }

            var bookingDetail = new BookingDetailDTO(booking.Id, booking.Date, booking.ApplicationUser.UserName,booking.ApplicationUser.Name, booking.ApplicationUser.Surname,
                booking.ApplicationUser.Address, bookingForCreate.PaymentMethod,booking.ApplicationUser.PhoneNumber, bookingForCreate.BookingItems);
    

            return CreatedAtAction("GetBooking", new { id = booking.Id }, bookingDetail);
        }
    }
}