using AppForSEII2526.API.DTOs.PurchasesDTO;
using AppForSEII2526.API.DTOs.RentalDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PurchasesController> _logger;

        public PurchasesController(ApplicationDbContext context, ILogger<PurchasesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //DETAILS PURCHASE

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPurchase(int id)
        {
            // Check if Purchases table exists
            if (_context.Purchases == null)
            {
                _logger.LogError("Error: Purchases table does not exist");
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Where(p => p.Id == id)
                    .Include(ap => ap.ApplicationUser)
                    .Include(p => p.PurchaseItems)
                     .ThenInclude(pi => pi.Car)
                        .ThenInclude(c => c.Model)
                .Select(p => new PurchaseDetailDTO(p.Id, p.PurchasingDate, p.ApplicationUser.Name, p.ApplicationUser.Surname, p.ApplicationUser.Address,p.ApplicationUser.UserName,(PaymentMethodTypes)p.PaymentMethod,
                    p.PurchaseItems.Select(pi => new PurchaseItemDTO(pi.Car.Id, pi.Car.Model.Name, pi.Car.PurchasingPrice, pi.Car.Color, pi.Quantity)).ToList<PurchaseItemDTO>())).FirstOrDefaultAsync();

            if (purchase == null)
            {
                _logger.LogError($"Error: Purchase with id {id} does not exist");
                return NotFound();
            }

            return Ok(purchase);

        }

        //POST PURCHASE
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreatePurchase(PurchaseForCreateDTO purchaseForCreate)
        {
            if (purchaseForCreate.PurchaseItems.Count == 0)
            {
                ModelState.AddModelError("PurchaseItems", "Error! You must include at least one car to be purchased");
            }

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == purchaseForCreate.UserName);
            if (user == null)
                ModelState.AddModelError("PurchaseApplicationUser", "Error! UserName is not registered");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var carModels = purchaseForCreate.PurchaseItems.Select(pi => pi.Model).ToList<string>(); //guardar modelos

            var cars = _context.Cars.Include(c => c.PurchaseItems)
                .ThenInclude(pi => pi.Purchase)
                    .Where(c => carModels.Contains(c.Model.Name))
                        .Select(c => new
                        {
                            //coger atributos de la bbdd creada para comprobar las condiciones
                            c.Id,
                            c.Model,
                            c.QuantityForPurchasing,
                            c.PurchasingPrice,
                            NumberOfPurchasedItems=c.PurchaseItems.Sum(pi=>pi.Quantity)
                        }).ToList();

            Purchase purchase = new Purchase((AppForSEII2526.API.Models.PaymentMethodTypes)purchaseForCreate.PaymentMethod,purchaseForCreate.PurchaseDate,
                                            user,new List<PurchaseItem>());

            foreach (var item in purchaseForCreate.PurchaseItems)
            {
                var car = cars.FirstOrDefault(c => c.Model.Name == item.Model);

                if (car == null || (car.NumberOfPurchasedItems >= car.QuantityForPurchasing))
                {
                    ModelState.AddModelError("PurchaseItems", $"Error! Car Model '{item.Model}' is not available for being purchased from the database");
                }
                else
                {
                    int stock=car.QuantityForPurchasing-car.NumberOfPurchasedItems;
                    if(item.Quantity > stock)
                    {
                        ModelState.AddModelError("PurchaseItems", $"Error! Not enough stock for Car Model '{item.Model}'. Available: {stock}, Requested: {item.Quantity}");
                    }
                    else
                    {
                        purchase.PurchaseItems.Add(new PurchaseItem(car.Id, purchase.Id, item.Quantity));
                        item.PurchasingPrice = car.PurchasingPrice;
                    }
                   
                }
            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            decimal totalCost = purchaseForCreate.PurchaseItems.Sum(
                pi => pi.PurchasingPrice * pi.Quantity
            );

            purchase.PurchasingPrice = totalCost;
            _context.Add(purchase);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Purchase", $"Error! There was an error while saving your purchase, please, try again later");
                return Conflict("Error" + ex.Message);
            }

            var purchaseDetail = new PurchaseDetailDTO(purchase.Id, purchase.PurchasingDate, purchase.ApplicationUser.Name, purchase.ApplicationUser.Surname, purchase.ApplicationUser.Address,purchase.ApplicationUser.UserName,
                                 purchase.PaymentMethod,purchaseForCreate.PurchaseItems);

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchaseDetail);
        }

    }
}
