using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;

namespace AppForSEII2526.UT.CarsController_test
{
    public class GetCoches_FILTRO_COLOR_MODELO_test : AppForSEII25264SqliteUT
    {
        public GetCoches_FILTRO_COLOR_MODELO_test()
        {
            var models = new List<Model>()
            {
                new Model("Sedán"),
                new Model("SUV"),
                new Model("Hatchback"),
                new Model("Coupe"),
                new Model("Pickup Truck")
            };

            var cars = new List<Car>()
            {
                new Car("Standard", "Red", "Compact family sedan", "Toyota", 2500000.00m, 0, 2, 12000.00m, 4.50m, 1.80m, "Gasoline", "Oil change, tire rotation", 16.00m, 1),
                new Car("Premium", "Black", "Luxury business sedan", "Mercedes-Benz", 4800000.00m, 4, 2, 20000.00m, 4.80m, 2.00m, "Hybrid", "Oil change, battery check", 17.00m, 1),
                new Car("Standard", "Blue", "Family SUV with ample space", "Toyota", 3600000.00m, 6, 3, 15000.00m, 4.60m, 2.40m, "Diesel", "Tire rotation, oil change", 18.00m, 2),
                new Car("Compact", "White", "Fuel-efficient city hatchback", "Volkswagen", 1900000.00m, 7, 5, 8000.00m, 4.30m, 1.60m, "Gasoline", "Oil change, tire replacement", 15.00m, 3),
                new Car("Sport", "Red", "High-performance two-door coupe", "Audi", 6200000.00m, 2, 1, 28000.00m, 4.90m, 3.20m, "Gasoline", "Oil change, brake inspection", 19.00m, 4)
            };

            var purchases = new List<Purchase>()
            {
            };

            var purchasesItems = new List<PurchaseItem>()
            {
            };
        }
        
    }
}
