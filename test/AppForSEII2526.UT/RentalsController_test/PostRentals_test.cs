using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CarsDTO;
using AppForSEII2526.API.DTOs.RentalDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class PostRentals_test : AppForSEII25264SqliteUT
    {
        public PostRentals_test()
        {
            var models = new List<Model>()
            {
                new Model("Sedan"),
                new Model("SUV"),
                new Model("Hatchback")
            };

            // Guardamos los modelos primero para que tengan Ids válidos
            _context.AddRange(models);
            _context.SaveChanges();

            var cars = new List<Car>()
            {
                new Car("Standard","Red", "Compact family sedan", "Toyota", 20000m, 100, 15, 40,2,6,"Gasoline","Oil change, tire rotation",16m,models[0]),
                new Car("Premium","Black", "Luxury SUV", "BMW", 60000m, 50, 13, 80,3,8,"Diesel","Brake inspection, fluid check",20,models[1]),
                new Car("Economy","Red", "Compact hatchback", "Ford", 15000m, 150, 10, 30,1,4,"Gasoline","Battery check, air filter replacement",15,models[2]),
                new Car("Electric","White", "Electric sedan", "Tesla", 80000m, 30, 12, 100,4,10,"Electric","Tire rotation, brake inspection",0,models[0]),
                new Car("Hybrid","Blue", "Hybrid SUV", "Honda", 35000m, 80, 15, 50,2,7,"Hybrid","Oil change, fluid check",10,models[1])
            };

            // Guardamos los coches para que tengan Ids válidos antes de referenciarlos en RentalItem
            _context.AddRange(cars);
            _context.SaveChanges();

            ApplicationUser user = new ApplicationUser("1", "Pablo", "Ballestero", "pablo.ballestero@uclm.es", "Paseo Cervantes,8", "633");
            _context.Add(user);
            _context.SaveChanges();

            var rental = new Rental(user, PaymentMethodTypes.Efectivo, DateTime.Now, true,
                DateTime.Now.AddDays(2), DateTime.Now.AddDays(5), new List<RentalItem>());

            _context.Add(rental);
            _context.SaveChanges();

            var testquantity = 13; //igual a QuantityofRenting del segundo coche

            // Ahora que los coches existen y tienen Ids, creamos el RentalItem apuntando a un Car real
            var rentalItem = new RentalItem(cars[1], rental, testquantity);
            rental.RentalItems.Add(rentalItem);

            _context.Add(rentalItem);
            _context.SaveChanges();
        }



        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            var rentalNoITem = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true, DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now.AddDays(5), new List<RentalItemDTO>());

            var rentalItems = new List<RentalItemDTO>() { new RentalItemDTO(1, "Sedan", "Toyota", 40, 2) };

            var rentalFromBeforeToday = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now, DateTime.Now.AddDays(5), rentalItems);

            var rentalToBeforeFrom = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now.AddDays(5), DateTime.Now.AddDays(2), rentalItems);

            var RentalApplicationUser = new RentalForCreateDTO("TengenToppaGurrenLagann", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now.AddDays(4), rentalItems);

            var rentalCarDontExist = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now.AddDays(5),
                new List<RentalItemDTO>() { new RentalItemDTO(12, "SUV", "BMW", 80, 1) });

            var rentalCarNoStock = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now.AddDays(5),
                new List<RentalItemDTO>() { new RentalItemDTO(2, "SUV", "BMW", 80, 1) });

            var rentalCarNotEnoughStock = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, DateTime.Now.AddDays(2), DateTime.Now.AddDays(5),
                new List<RentalItemDTO>() { new RentalItemDTO(1, "Sedan", "Toyota", 80, 100) });


            var allTests = new List<object[]>
            {             //input for createpurchase - Error expected
                new object[] { rentalNoITem, "Error! You must include at least one car to be rented",  },
                new object[] { rentalFromBeforeToday, "Error! Your rental date must start later than today", },
                new object[] { rentalToBeforeFrom, "Error! Your rental must end later than it starts", },
                new object[] { RentalApplicationUser, "Error! UserName is not registered", },
                new object[] { rentalCarDontExist, "Error! Car Model 'SUV' is not available for being rented from the database", },
                new object[] { rentalCarNoStock, "Error! Car Model 'SUV' has no available units for the selected dates", },
                new object[] { rentalCarNotEnoughStock, "Error! Not enough stock for Car Model 'Sedan'. Available: 15, Requested: 100", },
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

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateRental_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            DateTime to = DateTime.Now.AddDays(6);
            DateTime from = DateTime.Now.AddDays(3);

            var rentalDTO = new RentalForCreateDTO("pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8",
                PaymentMethodTypes.Efectivo, true,
                DateTime.Now, from, to, new List<RentalItemDTO>()
                { new RentalItemDTO(1, "Sedan", "Toyota", 40, 1) });

            var expectedrentalDetailDTO = new RentalDetailDTO(2, DateTime.Now,
                "pablo.ballestero@uclm.es", "Pablo", "Ballestero", "Paseo Cervantes,8", true, PaymentMethodTypes.Efectivo,
                from, to, new List<RentalItemDTO>()
                { new RentalItemDTO(1, "Sedan", "Toyota", 40, 1) });

            // Act
            var result = await controller.CreateRental(rentalDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<RentalDetailDTO>(createdResult.Value);

            Assert.Equal(expectedrentalDetailDTO, actualRentalDetailDTO);

        }

    }
}