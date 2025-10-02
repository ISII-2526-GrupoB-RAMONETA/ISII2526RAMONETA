namespace AppForSEII2526.API.Models
{
    public class RentalItem
    {

        public RentalItem()
        {

        }

        public RentalItem(int carId, int rentalId, int quantity)
        {
            CarId = carId;
            RentalId = rentalId;
            Quantity = quantity;
        }

        public int CarId { get; set; }
        public int RentalId { get; set; }
        public int Quantity { get; set; }


    }

}

