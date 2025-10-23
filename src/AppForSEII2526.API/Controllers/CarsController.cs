using AppForSEII2526.API.DTOs.CarsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //used to enable your controller to acces the database
        private readonly ApplicationDbContext _context;
        //sued to log any information when your system is running
        private readonly ILogger<CarsController> _logger;

        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("[action]")]
        //[ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]

        //public async Task<ActionResult> GetCarsForRenting()
        //{
        //    IList<Car> cars = await _context.Cars
        //        .ToListAsync();
        //    return Ok(cars);
        //}

        //public async Task<ActionResult> ComputeDivision(decimal op1, decimal op2)
        //{
        //    if (op2 == 0)
        //    {
        //        _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
        //        return BadRequest("op2 must be different from 0");
        //    }
        //    decimal result = decimal.Round(op1 / op2, 2);
        //    return Ok(result);
        //}

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCoches_conDTOs()
        {
            var coches = await _context.Cars
                .Select(c => new CarForRentalDTO(
                    c.Id,
                    c.Color,
                    c.Fueltype,
                    c.Manufacturer,
                    c.RentingPrice,
                    c.Model.Name
                ))
                .ToListAsync();
            return Ok(coches);

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCoches_FILTRO_PRECIO_DTO(decimal? precio, string? modelo)
        {
            var cars = await _context.Cars.Include(c => c.Model).Where(c => ((c.RentingPrice<precio) || (precio == null))
                        && ((c.Model.Name.Equals(modelo)) || (modelo == null))).Select(c => new CarForRentalDTO(c.Id, c.Color, c.Fueltype, c.Manufacturer, c.RentingPrice, c.Model.Name)).ToListAsync();
            return Ok(cars);

        }

    }

    }
