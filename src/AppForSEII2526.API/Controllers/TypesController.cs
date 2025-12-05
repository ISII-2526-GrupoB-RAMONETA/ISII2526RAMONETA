using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    
    public class TypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public TypesController(ApplicationDbContext context, ILogger<MaintenancesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Maintenances/GetMaintenancesForBooking
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTypes(string? typeName)
        {

            IList<string> types = await _context.MaintenanceTypes
                .Where(type => (typeName == null || type.Type.Contains(typeName))) // where clause             
                .OrderBy(type => type.Type)
                .Select(type => type.Type)
                .ToListAsync();

            return Ok(types);
        }
    }
}
