using AppForSEII2526.API.DTOs.MaintenancesDTO;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalItemDTO
    {
        public int CarId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [Precision(10, 2)]
        public decimal PriceForRenting { get; set; }

        public int Quantity { get; set; }

        public RentalItemDTO(int carId, string model, string manufacturer, decimal priceForRenting, int quantity)
        {
            CarId = carId;
            Model = model;
            Manufacturer = manufacturer;
            PriceForRenting = priceForRenting;
            Quantity = quantity;
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalItemDTO dTO &&
                   CarId == dTO.CarId &&
                   Model == dTO.Model &&
                   Manufacturer == dTO.Manufacturer &&
                   PriceForRenting == dTO.PriceForRenting &&
                   Quantity == dTO.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Model, Manufacturer, PriceForRenting, Quantity);
        }
    }
}
