
namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseItemDTO
    {
        public PurchaseItemDTO()
        {
        }   
        public PurchaseItemDTO(int carID, string model, decimal purchasingPrice, string color, int quantity,string description)
        {
            CarID = carID;
            Model = model;
            PurchasingPrice = purchasingPrice;
            Color = color;
            Quantity = quantity;
            Description = description;
        }

        public PurchaseItemDTO(int carID, string model, decimal purchasingPrice, string color, int quantity)
        {
            CarID = carID;
            Model = model;
            PurchasingPrice = purchasingPrice;
            Color = color;
            Quantity = quantity;
        }

        public int CarID { get; set; }

        public string Model { get; set; }

        public decimal PurchasingPrice { get; set; }

        public string Color { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public decimal TotalPrice => PurchasingPrice * Quantity;

        public override bool Equals(object? obj)
        {
            return obj is PurchaseItemDTO dTO &&
                   CarID == dTO.CarID &&
                   Model == dTO.Model &&
                   PurchasingPrice == dTO.PurchasingPrice &&
                   Color == dTO.Color &&
                   Quantity == dTO.Quantity &&
                   Description == dTO.Description &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarID, Model, PurchasingPrice, Color, Quantity, Description);
        }
    }
}
