using AppForSEII2526.API.DTOs.RentalDTO;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

                .Select(r => new RentalDetailDTO(r.Id, r.RentingDate, r.ApplicationUser.Name, r.ApplicationUser.Surname, r.ApplicationUser.Address,
                (PaymentMethodTypes)r.PaymentMethod, r.Startdate, r.Enddate,r.TotalPrice, r.RentalItems
                .Select(ri => new RentalItemDTO(ri.Car.Id, ri.Car.Model.Name, ri.Car.Manufacturer,ri.Car.RentingPrice,ri.Car.QuantityForRenting)).ToList<RentalItemDTO>()))
                .FirstOrDefaultAsync();

        if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }
    }

}




















//[HttpPost]
//        [Route("[Action]")]
//        [ProducesResponseType(typeof(CarDetailDTO),(int)HttpStatusCode.Created)]
//        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.BadRequest)]
//        [ProducesResponseType(typeof(string),(int)HttpStatusCode.Conflict)]

//        public async Task<IActionResult> CreatePurchase(RentalForCreateDTO purchaseForCreate)
//        {
//            if (purchaseForCreate.PurchaseItems.Count == 0)
//            {
//                ModelState.AddModelError("PurchaseItems", "The purchase must contain at least one item.");
//            }

//            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == purchaseForCreate.CustomerUserName);
//            if (user == null)
//            {
//                ModelState.AddModelError("CostumerUserName", "Username is not registered.");
//            }
//            if (ModelState.ErrorCount > 0)
//            {
//                return BadRequest("Invalid purchase data.");
//            }

//            var carTitles = RentalForCreate.RentalItems.Select(ri => ri.Title).ToList<string>();
//        }
//    }
//}
