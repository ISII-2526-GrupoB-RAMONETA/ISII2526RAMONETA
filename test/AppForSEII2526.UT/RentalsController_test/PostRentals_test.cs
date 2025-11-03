using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.RentalDTO;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForMovies.UT.RentalsController_test
{
    public class PostRentals_test : AppForMovies4SqliteUT
    {
        public PostRentals_test()
        {

            var models = new List<Model>() {
                new Model("Model A"),
                new Model("Model B"),
                new Model("Model C"),
            };

            var cars = new List<Car>(){
                new Car("carclass","Red","desc","manufacturer",20000,5,2,150,4.5m,2.0m,"Gasoline","Basic",18,6),
            };

            ApplicationUser user = new ApplicationUser("1", "Pablo", "Ballesterou", "pab@uclm.es","calle","2");

            var rental = new Rental(user,DateTime.Now, PaymentMethodTypes.Efectivo, DateTime.Today.AddDays(2), DateTime.Today.AddDays(10), new List<RentalItem>());

            _context.ApplicationUsers.Add(user);
            _context.AddRange(models);
            _context.AddRange(cars);
            _context.Add(rental);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            var rentalNoITem = new RentalForCreateDTO("Pablo", "Ballesterou","calle" , PaymentMethodTypes.Efectivo, true,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                new List<RentalItemDTO>());

            var rentalItems = new List<RentalItemDTO>() { new RentalItemDTO(2, "modelo2", "Manuf" ,30,2,"couchir") };

            var rentalFromBeforeToday = new RentalForCreateDTO("Pablo", "Ballesterou", "calle", PaymentMethodTypes.Efectivo, true,
                DateTime.Today, DateTime.Today.AddDays(5), rentalItems);

            var rentalToBeforeFrom = new RentalForCreateDTO("Pablo", "Ballesterou", "calle", PaymentMethodTypes.Efectivo, true,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), rentalItems);

            var RentalApplicationUser = new RentalForCreateDTO("Pablo", "Motous", "calle", PaymentMethodTypes.Efectivo, true,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), rentalItems);

            var rentalMovieNotAvailable = new RentalForCreateDTO("Pablo", "Ballesterou", "calle", PaymentMethodTypes.Efectivo, true,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                new List<RentalItemDTO>() { new RentalItemDTO(1, "model1", "manufacturer3", 1,2,"coche") });


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { rentalNoITem, "Error! You must include at least one movie to be rented",  },
                new object[] { rentalFromBeforeToday, "Error! Your rental date must start later than today", },
                new object[] { rentalToBeforeFrom, "Error! Your rental must end later than it starts", },
                new object[] { RentalApplicationUser, "Error! UserName is not registered", },
                new object[] { rentalMovieNotAvailable, "Error! Movie titled 'The lord of the rings' is not available for being rented from", },
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreateRental_Error_test(RentalForCreateDTO rentalDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorExpected, errorActual);

        }

    }
}