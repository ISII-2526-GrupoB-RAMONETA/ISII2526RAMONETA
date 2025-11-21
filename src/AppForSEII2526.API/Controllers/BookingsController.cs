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
        //used to log any information when your system is running
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
            // Verifica que exista la tabla Bookings
            if (_context.Bookings == null)
            {
                _logger.LogError("Error: Bookings table does not exist");
                return NotFound();
            }

            // Recupera una reserva incluyendo:
            // - el usuario
            // - los booking items
            // - el mantenimiento de cada item
            // - el tipo de mantenimiento
            var booking = await _context.Bookings
                .Where(b => b.Id == id)
                    .Include(ap => ap.ApplicationUser) // Usuario asociado a la reserva
                    .Include(b => b.BookingItems) // Items de la reserva

                        .ThenInclude(bi => bi.Maintenance) // Mantenimiento por item
                            .ThenInclude(maintenance => maintenance.MaintenanceType) // Tipo de mantenimiento

                .Select(b => new BookingDetailDTO(b.Id, b.Date, b.ApplicationUser.UserName,b.ApplicationUser.Name, b.ApplicationUser.Surname, b.ApplicationUser.Address,
                (PaymentMethodTypes)b.PaymentMethod, b.ApplicationUser.PhoneNumber, b.BookingItems
                .Select(bi => new BookingItemDTO(bi.Maintenance.Id, bi.Maintenance.Name, bi.Maintenance.NumberOfDays, bi.Maintenance.Price, bi.Comment, bi.Maintenance.MaintenanceType.Type)).ToList<BookingItemDTO>()))
                .FirstOrDefaultAsync();

            // Si no se encuentra la reserva -> 404
            if (booking == null)
            {
                _logger.LogError($"Error: Booking with id {id} does not exist");
                return NotFound();
            }
            // Devuelve la reserva en un 200 OK
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

            //Si es número de teléfono no es nulo y no empieza por +34 salta el error
            if((bookingForCreate.PhoneNumber != null) && (!bookingForCreate.PhoneNumber.StartsWith("+34")))
                ModelState.AddModelError("BookingApplicationUser", "Error! el teléfono debe empezar por +34");

            if (ModelState.ErrorCount > 0) //Si hay algun error se devuelve una BadRequest
                return BadRequest(new ValidationProblemDetails(ModelState));

            var maintenanceNames = bookingForCreate.BookingItems.Select(bi => bi.Name).ToList<string>(); // Obtiene solo los nombres de los mantenimientos

            // Busca los mantenimientos en la base de datos
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

            // Crea un nuevo Booking vacío con el usuario y la fecha actual
            Booking booking = new Booking(DateTime.Now, (PaymentMethodTypes)bookingForCreate.PaymentMethod,
                new List<BookingItem>(), user);

            // Recorre cada item enviado por el usuario y lo valida
            foreach (var item in bookingForCreate.BookingItems)
            {
                var maintenance = maintenances.FirstOrDefault(m => m.Name == item.Name);
                // Si el mantenimiento no existe da error
                if (maintenance == null)
                {
                    ModelState.AddModelError("BookingItems", $"Error! Maintenance named '{item.Name}' is not available for being booked");

                }
                else
                {
                    // Crea un BookingItem válido y actualiza el precio total y el número de días total
                    booking.BookingItems.Add(new BookingItem(maintenance.Id, booking, item.Comment));
                    booking.TotalPrice += maintenance.Price;
                    booking.TotalNumberOfDays += maintenance.NumberOfDays;


                }

                

            }




            // Si hubo errores en los items da error 400
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            // Guarda la reserva
            _context.Add(booking);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) // Error al guardar en base de datos
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Booking", $"Error! There was an error while saving your booking, plese, try again later");
                return Conflict("Error" + ex.Message);

            }
            // Crea el DTO final para devolverlo
            var bookingDetail = new BookingDetailDTO(booking.Id, booking.Date, booking.ApplicationUser.UserName,booking.ApplicationUser.Name, booking.ApplicationUser.Surname,
                booking.ApplicationUser.Address, bookingForCreate.PaymentMethod,booking.ApplicationUser.PhoneNumber, bookingForCreate.BookingItems);

            // Devuelve 201 Created indicando con el DTO de la compra hecha
            return CreatedAtAction("GetBooking", new { id = booking.Id }, bookingDetail);
        }
    }
}