
using System.Drawing;

namespace AppForSEII2526.API.DTOs.CarsDTO
{
    public class CarForPurchaseDTO
    {
        //private Model model;

        public CarForPurchaseDTO(int id, string model, string color, string fueltype, string manufacturer, decimal purchasingPrice, string description)
        {
            Id = id;
            Model = model;
            Color = color;
            Fueltype = fueltype;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
            Description = description;
        }

        public int Id { get; set; }
        
        public string Color { get; set; }

        public string Model { get; set; }

        public string Fueltype { get; set; }

        public string Manufacturer { get; set; }

        public decimal PurchasingPrice { get; set; }

        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CarForPurchaseDTO dTO &&
                   Id == dTO.Id &&
                   Color == dTO.Color &&
                   Model == dTO.Model &&
                   Fueltype == dTO.Fueltype &&
                   Manufacturer == dTO.Manufacturer &&
                   PurchasingPrice == dTO.PurchasingPrice;
        }
    }
}
