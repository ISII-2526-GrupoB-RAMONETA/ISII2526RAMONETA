using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.MaintenancesDTO;

namespace AppForSEII2526.UT.BookingsController_test
{
    public class GetBookings_test : AppForSEII25264SqliteUT
    {
        public GetBookings_test()
        {
            var types = new List<MaintenanceType>()
            {
                new MaintenanceType ("Preventive"),
                new MaintenanceType ("Corrective")
            };

            var maintenances = new List<Maintenance>()
            {
                new Maintenance("Air filter cleaning",3,80m,types[0]),
                new Maintenance("Repair ventilation motor",5,300m,types[1])
            };

            ApplicationUser user = new ApplicationUser("1", "Pablo", "Ramón Palarea", "pablo.ramon@uclm.es", "Val general", "622");

            var booking = new Booking(DateTime.Now, PaymentMethodTypes.Efectivo, user, new List<BookingItem>());

            booking.BookingItems.Add(new BookingItem(booking, "Air filter cleaned, dust and debris removed", maintenances[0]));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(types);
            _context.AddRange(maintenances);
            _context.AddRange(booking);
            _context.SaveChanges();


        }


        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBooking_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingsController>>();
            ILogger<BookingsController> logger = mock.Object;

            var controller = new BookingsController(_context, logger);

            //Act
            var result = await controller.GetBooking(0);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetBooking_Found_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingsController>>();
            ILogger<BookingsController> logger = mock.Object;

            var controller = new BookingsController(_context, logger);

            var expectedBooking = new BookingDetailDTO(1, DateTime.Now, "pablo.ramon@uclm.es", "Pablo", "Ramón Palarea", "Val general",
                PaymentMethodTypes.Efectivo, "622", new List<BookingItemDTO>());
            expectedBooking.BookingItems.Add(new BookingItemDTO(1, "Air filter cleaning", 3, 80m, "Air filter cleaned, dust and debris removed", "Preventive"));

            //Act
            var result = await controller.GetBooking(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bookingDTOActual = Assert.IsType<BookingDetailDTO>(okResult.Value);
            var eq = expectedBooking.Equals(bookingDTOActual);
            //we check that the expected and actual are the same
            Assert.Equal(expectedBooking, bookingDTOActual);




        }



    }
}
