namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {

        public BookingItem()
        {
        }

        public BookingItem(Booking booking,string comment,Maintenance maintenance)
        {
            Booking = booking;
            BookingID= booking.Id;

            Comment = comment;

            Maintenance = maintenance;
            MandID= maintenance.Id;

        }

        public int Id { get; set; }

        public Booking Booking { get; set; }

        public int BookingID { get; set; }


        [StringLength(300, ErrorMessage = "Comment cannot be longer than 300 characters.")]
        public string Comment { get; set; }
        
        public Maintenance Maintenance { get; set; }

        public int MandID { get; set; }
    }
}
