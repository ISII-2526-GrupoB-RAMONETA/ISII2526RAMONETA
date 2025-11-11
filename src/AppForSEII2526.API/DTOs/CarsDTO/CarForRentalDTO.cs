using System;

namespace AppForSEII2526.API.DTOs.CarsDTO
{
    public class CarForRentalDTO
    {

        public CarForRentalDTO(int id, string color, string fueltype, string manufacturer, decimal rentingPrice, string model)
        {
            Id = id;
            Color = color;
            Fueltype = fueltype;
            Manufacturer = manufacturer;
            RentingPrice = rentingPrice;
            Model = model;
        }

        public int Id { get; set; }

        public string Color { get; set; }

        public string Fueltype { get; set; }

        public string Manufacturer { get; set; }

        public decimal RentingPrice { get; set; }

        public string Model { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CarForRentalDTO dTO &&
                   Id == dTO.Id &&
                   Color == dTO.Color &&
                   Model == dTO.Model &&
                   Fueltype == dTO.Fueltype &&
                   Manufacturer == dTO.Manufacturer &&
                   RentingPrice == dTO.RentingPrice;
        }
    }
}
