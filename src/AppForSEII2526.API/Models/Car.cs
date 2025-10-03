namespace AppForSEII2526.API.Models
{
    public class Car

    {
        public Car()
        {

        }
        public Car(string carClass, string color, string description, string manufacturer, decimal purchasingPrice, int quantityForPurchasing, int quantityForRenting, decimal rentalItems, decimal rentingPrice, decimal reviewItems, decimal engDisplacement,string  fueltype,string maintenanceTypes,decimal purchaseItems,decimal rimSize)
        {
            CarClass = carClass;
            Color = color;
            Description = description;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
            QuantityForPurchasing = quantityForPurchasing;
            QuantityForRenting = quantityForRenting;
            RentalItems = rentalItems;
            RentingPrice = rentingPrice;
            ReviewItems = reviewItems;
            EngDisplacement = engDisplacement;
            Fueltype = fueltype;
            MaintenanceTypes = maintenanceTypes;
            PurchaseItems = purchaseItems;
            RimSize = rimSize;
        }


        public int Id { get; set; }
        public string CarClass { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasingPrice { get; set; }
        public int QuantityForPurchasing { get; set; }
        public int QuantityForRenting { get; set; }
        public decimal RentalItems { get; set; }
        public decimal RentingPrice { get; set; }
        public decimal ReviewItems { get; set; }
        public decimal EngDisplacement { get; set; }
        public string Fueltype { get; set; }
        public string MaintenanceTypes { get; set; }
        public decimal PurchaseItems { get; set; }
        public decimal RimSize { get; set; }

        public Model Model { get; set; }

        }
}
