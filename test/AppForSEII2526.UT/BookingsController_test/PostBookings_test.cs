using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.MaintenancesDTO;
using AppForSEII2526.API.Controllers;

namespace AppForSEII2526.UT.BookingsController_test
{
    public class PostBookings_test : AppForSEII25264SqliteUT
    {
        private const string _userName = "pablo.ramon@uclm.es";
        private const string _customerName = "Pablo";
        private const string _customerSurname = "Ramón Palarea";
        private const string _address = "Val general";
        private const string _phoneNumber = "622";

        private const string _maintenance1Name = "Air filter cleaning";
        private const string _maintenance1Type = "Preventive";
        private const string _maintenance2Name = "Repair ventilation motor";
        private const string _maintenance2Type = "Corrective";

        public PostBookings_test()
        {
            var types = new List<MaintenanceType>()
            {
                new MaintenanceType (_maintenance1Type),
                new MaintenanceType (_maintenance2Type)
            };

            var maintenances = new List<Maintenance>()
            {
                new Maintenance(_maintenance1Name,3,80m,types[0]),
                new Maintenance(_maintenance2Name,5,300m,types[1])
            };

            ApplicationUser user = new ApplicationUser("1", _customerName, _customerSurname, _userName, _address, _phoneNumber);

            var booking = new Booking(DateTime.Now, PaymentMethodTypes.Efectivo, user, new List<BookingItem>());
            booking.BookingItems.Add(new BookingItem(booking, "Air filter cleaned, dust and debris removed", maintenances[0]));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(types);
            _context.AddRange(maintenances);
            _context.Add(booking);
            _context.SaveChanges();


        }

        public static IEnumerable<object[]> TestCasesFor_CreateBooking()
        {
            //Caso 1 -> Error porque no se incluye mantenimiento
            var bookingNoItem = new BookingForCreateDTO(_userName, _customerName, _customerSurname, _address, PaymentMethodTypes.Efectivo, _phoneNumber, new List<BookingItemDTO>());

            var bookingItems = new List<BookingItemDTO>()
            {
                new BookingItemDTO(2,_maintenance2Name,5,300m,"Ventilation system repaired, main fan replaced",_maintenance2Type)

            };

            // Caso 2 -> Error porque el usuario no está registrado
            var BookingApplicationUser = new BookingForCreateDTO("usuario.ejemplo@uclm.es", _customerName, _customerSurname, _address, PaymentMethodTypes.Efectivo, _phoneNumber, bookingItems);

            // Caso 3 -> Error porque el mantenimiento no existe
            var bookingMaintenanceNotAvaible = new BookingForCreateDTO(_userName, _customerName, _customerSurname, _address,
                PaymentMethodTypes.Efectivo, _phoneNumber, new List<BookingItemDTO>() {new BookingItemDTO(0, "Mantenimiento nulo",0,0,"0", "") });

            // Se devuelven los 3 casos con sus mensajes esperados
            var allTests = new List<object[]>
            {
                new object []{ bookingNoItem, "Error! You must include at least one maintenance to be booked", },
                new object []{ BookingApplicationUser, "Error! UserName is not registered", },
                new object []{ bookingMaintenanceNotAvaible, "Error! Maintenance named 'Mantenimiento nulo' is not available for being booked", },


            };

            return allTests;


        }

        [Theory] // Indica que este método se ejecutará varias veces (uno por cada caso)
        [Trait("LevelTesting", "Unit Testing")] // Indica que es un test unitario
        [Trait("Database", "WithoutFixture")] // Usa una BD en memoria
        [MemberData(nameof(TestCasesFor_CreateBooking))]
        public async Task CreateBooking_Error_test(BookingForCreateDTO bookingDTO, string errorExpected)
        {
            //Arrange
            var mock = new Mock<ILogger<BookingsController>>();
            ILogger<BookingsController> logger = mock.Object;

            // Se instancia el controlador
            var controller = new BookingsController(_context, logger);

            //Act
            // Se llama al método
            var result = await controller.CreateBooking(bookingDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);


        }

        [Fact] 
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateBooking_Success_test()
        {
            //Arrange
            var mock = new Mock<ILogger<BookingsController>>();
            ILogger<BookingsController> logger = mock.Object;

            var controller = new BookingsController(_context, logger);

            // DTO enviado al API para crear un booking correctamente
            var bookingDTO = new BookingForCreateDTO(_userName, _customerName, _customerSurname, _address, PaymentMethodTypes.Efectivo, _phoneNumber,
                new List<BookingItemDTO>() { new BookingItemDTO(2, _maintenance1Name, 3, 80m, "Air filter cleaned, dust and debris removed", _maintenance1Type) });

            // DTO esperado como resultado del método al crear la reserva
            var expectedBookingDetailDTO = new BookingDetailDTO(2, DateTime.Now, _userName, _customerName, _customerSurname, _address, PaymentMethodTypes.Efectivo,
                _phoneNumber,
                new List<BookingItemDTO>() { new BookingItemDTO(2, _maintenance1Name, 3, 80m, "Air filter cleaned, dust and debris removed", _maintenance1Type) });


            //Act
            var result = await controller.CreateBooking(bookingDTO); // Se llama a la acción del controller

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualBookingDetailDTO = Assert.IsType<BookingDetailDTO>(createdResult.Value);

            Assert.Equal(expectedBookingDetailDTO, actualBookingDetailDTO);




        }







    }
}
