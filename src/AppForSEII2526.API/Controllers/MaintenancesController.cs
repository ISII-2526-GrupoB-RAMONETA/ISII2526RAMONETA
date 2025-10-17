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
                .Select(m => new MaintenanceDTO(m.Id, m.Name, m.MaintenanceType.Type)).ToListAsync();
            return Ok(maintenances);

        }







    }




}
