namespace AppForSEII2526.API.Models
{
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
        public int Quantity { get; set; }


    }

}

