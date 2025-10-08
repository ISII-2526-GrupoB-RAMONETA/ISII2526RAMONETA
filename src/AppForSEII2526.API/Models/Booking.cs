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

    }
}
