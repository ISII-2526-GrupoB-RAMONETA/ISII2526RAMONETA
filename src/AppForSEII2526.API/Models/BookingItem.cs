namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {

        public BookingItem()
        {
        }

        public BookingItem(Booking Booking,string Comment,Maintenance Maintenance)
        {
            Booking = Booking;
            BookingID= Booking.Id;

            Comment = Comment;

            Maintenance = Maintenance;
            MandID= Maintenance.Id;

        }

        public int Id { get; set; }

        public Booking Booking { get; set; }

        public int BookingID { get; set; }

        public string Comment { get; set; }

        public Maintenance Maintenance { get; set; }

        public int MandID { get; set; }
    }
}
