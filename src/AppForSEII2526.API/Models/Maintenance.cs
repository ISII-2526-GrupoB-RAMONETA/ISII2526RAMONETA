namespace AppForSEII2526.API.Models
{
    public class Maintenance
    {
        public Maintenance()
        {
        }

        public Maintenance(string name,int numberOfDays,decimal price,MaintenanceType maintenanceType) { 
            Name = name;
            NumberOfDays = numberOfDays;
            Price = price;
            MaintenanceType = maintenanceType;
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Display(Name = "Number of days")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum days for maintenance is 1")]
        public int NumberOfDays { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }

        //public IList<MaintenanceType> MaintenanceTypes { get; set; }
        public MaintenanceType MaintenanceType { get; set; }

        public IList<BookingItem> BookingItems { get; set; }


    }
}
