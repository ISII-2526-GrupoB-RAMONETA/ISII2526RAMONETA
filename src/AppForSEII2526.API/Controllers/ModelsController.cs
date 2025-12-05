using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // <-- Agrega este using

namespace AppForSEII2526.API.Controllers
{
    public class ModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public ModelsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Cars/GetCarsForRental
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetModels(string? modelName)
        {

            IList<string> models = await _context.Models
                .Where(model => (modelName == null || model.Name.Contains(modelName))) // where clause             
                .OrderBy(model => model.Name)
                .Select(model => model.Name)
                .ToListAsync();

            return Ok(models);
        }
    }
}
