using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchasesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class PostPurchases_test: AppForSEII25264SqliteUT
    {
        private const string _Name = "Elena";
        private const string _Surname = "Pretel";
        private const string _userName = "elena@uclm.es";
        private const string _address = "Avenida España, 2";

        private const string _car1Model = "Sedán";
        private const string _car1Color = "Red";
        private const string _car2Model = "Sedán";
        private const string _car2Color = "Black";
        private const string _car3Model = "SUV";
        private const string _car3Color = "Blue";

        public PostPurchases_test()
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
                new ApplicationUser("1", "Elena", "Pretel", "elena@uclm.es", "Avenida España, 2", null),
                new ApplicationUser("2", "Pablo", "Ramón", "pablo.ramon@uclm.es", "Val General, 12", null),
                new ApplicationUser("3", "Pablo", "Ballestero", "pablo.ballestero@uclm.es", "Paseo Cervantes,8", null),
                new ApplicationUser("4", "Tomás", "González", "tomas.gonzalez@uclm.es", "Blasco Ibáñez,4", null)
            };



            var purchases = new List<Purchase>
            {
                new Purchase(1, PaymentMethodTypes.TarjetaCredito, new DateTime(2025, 1, 15), 2500000.00m, true, new List<PurchaseItem>(), users[0]),
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




        public static IEnumerable<object[]> TestCasesFor_CreatePurchase()
        {
            var purchaseNoItem = new PurchaseForCreateDTO(_Name,_Surname,_userName,_address, new List<PurchaseItemDTO>(),PaymentMethodTypes.Efectivo); //Error 1

            var purchaseItems = new List<PurchaseItemDTO>()
            {
                new PurchaseItemDTO(1,_car1Model,20000m,_car1Color,1,"")
            };

            var purchaseApplicationUser = new PurchaseForCreateDTO(_Name,_Surname,"correo@uclm.es",_address,purchaseItems,PaymentMethodTypes.Efectivo); //Error 2

            var purchaseCarNotAvailable = new PurchaseForCreateDTO(_Name,_Surname,_userName,_address,new List<PurchaseItemDTO>() {new PurchaseItemDTO(1,_car1Model,20000m,_car1Color,1,"")},PaymentMethodTypes.Efectivo); //Error 3

            var purchaseCarInsufficientStock = new PurchaseForCreateDTO(_Name,_Surname,_userName,_address,new List<PurchaseItemDTO>() {new PurchaseItemDTO(3,_car3Model,30000m,_car3Color,10,"descripcion")},PaymentMethodTypes.Efectivo); //Error 4

            var purchaseNoDescription = new PurchaseForCreateDTO(_Name, _Surname, _userName, _address, new List<PurchaseItemDTO>() { new PurchaseItemDTO(3,_car3Model,30000m,_car3Color,2,"")},PaymentMethodTypes.Efectivo); //Error examen
            
            var allTests = new List<object[]>
            {
                new object[] { purchaseNoItem, "Error! You must include at least one car to be purchased", },
                new object[] { purchaseApplicationUser, "Error! UserName is not registered", },
                new object[] { purchaseCarNotAvailable, "Error! Car Model with Id '1' is not available for being purchased from the database"},
                new object[] { purchaseCarInsufficientStock, "Error! Not enough stock for Car Id '3'. Available: 5, Requested: 10"},
                new object[] { purchaseNoDescription, "¡Error! Estás comprando demasiados coches sin descripción." }
            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreatePurchase))]
        public async Task CreatePurchase_Error_test(PurchaseForCreateDTO purchaseDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);

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
        public async Task CreatePurchase_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            var fecha = new DateTime(2025, 11, 21);

            var purchaseDTO = new PurchaseForCreateDTO(_Name, _Surname, _userName, _address, new List<PurchaseItemDTO>() {new PurchaseItemDTO(3,_car3Model, 3600000.00m, _car3Color,4)}, PaymentMethodTypes.Efectivo,fecha);


            var expectedpurchaseDetailDTO = new PurchaseDetailDTO(6,fecha,_Name,_Surname,_userName,_address,PaymentMethodTypes.Efectivo,new List<PurchaseItemDTO>() {new PurchaseItemDTO(3,_car3Model, 3600000.00m, _car3Color,4)});

            // Act
            var result = await controller.CreatePurchase(purchaseDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualPurchaseDetailDTO = Assert.IsType<PurchaseDetailDTO>(createdResult.Value);

            Assert.Equal(expectedpurchaseDetailDTO, actualPurchaseDetailDTO);

        }
    }
}
