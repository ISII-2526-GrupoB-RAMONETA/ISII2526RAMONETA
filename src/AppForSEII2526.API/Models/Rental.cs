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

        public Rental(PaymentMethodTypes paymentMethod, DateTime rentingDate, decimal totalPrice, bool deliveryCarDealer, string name, string surname, DateTime startdate, DateTime enddate)
        {
            PaymentMethod = paymentMethod;
            RentingDate = rentingDate;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
            Name = name;
            Surname = surname;
            Startdate = startdate;
            Enddate = enddate;
        }

        public int Id { get; set; }
        public PaymentMethodTypes PaymentMethod { get; set; }
        public DateTime RentingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool DeliveryCarDealer { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }

        public IList<RentalItem> RentalItems { get; set; }
    }

}

