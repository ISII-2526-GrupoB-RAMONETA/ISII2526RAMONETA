namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalDetailDTO : RentalForCreateDTO
    {
        public RentalDetailDTO(int id, DateTime rentalDate, string customerUserName, string customerName, string customerSurname,
            string deliveryAddress, bool deliveryCarDealer, PaymentMethodTypes paymentMethod, DateTime startdate,DateTime enddate, IList<RentalItemDTO> rentalItems)
            : base(customerUserName,
                   customerName,
                   customerSurname,
                   deliveryAddress,
                   paymentMethod,
                   deliveryCarDealer,
                   startdate,
                   enddate,
                   rentalItems)
        {
            Id = id;
            RentalDate = rentalDate;
        }
        public int Id { get; set; }

        public DateTime RentalDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RentalDetailDTO dTO &&
                   base.Equals(obj) &&
                   TotalPrice == dTO.TotalPrice &&
                   Id == dTO.Id &&
                   CompareDate(RentalDate, dTO.RentalDate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id, RentalDate);
        }
    }
}
