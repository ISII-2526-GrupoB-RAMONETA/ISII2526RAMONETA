using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingForCreateDTO
    {
        public string CustomerUserName { get; set; }

        public string CustomerUserSurname { get; set; }

        public string Address { get; set; }

        public PaymentMethodTypes PaymentMethod { get; set; }


        public IList<BookingItemDTO> BookingItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return BookingItems.Sum(ri => ri.Price * ri.NumberOfDays);
            }
        }

        public int NumberOfDays { get
            {
                return BookingItems.Sum(ri => ri.NumberOfDays);
            }
        }

        public BookingForCreateDTO(string customerUserName, string customerUserSurname, string address, PaymentMethodTypes paymentMethod, IList<BookingItemDTO> bookingItems)
        {
            CustomerUserName = customerUserName;
            CustomerUserSurname = customerUserSurname;
            Address = address;
            PaymentMethod = paymentMethod;
            BookingItems = bookingItems;
        }

        public BookingForCreateDTO() { 
        BookingItems= new List<BookingItemDTO>();   
        }
    }
}
