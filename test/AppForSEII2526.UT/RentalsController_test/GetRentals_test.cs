using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.CarsDTO;
using AppForSEII2526.API.DTOs.RentalDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppForSEII2526.UT.RentalsController_test
{
    public class GetRentals_test : AppForSEII25264SqliteUT
    {
        public GetRentals_test()
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

            var testquantity = 2;

            // Ahora que los coches existen y tienen Ids, creamos el RentalItem apuntando a un Car real
            var rentalItem = new RentalItem(cars[0], rental, testquantity);
            rental.RentalItems.Add(rentalItem);

            _context.Add(rentalItem);
            _context.SaveChanges();
        }



        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetRental_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;

            var controller = new RentalsController(_context, logger);

            // Act
            var result = await controller.GetRental(0);

            //Assert
            //we check that the response type is OK and obtain the list of movies
            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetRental_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<RentalsController>>();
            ILogger<RentalsController> logger = mock.Object;
            var controller = new RentalsController(_context, logger);

            var testdatefrom = DateTime.Now.AddDays(2); //en rentalDetailDTO
            var testdateto = DateTime.Now.AddDays(5);


            var expectedRental = new RentalDetailDTO(1, DateTime.Now, "pablo.ballestero@uclm.es", "Pablo", "Ballestero",
                        "Paseo Cervantes,8", true, PaymentMethodTypes.Efectivo,
                        testdatefrom, testdateto,
                        new List<RentalItemDTO>());

            var expectedPrice = (decimal)(testdateto - testdatefrom).TotalDays * 2 * 40; //2=testquantity

            expectedRental.RentalItems.Add(new RentalItemDTO(1, "Sedan", "Toyota", 40, 2));

            // Act 
            var result = await controller.GetRental(1);

            //Assert
            //we check that the response type is OK and obtain the rental
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rentalDTOActual = Assert.IsType<RentalDetailDTO>(okResult.Value);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            var expectedJson = JsonSerializer.Serialize(expectedRental, options);
            var actualJson = JsonSerializer.Serialize(rentalDTOActual, options);

            // Escribimos la representación completa en la salida del test
            Console.WriteLine("Expected RentalDetailDTO:\n" + expectedJson);
            Console.WriteLine("Actual   RentalDetailDTO:\n" + actualJson);

            // ahora el assert original
            Assert.Equal(expectedRental, rentalDTOActual);

        }
    }
}