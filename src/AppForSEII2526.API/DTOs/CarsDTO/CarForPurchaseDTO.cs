
using System.Drawing;

namespace AppForSEII2526.API.DTOs.CarsDTO
{
    public class CarForPurchaseDTO
    {
        //private Model model;

        public CarForPurchaseDTO(int id, string model, string color, string fueltype, string manufacturer, decimal purchasingPrice)
        {
            Id = id;
            Model = model;
            Color = color;
            Fueltype = fueltype;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
        }

        public int Id { get; set; }
        
        public string Color { get; set; }

        public string Model { get; set; }

        public string Fueltype { get; set; }

        public string Manufacturer { get; set; }

        public decimal PurchasingPrice { get; set; }
    }
}
