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
        public GetMaintenancesFiltro_test()
        {
            var types = new List<MaintenanceType>()
            {
                new MaintenanceType ("Preventive"),
                new MaintenanceType ("Corrective")
            };

            var maintenances = new List<Maintenance>()
            {
                new Maintenance("Air filter cleaning",3,80m,types[0]),
                new Maintenance("Oil level check",1,50m,types[0]),
                new Maintenance("Repair ventilation motor",5,300m,types[1])

            };

            ApplicationUser user = new ApplicationUser("1","Pablo", "Ramón Palarea", "pablo.ramon@uclm.es","Val general","622");

            var booking = new Booking(DateTime.Today,PaymentMethodTypes.Efectivo,user,new List<BookingItem>());

            var bookingItem = new BookingItem(booking, "Air filter cleaned, dust and debris removed", maintenances[maintenances.Count - 1]);
            booking.BookingItems.Add(bookingItem);

            _context.Add(user);
            _context.AddRange(types);
            _context.AddRange(maintenances);
            _context.AddRange(booking);
            _context.SaveChanges();
        }



        public static IEnumerable<object[]> TestCasesFor_GetMaintenancesFiltro_OK()
        {
            var maintenancesDTOs = new List<MaintenanceDTO>()
            {
                new MaintenanceDTO(1,"Air filter cleaning","Preventive",80m,3),
                new MaintenanceDTO(2,"Oil level check","Preventive",50m,1),
                new MaintenanceDTO(3,"Repair ventilation motor","Corrective",300m,5)
            };

            var maintenanceDTOsTC1 = new List<MaintenanceDTO>()
            {
               maintenancesDTOs[0], maintenancesDTOs[1],maintenancesDTOs[2]
            }.OrderBy(m => m.Name).ToList();



            var maintenancesDTOsTC2 = new List<MaintenanceDTO> { maintenancesDTOs[1]  };
            var maintenancesDTOsTC3 = new List<MaintenanceDTO> { maintenancesDTOs[2]  };
            var maintenancesDTOsTC4 = new List<MaintenanceDTO> { maintenancesDTOs[0], maintenancesDTOs[1]  };



            var allTests = new List<Object[]>
            {
                new object[] {null,null,maintenanceDTOsTC1, },
                new object[] {"Oil",null,maintenancesDTOsTC2, },
                new object[] {null,"Corrective",maintenancesDTOsTC3, },
                new object[] {null,"Preventive",maintenancesDTOsTC4,}
            };

            return allTests;




        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetMaintenancesFiltro_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMaintenancesFiltro_OK_test(string? filterName, string? filterType, IList<MaintenanceDTO> expectedMaintenances)
        {
            //Arrange
            var controller = new MaintenancesController(_context, null);

            //Act
            var result = await controller.GetMaintenancesFiltro(filterName, filterType);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);

            //and obtain the list of maintenances
            var maintenanceDTOsActual = Assert.IsType<List<MaintenanceDTO>>(okResult.Value);
            Assert.Equal(expectedMaintenances, maintenanceDTOsActual);



        }


        //[Fact]
        //[Trait("LevelTesting", "Unit Testing")]
        //[Trait("Database", "WithoutFixture")]
        //public 




    }
}
