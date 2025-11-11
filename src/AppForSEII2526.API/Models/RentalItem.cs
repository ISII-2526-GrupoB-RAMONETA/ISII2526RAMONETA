namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {

            public RentalItem() { }

        public RentalItem(Car car, Rental rent)
        {
            Car = car;
            CarId = car.Id;
            Rent = rent;
            RentalId = rent.Id;
        }

        public RentalItem(Car car, Rental rent, string? description) : this(car, rent)
        {
            Description = description;
            PriceForRenting = car.RentingPrice;
        }

        public RentalItem(int carId, Rental rental, decimal priceForRenting)
        {
            CarId = carId;
            Rent = rental;
            RentalId = rental?.Id ?? 0;
            PriceForRenting = priceForRenting;
        }

        public RentalItem(int carId, Rental rental, decimal priceForRenting, string? description) : this(carId, rental, priceForRenting) => Description = description;


        public Car Car { get; set; }
        public int CarId { get; set; }
        public Rental Rent { get; set; }
        //public Rental Rental { get; set; }
        public int RentalId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public decimal PriceForRenting { get; set; }
    }

}

