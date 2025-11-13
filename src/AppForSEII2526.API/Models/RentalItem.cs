using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(RentalId))]
    public class RentalItem
    {
        public RentalItem() { }

        public RentalItem(Car car, Rental rent, int quantity)
        {
            Car = car;
            CarId = car.Id;
            Rental = rent;
            RentalId = rent.Id;
            Quantity = quantity;
        }
        public RentalItem(int carId, int rentId, int quantity)
        {
            CarId = carId;
            RentalId = rentId;
            Quantity = quantity;
        }


        public RentalItem(Car car, int carId, Rental rental, int rentalId, int quantity)
        {
            Car = car;
            CarId = carId;
            Rental = rental;
            RentalId = rentalId;
            Quantity = quantity;
        }


        public Car Car { get; set; }
        public int CarId { get; set; }
        public Rental Rental { get; set; }
        //public Rental Rental { get; set; }
        public int RentalId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }
    }

}

