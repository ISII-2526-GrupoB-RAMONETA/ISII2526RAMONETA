namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalItemDTO
    {
        public int CarId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public decimal PriceForRenting { get; set; }

        public int Quantity { get; set; }
        public string? Description { get; set; }

        public RentalItemDTO(int carId, string model, string manufacturer, decimal priceForRenting,int quantity, string? description)
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            PriceForRenting = priceForRenting;
            Quantity = quantity;
            Description = description;
        }
    }
}
