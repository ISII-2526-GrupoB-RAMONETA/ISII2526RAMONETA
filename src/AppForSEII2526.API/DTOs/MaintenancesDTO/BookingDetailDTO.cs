
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingDetailDTO : BookingForCreateDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public BookingDetailDTO(int id, DateTime date, string customerUserName,string customerName, string customerSurname,
            string address, PaymentMethodTypes paymentMethod, string? phoneNumber, IList<BookingItemDTO> bookingItems)
        : base(customerUserName,customerName, customerSurname, address,
              paymentMethod,
              phoneNumber,
              bookingItems
              )
        {
            Id = id;
            Date = date;


        }

        public override bool Equals(object? obj)
        {
            return obj is BookingDetailDTO dTO &&
                   CustomerUserName == dTO.CustomerUserName &&
                   CustomerName == dTO.CustomerName &&
                   CustomerSurname == dTO.CustomerSurname &&
                   Address == dTO.Address &&
                   PaymentMethod == dTO.PaymentMethod &&
                   PhoneNumber == dTO.PhoneNumber &&
                   BookingItems.SequenceEqual(dTO.BookingItems) &&
                   TotalPrice == dTO.TotalPrice &&
                   TotalNumberOfDays == dTO.TotalNumberOfDays &&
                   Id == dTO.Id &&
                   CompareDate(Date, dTO.Date);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(CustomerUserName);
            hash.Add(CustomerName);
            hash.Add(CustomerSurname);
            hash.Add(Address);
            hash.Add(PaymentMethod);
            hash.Add(PhoneNumber);
            hash.Add(BookingItems);
            hash.Add(TotalPrice);
            hash.Add(TotalNumberOfDays);
            hash.Add(Id);
            hash.Add(Date);
            return hash.ToHashCode();
        }
    }
}
