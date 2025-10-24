namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingDetailDTO : BookingForCreateDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public BookingDetailDTO(int id, DateTime date, string customerUserName, string customerNameSurname,
            string address, PaymentMethodTypes paymentMethod, IList<BookingItemDTO> bookingItems)
        : base(customerUserName, customerNameSurname, address,
              paymentMethod,
              bookingItems
              )
        {
            Id = id;
            Date = date;


        }



    }
}
