using AppForSEII2526.Web.API;
namespace AppForSEII2526.Web
{
    public class BookingStateContainer
    {

        //we create an instance of Booking when an instance of BookingStateContainer is created
        public BookingForCreateDTO Booking { get; private set; } = new BookingForCreateDTO()
        {
            BookingItems = new List<BookingItemDTO>()
        };

        //we compute the TotalPrice and TotalNumberOfDays of the maintenances we have selected for booking them
        public decimal TotalPrice
        {
            get
            {
                return (decimal)Booking.BookingItems.Sum(ri => ri.Price);
            }
        }

        public int TotalNumberOfDays
        {
            get
            {
                return Booking.BookingItems.Sum(ri => ri.NumberOfDays);
            }
        }


        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddMaintenanceToBooking(MaintenanceDTO maintenace)
        //before adding a maintenance we checked whether it has been already added
        {
            if (!Booking.BookingItems.Any(ri => ri.MaintenanceId == maintenace.Id))
                //we add it if it is not in the list
                Booking.BookingItems.Add(new BookingItemDTO()
                {
                    MaintenanceId = maintenace.Id,
                    Name = maintenace.Name,
                    Type = maintenace.Type,
                    Price = maintenace.Price,
                    NumberOfDays = maintenace.NumberOfDays
                }

             );

        }

        //to delete maintenances from the list of selected maintenances
        public void RemoveBookingItemToBook(BookingItemDTO item)
        {
            Booking.BookingItems.Remove(item);
        }

        //we eliminate all the maintenances from the list
        public void ClearBookingCart()
        {
            Booking.BookingItems.Clear();
        }

        //we have already finished the process of booking, thus, we create a new Booking
        public void BookingProcessed()
        {
            //we have finished the booking process so we create a new object without data
            Booking = new BookingForCreateDTO()
            {
                BookingItems = new List<BookingItemDTO>()

            };
        }



    }
}
