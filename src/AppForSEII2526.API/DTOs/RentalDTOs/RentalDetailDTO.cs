namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalDetailDTO : RentalForCreateDTO
    {
        public RentalDetailDTO(int id, DateTime rentalDate, string customerUserName, string customerNameSurname,
            string deliveryAddress, PaymentMethodTypes paymentMethod, DateTime startdate,DateTime enddate, decimal totalPrice, IList<RentalItemDTO> rentalItems)
            : base(customerUserName,
                   customerNameSurname,
                   deliveryAddress,
                   paymentMethod,
                   startdate,
                   enddate,
                   totalPrice,
                   rentalItems)
        {
            Id = id;
            RentalDate = rentalDate;
        }
        public int Id { get; set; }

        public DateTime RentalDate { get; set; }

        //public override bool Equals(object? obj)
        //{
        //    return obj is RentalDetailDTO dTO &&
        //           base.Equals(obj) &&
        //           TotalPrice == dTO.TotalPrice &&
        //           Id == dTO.Id &&
        //           CompareDate(RentalDate, dTO.RentalDate);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(base.GetHashCode(), Id, RentalDate);
        //}
    }
}
