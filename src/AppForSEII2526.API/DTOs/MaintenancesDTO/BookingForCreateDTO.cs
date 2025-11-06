using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingForCreateDTO
    {
        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        [StringLength(20, ErrorMessage = "Name must have less than 20 characters")]
        public string CustomerName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(20, ErrorMessage = "Surname must have less than 50 characters")]
        public string CustomerSurname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address must have at least 5 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address")]
        public string Address { get; set; }
        [Required]
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

        public BookingForCreateDTO(string customerUserName,string customerName, string customerSurname, string address, PaymentMethodTypes paymentMethod, string? phoneNumber, IList<BookingItemDTO> bookingItems)
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
