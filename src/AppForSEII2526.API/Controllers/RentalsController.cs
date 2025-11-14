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
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(ApplicationDbContext context, ILogger<RentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRental(int id)
        {
            if (_context.Rentals == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }

            var rental = await _context.Rentals
                    .Where(r => r.Id == id)
                        .Include(ap => ap.ApplicationUser) //nombre, apellido, direccion
                        .Include(r => r.RentalItems) //
                            .ThenInclude(ri => ri.Car)
                    //.ThenInclude(Rental => Rental.)

                    .Select(r => new RentalDetailDTO(r.Id, r.RentalDate, r.ApplicationUser.UserName, r.ApplicationUser.Name, r.ApplicationUser.Surname, r.ApplicationUser.Address, r.DeliveryCarDealer,
                    (PaymentMethodTypes)r.PaymentMethod, r.RentalDateFrom, r.RentalDateTo, r.RentalItems
                    .Select(ri => new RentalItemDTO(ri.Car.Id, ri.Car.Model.Name, ri.Car.Manufacturer, ri.Car.RentingPrice, ri.Quantity)).ToList<RentalItemDTO>()))
                    .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }



        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {
            //any validation defined in RentalForCreate is checked before running the method so they don't have to be checked again
            if (rentalForCreate.RentalDateFrom <= DateTime.Today)
                ModelState.AddModelError("RentalDateFrom", "Error! Your rental date must start later than today");

            if (rentalForCreate.RentalDateFrom >= rentalForCreate.RentalDateTo)
                ModelState.AddModelError("RentalDateFrom&RentalDateTo", "Error! Your rental must end later than it starts");

            if (rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! You must include at least one car to be rented");

            // if (!_context.ApplicationUsers.Any(au=>au.UserName==rentalForCreate.CustomerUserName))
            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == rentalForCreate.CustomerUserName);
            if (user == null)
                ModelState.AddModelError("RentalApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var carIds = rentalForCreate.RentalItems.Select(ri => ri.CarId).ToList<int>();

            // Obtener, para cada coche, cuántas unidades están ya rentadas EN LAS FECHAS SOLICITADAS (solapamiento)
            var cars = _context.Cars
                .Include(m => m.RentalItems)
                    .ThenInclude(ri => ri.Rental)
                .Where(m => carIds.Contains(m.Id))
                .Select(m => new {
                    m.Id,
                    m.QuantityForRenting,
                    m.RentingPrice,
                    NumberOfRentedItems = m.RentalItems
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
                var car = cars.FirstOrDefault(m => m.Id == item.CarId); // Usa CarId en vez de carId
                if (car == null)
                {
                    ModelState.AddModelError("RentalItems", $"Error! Car Model '{item.Model}' is not available for being rented from the database");
                    continue;
                }

                int stock = car.QuantityForRenting - car.NumberOfRentedItems;
                if (stock <= 0)
                {
                    ModelState.AddModelError("RentalItems", $"Error! Car Model '{item.Model}' has no available units for the selected dates.");
                }
                else if (item.Quantity > stock)
                {
                    ModelState.AddModelError("RentalItems", $"Error! Not enough stock for Car Model '{item.Model}'. Available: {stock}, Requested: {item.Quantity}");
                }
                else
                {
                    rental.RentalItems.Add(new RentalItem(car.Id, rental.Id, item.Quantity));
                    item.PriceForRenting = car.RentingPrice;
                }
            }

            // Reemplazado: evitar acceder a ri.Car (navegación null). Usar los DTOs ya rellenados.
            rental.TotalPrice = rentalForCreate.RentalItems.Sum(pi => pi.PriceForRenting * pi.Quantity * (decimal)numDays);

            //if there is any problem because of the available quantity of cars or because the car does not exist
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
