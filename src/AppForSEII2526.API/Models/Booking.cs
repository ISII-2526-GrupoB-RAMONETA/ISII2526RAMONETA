namespace AppForSEII2526.API.Models
{
    public class Booking
    {

        public enum PaymentMethodTypes
        {//Metodos de pago}
            Efectivo,
            TarjetaCredito,
            TarjetaDebito,
            TransferenciaBancaria,
            PagoMovil,
            PayPal,
            Criptomoneda,
            Cheque

        }
        public Booking()
        {
        }

        public Booking(string clientAdress, string clientName, string clientPhoneNumber, string clientSurname, DateTime date, PaymentMethodTypes paymentMethod)
        {
            ClientAdress = clientAdress;
            ClientName = clientName;
            ClientPhoneNumber = clientPhoneNumber;
            ClientSurname = clientSurname;
            Date = date;
            PaymentMethod = paymentMethod;
        }

        public int Id { get; set; }

        [StringLength(150, ErrorMessage = "Adress cannot be longer than 150 characters.")]
        public string ClientAdress { get; set; }

        [StringLength(50, ErrorMessage = "Client Name cannot be longer than 50 characters.")]
        public string ClientName { get; set; }


        [StringLength(15, ErrorMessage = "Client PhoneNumber cannot be longer than 15 characters.")]
        public string ClientPhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Client Surname cannot be longer than 50 characters.")]
        public string ClientSurname { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Display(Name = "Payment Method")]
        public PaymentMethodTypes PaymentMethod { get; set; }

        public IList<BookingItem> BookingItems { get; set; }



    }
}
