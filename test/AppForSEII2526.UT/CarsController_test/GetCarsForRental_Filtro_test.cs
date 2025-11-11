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

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForRental_Filtro_test : AppForSEII25264SqliteUT
    {
        public GetCarsForRental_Filtro_test()
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
                new Car("Standard","Red", "Compact family sedan", "Toyota", 20000m, 100, 5, 40,2,6,"Gasoline","Oil change, tire rotation",16m,models[0]),
                new Car("Premium","Black", "Luxury SUV", "BMW", 60000m, 50, 3, 80,3,8,"Diesel","Brake inspection, fluid check",20,models[1]),
                new Car("Economy","Red", "Compact hatchback", "Ford", 15000m, 150, 10, 30,1,4,"Gasoline","Battery check, air filter replacement",15,models[2]),
                new Car("Electric","White", "Electric sedan", "Tesla", 80000m, 30, 2, 100,4,10,"Electric","Tire rotation, brake inspection",0,models[0]),
                new Car("Hybrid","Blue", "Hybrid SUV", "Honda", 35000m, 80, 5, 50,2,7,"Hybrid","Oil change, fluid check",10,models[1])
            };

            // Guardamos los coches para que tengan Ids válidos antes de referenciarlos en RentalItem
            _context.AddRange(cars);
            _context.SaveChanges();

            ApplicationUser user = new ApplicationUser("1", "Pablo", "Ballestero", "pablo.ballestero@uclm.es", "Paseo Cervantes,8", "633");
            _context.Add(user);
            _context.SaveChanges();

            var rental = new Rental(user, DateTime.Now, PaymentMethodTypes.Efectivo,
                DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), new List<RentalItem>());

            _context.Add(rental);
            _context.SaveChanges();

            // Ahora que los coches existen y tienen Ids, creamos el RentalItem apuntando a un Car real
            var rentalItem = new RentalItem(cars[0].Id, rental, 2, "Need a reliable car for family trip");
            rental.RentalItems.Add(rentalItem);

            _context.Add(rentalItem);
            _context.SaveChanges();
        }



        public static IEnumerable<object[]> TestCasesFor_GetCoches_FILTRO_PRECIO_OK()
        {
            var CarsForRentalDTO = new List<CarForRentalDTO>()
            {
                new CarForRentalDTO(1,"Red","Gasoline", "Toyota", 40m,"Sedan"),
                new CarForRentalDTO(2,"Black","Diesel", "BMW", 80m,"SUV"),
                new CarForRentalDTO(3,"Red","Gasoline", "Ford", 30m,"Hatchback"),
                new CarForRentalDTO(4,"White","Electric", "Tesla", 100m,"Sedan"),
                new CarForRentalDTO(5,"Blue","Hybrid", "Honda", 50m,"SUV")
            };

            var CarForRentalDTOsTC1 = new List<CarForRentalDTO>() // espera todos los coches
            {
               CarsForRentalDTO[0], CarsForRentalDTO[1], CarsForRentalDTO[2], CarsForRentalDTO[3], CarsForRentalDTO[4]
            };

            var CarForRentalDTOsTC2 = new List<CarForRentalDTO>() // precio < 45 => Toyota(40) y Ford(30)
            {
               CarsForRentalDTO[0], CarsForRentalDTO[2]
            };

            var CarForRentalDTOsTC3 = new List<CarForRentalDTO>() // solo SUV => BMW y Honda
            {
               CarsForRentalDTO[1], CarsForRentalDTO[4]
            };

            var CarForRentalDTOsTC4 = new List<CarForRentalDTO> { // precio < 60 y modelo Sedan => solo Toyota (40)
                CarsForRentalDTO[0]
            };

            var allTests = new List<Object[]>
            {
                new object[] { null, null, CarForRentalDTOsTC1 },         // sin filtro => todos
                new object[] { 45m, null, CarForRentalDTOsTC2 },          // precio < 45
                new object[] { null, "SUV", CarForRentalDTOsTC3 },        // modelo = "SUV"
                new object[] { 60m, "Sedan", CarForRentalDTOsTC4 }        // precio < 60 y modelo = "Sedan"
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCoches_FILTRO_PRECIO_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCoches_FILTRO_PRECIO_OK_test(decimal? filterPrice, string? filterModel, IList<CarForRentalDTO> expectedCars)
        {
            // Arrange
            var controller = new CarsController(_context, null);

            // Act (usamos los parámetros del MemberData)
            var result = await controller.GetCarsForRental_Filtro(filterPrice, filterModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rentalDTOsActual = Assert.IsType<List<CarForRentalDTO>>(okResult.Value);
            Assert.Equal(expectedCars, rentalDTOsActual);
        }
    }
}