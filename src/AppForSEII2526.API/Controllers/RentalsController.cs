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
                .Where(r =>r.Id ==id)
                    .Include(ap => ap.ApplicationUser) //nombre, apellido, direccion
                    .Include(r => r.RentalItems) //
                        .ThenInclude(ri => ri.Car)
                            //.ThenInclude(Rental => Rental.)

                .Select(r => new RentalDetailDTO(r.Id, r.RentalDate, r.ApplicationUser.UserName,r.ApplicationUser.Name, r.ApplicationUser.Surname, r.ApplicationUser.Address,r.DeliveryCarDealer,
                (PaymentMethodTypes)r.PaymentMethod, r.RentalDateFrom, r.RentalDateTo, r.RentalItems
                .Select(ri => new RentalItemDTO(ri.Car.Id, ri.Car.Model.Name, ri.Car.Manufacturer,ri.Car.RentingPrice,ri.Quantity,ri.Description)).ToList<RentalItemDTO>()))
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
    //any validation defined in PurchaseForCreate is checked before running the method so they don't have to be checked again
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

    var cars = _context.Cars.Include(m => m.RentalItems)
        .ThenInclude(ri => ri.Rent)
        .Where(m => carIds.Contains(m.Id))
        .Select(m => new {
            CarId = m.Id, // Agrega el Id del coche
            QuantityForRenting = m.QuantityForRenting,
            RentingPrice = m.RentingPrice,
            NumberOfRentedItems = m.RentalItems.Count(ri => ri.Rent.RentalDateFrom <= rentalForCreate.RentalDateTo
                    && ri.Rent.RentalDateTo >= rentalForCreate.RentalDateFrom)
        })
        .ToList();


    Rental rental = new Rental(rentalForCreate.PaymentMethod, DateTime.Now, rentalForCreate.TotalPrice,rentalForCreate.DeliveryCarDealer,
                        
                        rentalForCreate.RentalDateFrom, rentalForCreate.RentalDateTo, new List<RentalItem>());

            rental.TotalPrice = 0;
    var numDays = (rental.RentalDateTo - rental.RentalDateFrom).TotalDays;


    foreach (var item in rentalForCreate.RentalItems)
    {
        var car = cars.FirstOrDefault(m => m.CarId == item.CarId); // Usa CarId en vez de carId
        // Debes ajustar los nombres de las propiedades para que coincidan con la proyección anónima
        if ((car == null)) //|| (car.NumberOfRentedItems >= car.QuantityForRenting))
        {
            ModelState.AddModelError("RentalItems", $"Error! Car with id '{item.CarId}' is not available for being rented from {rentalForCreate.RentalDateFrom.ToShortDateString()} to {rentalForCreate.RentalDateTo.ToShortDateString()}");
        }
        else
        {
            rental.RentalItems.Add(new RentalItem(car.CarId, rental, car.RentingPrice, item.Description));
            item.PriceForRenting = car.RentingPrice;
        }
    }
    rental.TotalPrice = rental.RentalItems.Sum(ri => ri.PriceForRenting * (decimal)numDays);


    //if there is any problem because of the available quantity of movies or because the movie does not exist
    if (ModelState.ErrorCount > 0)
    {
        return BadRequest(new ValidationProblemDetails(ModelState));
    }

            rental.ApplicationUser = user!;
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
    var rentalDetail = new RentalDetailDTO(rental.Id, rental.RentalDate, rental.ApplicationUser.UserName, rental.ApplicationUser.Name, rental.ApplicationUser.Surname,rental.ApplicationUser.Address,
        rentalForCreate.DeliveryCarDealer,rentalForCreate.PaymentMethod,
        rental.RentalDateFrom, rental.RentalDateTo,
        rentalForCreate.RentalItems);

    return CreatedAtAction("GetRental", new { id = rental.Id }, rentalDetail);
}
    }

}
