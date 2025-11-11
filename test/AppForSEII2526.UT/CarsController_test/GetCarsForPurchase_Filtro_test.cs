using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.CarsDTO;
using AppForSEII2526.API.DTOs.MaintenancesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCarsForPurchase_Filtro_test : AppForSEII25264SqliteUT
    {
        public GetCarsForPurchase_Filtro_test()
        {
            var models = new List<Model>()
            {
                new Model("Sedan"),
                new Model("SUV"),
                new Model("Hatchback"),
                new Model("Coupe"),
                new Model("Pickup Truck")
            };

            var cars = new List<Car>()
            {
                new Car("Standard", "Red", "Compact family sedan", "Toyota", 2500000.00m, 0, 2, 12000.00m, 4.50m, 1.80m, "Gasoline", "Oil change, tire rotation", 16.00m, models[0]),
                new Car("Premium", "Black", "Luxury business sedan", "Mercedes-Benz", 4800000.00m, 4, 2, 20000.00m, 4.80m, 2.00m, "Hybrid", "Oil change, battery check", 17.00m, models[0]),
                new Car("Standard", "Blue", "Family SUV with ample space", "Toyota", 3600000.00m, 6, 3, 15000.00m, 4.60m, 2.40m, "Diesel", "Tire rotation, oil change", 18.00m, models[1]),
                new Car("Compact", "White", "Fuel-efficient city hatchback", "Volkswagen", 1900000.00m, 7, 5, 8000.00m, 4.30m, 1.60m, "Gasoline", "Oil change, tire replacement", 15.00m, models[2]),
                new Car("Sport", "Red", "High-performance two-door coupe", "Audi", 6200000.00m, 2, 1, 28000.00m, 4.90m, 3.20m, "Gasoline", "Oil change, brake inspection", 19.00m, models[3])
            };
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser("1", "Elena", "Pretel", "elena.pretel", "Avenida España, 2", null),
                new ApplicationUser("2", "Pablo", "Ramón", "pablo.ramon", "Val General, 12", null),
                new ApplicationUser("3", "Pablo", "Ballestero", "pablo.ballestero", "Paseo Cervantes,8", null),
                new ApplicationUser("4", "Tomás", "González", "tomas.gonzalez", "Blasco Ibáñez,4", null)
            };
            ApplicationUser user = new ApplicationUser("1", "Elena", "Pretel", "elena.pretel", "Avenida España, 2", null);


            var purchases = new List<Purchase>
            {
                new Purchase(1, PaymentMethodTypes.Efectivo, new DateTime(2025, 1, 15), 2500000.00m, true, new List<PurchaseItem>(), users[0]),
                new Purchase(2, PaymentMethodTypes.Efectivo, new DateTime(2025, 2, 12), 4800000.00m, true, new List<PurchaseItem>(), users[1]),
                new Purchase(3, PaymentMethodTypes.TarjetaCredito, new DateTime(2025, 3, 10), 3600000.00m, false, new List<PurchaseItem>(), users[2]),
                new Purchase(4, PaymentMethodTypes.TransferenciaBancaria, new DateTime(2025, 4, 5), 1900000.00m, false, new List<PurchaseItem>(), users[3]),
                new Purchase(5, PaymentMethodTypes.Efectivo, new DateTime(2025, 5, 15), 6200000.00m, true, new List<PurchaseItem>(), users[0])
            };

            // Asignar usuarios a compras
            purchases[0].ApplicationUser = users[0];
            purchases[1].ApplicationUser = users[1];
            purchases[2].ApplicationUser = users[2];
            purchases[3].ApplicationUser = users[3];
            purchases[4].ApplicationUser = users[0]; // Elena repite

            // 4. PurchaseItems (usando el constructor completo)
            var purchaseItems = new List<PurchaseItem>
            {
            new PurchaseItem(cars[0], 1, purchases[0], 1, 1),
            new PurchaseItem(cars[1], 2, purchases[1], 2, 1),
            new PurchaseItem(cars[2], 3, purchases[2], 3, 1),
            new PurchaseItem(cars[3], 4, purchases[3], 4, 1)
            // La compra 5 no tiene item (o puedes añadir uno después)
            };

            // 5. Asignar items a las compras
            purchases[0].PurchaseItems = new List<PurchaseItem> { purchaseItems[0] };
            purchases[1].PurchaseItems = new List<PurchaseItem> { purchaseItems[1] };
            purchases[2].PurchaseItems = new List<PurchaseItem> { purchaseItems[2] };
            purchases[3].PurchaseItems = new List<PurchaseItem> { purchaseItems[3] };
            purchases[4].PurchaseItems = new List<PurchaseItem>(); // vacía por ahora

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.AddRange(users);
            _context.AddRange(purchases);
            _context.AddRange(purchaseItems);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetCarsForPurchase_Filtro_OK()
        {
            // 1. Definimos el "pool" completo de DTOs que esperamos
            // (La base de datos tiene 5 coches, así que definimos los 5)
            var carDTOs = new List<CarForPurchaseDTO>
            {
                new CarForPurchaseDTO(1, "Sedan", "Red", "Gasoline", "Toyota", 2500000.00m),
                new CarForPurchaseDTO(2, "Sedan", "Black", "Hybrid", "Mercedes-Benz", 4800000.00m),
                new CarForPurchaseDTO(3, "SUV", "Blue", "Diesel", "Toyota", 3600000.00m),
                new CarForPurchaseDTO(4, "Hatchback", "White", "Gasoline", "Volkswagen", 1900000.00m),
                new CarForPurchaseDTO(5, "Coupe", "Red", "Gasoline", "Audi", 6200000.00m)
            };

            // 2. Definimos los resultados esperados para CADA test

            // Test 1: (color: null, modelo: null) -> Espera TODOS los coches
            var carPurchasesDTOsTC1 = new List<CarForPurchaseDTO>()
            {
                carDTOs[0], carDTOs[1], carDTOs[2], carDTOs[3], carDTOs[4]
            };

            // Test 2: (color: "Red", modelo: null) -> Espera el Toyota Sedan y el Audi Coupe
            var carPurchasesDTOsTC2 = new List<CarForPurchaseDTO>
            {
                carDTOs[0], carDTOs[4]
            };

            // Test 3: (color: null, modelo: "Sedan") -> Espera el Toyota Sedan y el Mercedes Sedan
            var carPurchasesDTOsTC3 = new List<CarForPurchaseDTO>
            {
                carDTOs[0], carDTOs[1]
            };

            // Test 4: (color: "Blue", modelo: "SUV") -> Espera SOLO el Toyota SUV
            var carPurchasesDTOsTC4 = new List<CarForPurchaseDTO>
            {
                carDTOs[2]
            };

            // 3. Devolvemos los casos de prueba con los datos esperados CORRECTOS
            var allTests = new List<Object[]>
            {
                 // Filtro (null, null) -> Espera TC1 (5 coches)
                new object[] {null, null, carPurchasesDTOsTC1 },
                 
                 // Filtro ("Red", null) -> Espera TC2 (2 coches)
                new object[] {"Red", null, carPurchasesDTOsTC2 },
                 
                 // Filtro (null, "Sedan") -> Espera TC3 (2 coches)
                new object[] {null, "Sedan", carPurchasesDTOsTC3 },
                 
                 // Filtro ("Blue", "SUV") -> Espera TC4 (1 coche)
                new object[] {"Blue", "SUV", carPurchasesDTOsTC4 }
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesFor_GetCarsForPurchase_Filtro_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetCarsForPurchase_Filtro_OK_test(string? color, string? modelo, IList<CarForPurchaseDTO> expectedCars)
        {
            //Arrange
            var controller = new CarsController(_context, null);

            //Act
            var result = await controller.GetCarsForPurchase_Filtro(color, modelo);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);

            //and obtain the list of maintenances
            var carForPurchaseDTOsActual = Assert.IsType<List<CarForPurchaseDTO>>(okResult.Value);
            Assert.Equal(expectedCars, carForPurchaseDTOsActual);



        }


    }
}
