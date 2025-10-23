
namespace AppForSEII2526.API.DTOs.CarsDTO
{
    public class CarForRentalDTO
    {
        private Model model;

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
    }
}
