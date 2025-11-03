namespace AppForSEII2526.API.Models
{
    public class Booking
    {

        //public enum PaymentMethodTypes
        //{//Metodos de pago}
        //    Efectivo,
        //    TarjetaCredito,
        //    TarjetaDebito,
        //    TransferenciaBancaria,
        //    PagoMovil,
        //    PayPal,
        //    Criptomoneda,
        //    Cheque

        //}
        public Booking()
        {
        }

        public Booking(DateTime date, PaymentMethodTypes paymentMethod)
        {
            Date = date;
            PaymentMethod = paymentMethod;
        }

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Display(Name = "Payment Method")]
        public PaymentMethodTypes PaymentMethod { get; set; }

        public IList<BookingItem> BookingItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Booking(DateTime date, PaymentMethodTypes paymentMethod, IList<BookingItem> bookingItems, ApplicationUser applicationUser)
        {
            
            Date = date;
            PaymentMethod = paymentMethod;
            BookingItems = bookingItems;
            ApplicationUser = applicationUser;
        }



        //public string CustomerUserName { get; set; }

        //public string CustomerNameSurname { get; set; }

        //public string Address { get; set; }

        //public string? PhoneNumber { get; set; }

        //public Booking(DateTime date, PaymentMethodTypes paymentMethod, IList<BookingItem> bookingItems, ApplicationUser applicationUser, string customerUserName, string customerNameSurname, string address, string? phonenumber) : this(date, paymentMethod)
        //{
        //    BookingItems = bookingItems;
        //    ApplicationUser = applicationUser;
        //    CustomerUserName = customerUserName;
        //    CustomerNameSurname = customerNameSurname;
        //    Address = address;
        //    PhoneNumber = phonenumber;
        //}


    }
}
