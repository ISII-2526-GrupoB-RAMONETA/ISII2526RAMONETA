using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingForCreateDTO
    {
        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be a string with a minimum length of 2 and a maximum length of 20")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        public string CustomerName { get; set; }


        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname must be a string with a minimum length of 2 and a maximum length of 30")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        public string CustomerSurname { get; set; }


        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address must be a string with a minimum length of 5 and a maximum length of 50")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Address")]
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


        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is BookingForCreateDTO dTO &&
                   CustomerUserName == dTO.CustomerUserName &&
                   CustomerName == dTO.CustomerName &&
                   CustomerSurname == dTO.CustomerSurname &&
                   Address == dTO.Address &&
                   PaymentMethod == dTO.PaymentMethod &&
                   PhoneNumber == dTO.PhoneNumber &&
                   BookingItems.SequenceEqual(dTO.BookingItems) &&
                   TotalPrice == dTO.TotalPrice &&
                   TotalNumberOfDays == dTO.TotalNumberOfDays;
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
            return hash.ToHashCode();
        }
    }
}
