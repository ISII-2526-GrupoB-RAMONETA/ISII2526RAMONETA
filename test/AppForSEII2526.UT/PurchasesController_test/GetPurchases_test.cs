using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.PurchasesDTO;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.PurchasesController_test
{
    public class GetPurchases_test : AppForSEII25264SqliteUT
    {
        public GetPurchases_test()
        {
            var models = new List<Model>()
            {
                new Model("Sedan"),
                new Model("SUV"),
            };

            var cars = new List<Car>()
            {
                new Car("Standard", "Red", "Compact family sedan", "Toyota", 2500000.00m, 0, 2, 12000.00m, 4.50m, 1.80m, "Gasoline", "Oil change, tire rotation", 16.00m, models[0]),
                new Car("Standard", "Blue", "Family SUV with ample space", "Toyota", 3600000.00m, 6, 3, 15000.00m, 4.60m, 2.40m, "Diesel", "Tire rotation, oil change", 18.00m, models[1]),
            };

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser("1", "Elena", "Pretel", "elena@uclm.es", "Avenida España, 2", null),
            };

            var purchase = new Purchase(1, PaymentMethodTypes.TarjetaCredito, new DateTime(2025, 1, 15), 2500000.00m, true, new List<PurchaseItem>(), users[0]);
            
            purchase.PurchaseItems.Add(new PurchaseItem(cars[0], 1, purchase, 1, 1));

            _context.AddRange(models);
            _context.AddRange(cars);
            _context.AddRange(users);
            _context.Add(purchase);
            _context.SaveChanges();

        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_NotFound_test()
        {
            //Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;

            var controller = new PurchasesController(_context, logger);

            //Act
            var result = await controller.GetPurchase(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetPurchase_Found_test()
        {
            //Arrange
            var mock = new Mock<ILogger<PurchasesController>>();
            ILogger<PurchasesController> logger = mock.Object;
            var controller = new PurchasesController(_context, logger);

            var expectedPurchase = new PurchaseDetailDTO(1, new DateTime(2025, 1, 15),"Elena","Pretel","Avenida España, 2", "elena@uclm.es", PaymentMethodTypes.TarjetaCredito,new List<PurchaseItemDTO>());
            expectedPurchase.PurchaseItems.Add(new PurchaseItemDTO(1, "Sedan", 2500000.00m, "Red", 1));

            //Act
            var result = await controller.GetPurchase(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var purchaseDTOactual = Assert.IsType<PurchaseDetailDTO>(okResult.Value);
            var eq = expectedPurchase.Equals(purchaseDTOactual);

            Assert.Equal(expectedPurchase, purchaseDTOactual); 


        }

    }
}
