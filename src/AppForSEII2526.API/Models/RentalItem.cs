namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {

        public RentalItem()
        {

        }

        public RentalItem(int carId, int rentalId, int quantity)
        {
            RentalId = rentalId;
            Quantity = quantity;
        }

        public Car Car { get; set; }
        public int CarId { get; set; }
        public Rental Rental { get; set; }
        public int RentalId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }


    }

}

