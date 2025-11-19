using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.MaintenancesDTO;

namespace AppForSEII2526.UT.MaintenancesController_test
{
    public class GetMaintenancesFiltro_test : AppForSEII25264SqliteUT
    {
        public GetMaintenancesFiltro_test() // Carga datos iniciales en la BD de prueba
        {
            // Tipos de mantenimiento disponibles
            var types = new List<MaintenanceType>()
            {
                new MaintenanceType ("Preventive"),
                new MaintenanceType ("Corrective")
            };

            // Mantenimientos de ejemplo
            var maintenances = new List<Maintenance>()
            {
                new Maintenance("Air filter cleaning",3,80m,types[0]),
                new Maintenance("Oil level check",1,50m,types[0]),
                new Maintenance("Repair ventilation motor",5,300m,types[1])

            };
            // Usuario de prueba
            ApplicationUser user = new ApplicationUser("1","Pablo", "Ramón Palarea", "pablo.ramon@uclm.es","Val general","622");

            // Reserva para relacionar datos (no afecta al test pero mantiene consistencia)
            var booking = new Booking(DateTime.Today,PaymentMethodTypes.Efectivo,user,new List<BookingItem>());
            // BookingItem de prueba: usa el último mantenimiento de la lista
            var bookingItem = new BookingItem(booking, "Air filter cleaned, dust and debris removed", maintenances[maintenances.Count - 1]);
            booking.BookingItems.Add(bookingItem);

            // Se guardan los datos en la BD de prueba
            _context.Add(user);
            _context.AddRange(types);
            _context.AddRange(maintenances);
            _context.AddRange(booking);
            _context.SaveChanges();
        }

        // Generación de casos de prueba para GetMaintenancesFiltro()
        // Se devuelven combinaciones de filtros y las listas esperadas
        public static IEnumerable<object[]> TestCasesFor_GetMaintenancesFiltro_OK()
        {
            // Lista completa de mantenimientos en formato DTO
            var maintenancesDTOs = new List<MaintenanceDTO>()
            {
                new MaintenanceDTO(1,"Air filter cleaning","Preventive",80m,3),
                new MaintenanceDTO(2,"Oil level check","Preventive",50m,1),
                new MaintenanceDTO(3,"Repair ventilation motor","Corrective",300m,5)
            };

            //Caso 1 -> sin filtros -> devuelve todo ordenado por nombre
            var maintenanceDTOsTC1 = new List<MaintenanceDTO>()
            {
               maintenancesDTOs[0], maintenancesDTOs[1],maintenancesDTOs[2]
            }.OrderBy(m => m.Name).ToList();


            // Caso 2 -> filtrar por nombre: "Oil"
            var maintenancesDTOsTC2 = new List<MaintenanceDTO> { maintenancesDTOs[1]  };
            // Caso 3 -> filtrar por tipo: "Corrective"
            var maintenancesDTOsTC3 = new List<MaintenanceDTO> { maintenancesDTOs[2]  };
            // Caso 4 -> filtrar por tipo: "Preventive"
            var maintenancesDTOsTC4 = new List<MaintenanceDTO> { maintenancesDTOs[0], maintenancesDTOs[1]  };

            // Cada objeto del enumerable representa un test:
            // { filtroNombre, filtroTipo, resultadoEsperado }
            var allTests = new List<Object[]>
            {
                new object[] {null,null,maintenanceDTOsTC1, },
                new object[] {"Oil",null,maintenancesDTOsTC2, },
                new object[] {null,"Corrective",maintenancesDTOsTC3, },
                new object[] {null,"Preventive",maintenancesDTOsTC4,}
            };

            return allTests;




        }

        [Theory]// Indica que este método se ejecutará varias veces (uno por cada caso)
        [MemberData(nameof(TestCasesFor_GetMaintenancesFiltro_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMaintenancesFiltro_OK_test(string? filterName, string? filterType, IList<MaintenanceDTO> expectedMaintenances)
        {
            //Arrange -> se crea el controlador con el contexto de prueba
            var controller = new MaintenancesController(_context, null);

            //Act -> se ejecuta el método del controlador
            var result = await controller.GetMaintenancesFiltro(filterName, filterType);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);

            //and obtain the list of maintenances
            var maintenanceDTOsActual = Assert.IsType<List<MaintenanceDTO>>(okResult.Value);
            //Se compara la lista real con la esperada
            Assert.Equal(expectedMaintenances, maintenanceDTOsActual);



        }


        //[Fact]
        //[Trait("LevelTesting", "Unit Testing")]
        //[Trait("Database", "WithoutFixture")]
        //public 




    }
}
