using AppForSEII2526.API.DTOs.MaintenancesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenancesController : ControllerBase
    {
        //used to enable your controller to acces to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<MaintenancesController> _logger;


        public MaintenancesController(ApplicationDbContext context, ILogger<MaintenancesController> logger)
        {
            _context = context;
            _logger = logger;
        }



        //Método GET que causa error en el Swagger 

        //[HttpGet]
        //[Route("[action]")]
        //[ProducesResponseType(typeof(IList<Maintenance>), (int)HttpStatusCode.OK)]

        //public async Task<ActionResult> GetMaintenances()
        //{
        //   IList<Maintenance> maintenances = await _context.Maintenances
        //        .ToListAsync();
        //    return Ok(maintenances);

        //}




        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<MaintenanceDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetMaintenances()
        {
            var maintenances = await _context.Maintenances
                .Select(m => new MaintenanceDTO(m.Id, m.Name, m.MaintenanceType.Type, m.Price, m.NumberOfDays)).ToListAsync();
            return Ok(maintenances);

        }





        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<MaintenanceDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetMaintenances_filtro(string? maintenanceName, string? maintenanceType)
        {
            var maintenances = await _context.Maintenances
                .Include(m=>m.MaintenanceType)
                .Where(m =>
                (maintenanceName == null || m.Name.Contains(maintenanceName))
                && (maintenanceType == null || m.MaintenanceType.Type.Equals(maintenanceType))
                )
                .OrderBy(m => m.Name)
                .Select(m => new MaintenanceDTO(m.Id, m.Name, m.MaintenanceType.Type, m.Price, m.NumberOfDays)).ToListAsync();
            return Ok(maintenances);

        }







    }




}
