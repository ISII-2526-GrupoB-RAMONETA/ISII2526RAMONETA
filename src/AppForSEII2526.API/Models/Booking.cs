namespace AppForSEII2526.API.Models
{
    public class Booking
    {
        public Booking()
        {
        }

        public Booking(string clientAdress, string clientName, string clientPhoneNumber, string clientSurname, DateTime Date, string PaymentMethod)
        {
            clientAdress = clientAdress;
            clientName = clientName;
            clientPhoneNumber = clientPhoneNumber;
            clientSurname = clientSurname;
            Date = Date;
            PaymentMethod = PaymentMethod;
        }

        public int Id { get; set; }

        public string ClientAdress { get; set; }

        public string ClientName { get; set; }

        public string ClientPhoneNumber { get; set; }

        public string ClientSurname { get; set; }

        public DateTime Date { get; set; }

        public string PaymentMethod { get; set; }



    }
}
