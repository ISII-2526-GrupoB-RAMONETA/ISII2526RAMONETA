namespace AppForSEII2526.API.Models
{
    public class Car

    {
        public Car()
        {

        }
        public Car(string carClass, string color, string description, string manufacturer, decimal purchasingPrice, int quantityForPurchasing, int quantityForRenting, decimal rentingPrice, decimal reviewItems, decimal engDisplacement,string  fueltype,string maintenanceTypes,decimal purchaseItems,decimal rimSize)
        {
            //Comprar coche
            CarClass = carClass;
            Color = color;
            Description = description;
            Manufacturer = manufacturer;
            PurchasingPrice = purchasingPrice;
            QuantityForPurchasing = quantityForPurchasing;
            QuantityForRenting = quantityForRenting;
            RentingPrice = rentingPrice;
            ReviewItems = reviewItems;
            
            //Alquilar coche
            EngDisplacement = engDisplacement;
            Fueltype = fueltype;
            MaintenanceTypes = maintenanceTypes;
            PurchaseItems = purchaseItems;
            RimSize = rimSize;
        }

        [Key]
        public int Id { get; set; }

        //Comprar coche

        [StringLength(50, ErrorMessage = "Car class cannot be longer than 50 characters.")]
        public string CarClass { get; set; }


        [StringLength(30, ErrorMessage = "Color cannot be longer than 30 characters.")]
        public string Color { get; set; }
        

        [StringLength(255, ErrorMessage = "Description cannot be longer than 255 characters.")]
        public string Description { get; set; }
        

        [StringLength(50, ErrorMessage = "Manufacturer cannot be longer than 50 characters.")]
        public string Manufacturer { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, float.MaxValue, ErrorMessage = "Minimum price is 0.5 ")]
        [Display(Name = "Purchasing Price")]
        [Precision(10, 2)]
        public decimal PurchasingPrice { get; set; }


        [Display(Name = "Quantity For Purchasing")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum quantity for Purchase is 1")]
        public int QuantityForPurchasing { get; set; }


        [Display(Name = "Quantity For Renting")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity for renting is 1")]
        public int QuantityForRenting { get; set; }


        public IList<RentalItem> RentalItems { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100000, ErrorMessage = "Minimum is 0.5 and maximum 100.000")]
        [Display(Name = "Renting Price")]
        public decimal RentingPrice { get; set; }

        [Display(Name = "Review Items")]
        [Range(0, int.MaxValue, ErrorMessage = "Review items cannot be negative.")]
        public decimal ReviewItems { get; set; }



        //Alquilar coche
        public decimal EngDisplacement { get; set; }
        public string Fueltype { get; set; }
        public string MaintenanceTypes { get; set; }
        public decimal PurchaseItems { get; set; }
        public decimal RimSize { get; set; }


        //Relación con Model
        public Model Model { get; set; }

        }
}
