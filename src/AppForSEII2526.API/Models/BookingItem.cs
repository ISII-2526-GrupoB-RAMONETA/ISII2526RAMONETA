namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BookingId), nameof(MantId))]
    public class BookingItem
    {

        public BookingItem()
        {
        }

        public BookingItem(Booking booking,string comment,Maintenance maintenance)
        {
            Booking = booking;
            BookingId= booking.Id;

            Comment = comment;

            Maintenance = maintenance;
            MantId= maintenance.Id;

        }


        public Booking Booking { get; set; }

        public int BookingId { get; set; }


        [StringLength(300, ErrorMessage = "Comment cannot be longer than 300 characters.")]
        public string Comment { get; set; }
        
        public Maintenance Maintenance { get; set; }

        public int MantId { get; set; }
    }
}
