namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalItemDTO
    {
        public int CarId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public RentalItemDTO(int carId, string model, string manufacturer, decimal price, int quantity)
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            Price = price;
            Quantity = quantity;
        }
    }
}
