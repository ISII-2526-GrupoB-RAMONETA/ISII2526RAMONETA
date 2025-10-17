namespace AppForSEII2526.API.Models
{
    public class Rental
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
        public Rental()
        {

        }

        public Rental(PaymentMethodTypes paymentMethod, DateTime rentingDate, decimal totalPrice, bool deliveryCarDealer, DateTime startdate, DateTime enddate)
        {
            PaymentMethod = paymentMethod;
            RentingDate = rentingDate;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
            Startdate = startdate;
            Enddate = enddate;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethodTypes PaymentMethod { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentingDate { get; set; }

        [Precision(10, 2)]
        public decimal TotalPrice { get; set; }

        public bool DeliveryCarDealer { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Startdate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Enddate { get; set; }

        public IList<RentalItem> RentalItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }

}

