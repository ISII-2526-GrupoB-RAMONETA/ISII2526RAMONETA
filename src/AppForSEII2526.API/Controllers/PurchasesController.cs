using AppForSEII2526.API.DTOs.PurchasesDTO;
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
                .Select(p => new PurchaseDetailDTO(p.Id, p.PurchasingDate,p.ApplicationUser.Name, p.ApplicationUser.Surname, p.ApplicationUser.Address,
                    p.PurchaseItems.Select(pi => new PurchaseItemDTO(pi.Car.Id,pi.Car.Model.Name,pi.Car.PurchasingPrice,pi.Car.Color,pi.Car.QuantityForPurchasing)).ToList<PurchaseItemDTO>())).FirstOrDefaultAsync();

            if (purchase == null)
                {
                _logger.LogError($"Error: Purchase with id {id} does not exist");
                return NotFound();
            }

            return Ok(purchase);

        }
    }
}
