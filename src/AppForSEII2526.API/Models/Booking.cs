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

        public string ClientAdress { get; set; }

        public string ClientName { get; set; }

        public string ClientPhoneNumber { get; set; }

        public string ClientSurname { get; set; }

        public DateTime Date { get; set; }

        public PaymentMethodTypes PaymentMethod { get; set; }



    }
}
