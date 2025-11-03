using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingForCreateDTO
    {
        public string CustomerUserName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerSurname { get; set; }

        public string Address { get; set; }

        public PaymentMethodTypes PaymentMethod { get; set; }

        public string? PhoneNumber { get; set; }


        public IList<BookingItemDTO> BookingItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return BookingItems.Sum(ri => ri.Price);
            }
        }

        public int TotalNumberOfDays { get
            {
                return BookingItems.Sum(ri => ri.NumberOfDays);
            }
        }

       

        

        public BookingForCreateDTO(string customerUserName,string customerName, string customerSurname, string address, PaymentMethodTypes paymentMethod, IList<BookingItemDTO> bookingItems)
        {
            CustomerUserName = customerUserName;
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            Address = address;
            PaymentMethod = paymentMethod;
            BookingItems = bookingItems;
            
        }

        public BookingForCreateDTO(string customerUserName,string customerName, string customerSurname, string address, PaymentMethodTypes paymentMethod, IList<BookingItemDTO> bookingItems, string? phoneNumber)
        {
            CustomerUserName = customerUserName;
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            Address = address;
            PaymentMethod = paymentMethod;
            BookingItems = bookingItems;
            PhoneNumber = phoneNumber;
        }

        public BookingForCreateDTO() { 
        BookingItems= new List<BookingItemDTO>();   
        }


    }
}
