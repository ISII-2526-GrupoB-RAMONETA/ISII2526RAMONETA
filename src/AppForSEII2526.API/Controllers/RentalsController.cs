using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(ApplicationDbContext context, ILogger<RentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //DETAILS RENTAL

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRental(int id)
        {
            // Verifica que exista la Rentals Bookings
            if (_context.Rentals == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }



            var rental = await _context.Rentals
                    .Where(r => r.Id == id)
                        .Include(ap => ap.ApplicationUser) //Usuario asociado a la reserva
                        .Include(r => r.RentalItems) // Items del alquiler
                            .ThenInclude(ri => ri.Car) // Coche por item
                                .ThenInclude(c => c.Model) // Modelo del coche

                    .Select(r => new RentalDetailDTO(r.Id, r.RentalDate, r.ApplicationUser.UserName, r.ApplicationUser.Name, r.ApplicationUser.Surname, r.ApplicationUser.Address, r.DeliveryCarDealer,
                    (PaymentMethodTypes)r.PaymentMethod, r.RentalDateFrom, r.RentalDateTo, r.RentalItems
                    .Select(ri => new RentalItemDTO(ri.Car.Id, ri.Car.Model.Name, ri.Car.Manufacturer, ri.Car.RentingPrice, ri.Quantity)).ToList<RentalItemDTO>()))
                    .FirstOrDefaultAsync();

            // Si no se encuentra el alquiler -> 404
            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }
            // Devuelve el alquiler en un 200 OK
            return Ok(rental);
        }


        //POST RENTAL

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {
            //any validation defined in RentalForCreate is checked before running the method so they don't have to be checked again
            if (rentalForCreate.RentalDateFrom <= DateTime.Now)
                ModelState.AddModelError("RentalDateFrom", "Error! Your rental date must start later than today");

            if (rentalForCreate.RentalDateFrom >= rentalForCreate.RentalDateTo)
                ModelState.AddModelError("RentalDateFrom&RentalDateTo", "Error! Your rental must end later than it starts");

            if (rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! You must include at least one car to be rented");

            // if (!_context.ApplicationUsers.Any(au=>au.UserName==rentalForCreate.CustomerUserName))
            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == rentalForCreate.CustomerUserName);
            if (user == null)
                ModelState.AddModelError("RentalApplicationUser", "Error! UserName is not registered");


            if (!rentalForCreate.DeliveryAddress.ToLower().Contains("calle"))
                ModelState.AddModelError("RentaldeliveryAddress", "Error! La dirección de envío debe empezar por la palabra Calle");


            if (ModelState.ErrorCount > 0) //Si hay algun error se devuelve una BadRequest
                return BadRequest(new ValidationProblemDetails(ModelState));


            var carIds = rentalForCreate.RentalItems.Select(ri => ri.CarId).ToList<int>();  // Obtiene solo los ids de los coches

            // Obtener, para cada coche, cuántas unidades están ya rentadas EN LAS FECHAS SOLICITADAS (solapamiento)
            var cars = _context.Cars
                .Include(c => c.RentalItems)
                    .ThenInclude(ri => ri.Rental)
                        .Where(c => carIds.Contains(c.Id))
                            .Select(c => new {
                                //coger atributos de la bbdd creada para comprobar las condiciones
                                c.Id,
                                c.Model,
                                c.QuantityForRenting,
                                c.RentingPrice,
                                NumberOfRentedItems = c.RentalItems
                                    .Where(ri => ri.Rental.RentalDateFrom <= rentalForCreate.RentalDateTo
                                                 && ri.Rental.RentalDateTo >= rentalForCreate.RentalDateFrom)
                                    .Sum(ri => ri.Quantity)
                            })
                            .ToList();


            Rental rental = new Rental(user, rentalForCreate.PaymentMethod, rentalForCreate.RentalDate, rentalForCreate.DeliveryCarDealer,
                                rentalForCreate.RentalDateFrom, rentalForCreate.RentalDateTo, new List<RentalItem>());

            rental.TotalPrice = 0;
            var numDays = (rental.RentalDateTo - rental.RentalDateFrom).TotalDays;


            foreach (var item in rentalForCreate.RentalItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarId); 
                if (car == null || (car.NumberOfRentedItems >= car.QuantityForRenting)) //No existe o está agotado
                {
                    ModelState.AddModelError("RentalItems", $"Error! Car Model '{item.Model}' is not available for being rented from the database");
                }

                else
                {
                    int stock = car.QuantityForRenting - car.NumberOfRentedItems;
                    if (item.Quantity > stock) //No hay stock suficiente
                    {
                        ModelState.AddModelError("PurchaseItems", $"Error! Not enough stock for Car Id '{item.CarId}'. Available: {stock}, Requested: {item.Quantity}");
                    }
                    else
                    {
                        rental.RentalItems.Add(new RentalItem(car.Id, rental.Id, item.Quantity));
                        item.PriceForRenting = car.RentingPrice;
                    }

                }
            }

            rental.TotalPrice = rentalForCreate.RentalItems.Sum(pi => pi.PriceForRenting * pi.Quantity * (decimal)numDays);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            ////decimal totalCost = rentalForCreate.RentalItems.Sum(
            ////    pi => pi.PriceForRenting * pi.Quantity * (decimal)numDays
            ////);

            ////rental.TotalPrice = totalCost;

            //rental.ApplicationUser = user!;
            _context.Add(rental);

            try
            {
                //we store in the database both rental and its rentalitems
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", $"Error! There was an error while saving your rental, please, try again later");
                return Conflict("Error" + ex.Message);

            }

            //it returns rentalDetail
            var rentalDetail = new RentalDetailDTO(rental.Id, rental.RentalDate, rental.ApplicationUser.UserName, rental.ApplicationUser.Name, rental.ApplicationUser.Surname, rental.ApplicationUser.Address,
                rentalForCreate.DeliveryCarDealer, rentalForCreate.PaymentMethod,
                rental.RentalDateFrom, rental.RentalDateTo,
                rentalForCreate.RentalItems);

            return CreatedAtAction("GetRental", new { id = rental.Id }, rentalDetail);
        }
    }

}
