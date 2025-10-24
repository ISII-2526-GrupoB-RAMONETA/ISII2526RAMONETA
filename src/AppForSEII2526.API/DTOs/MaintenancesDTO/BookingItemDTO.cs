namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingItemDTO
    {
        public int MaintenanceId { get; set; }

        public string Name { get; set; }

        public int NumberOfDays { get; set; }

        public decimal Price { get; set; }

        public string Comment { get; set; }

        public string Type { get; set; }

        public BookingItemDTO(int maintenaceId, string name, int numberOfDays, decimal price, string comment,string type)
        {
            MaintenanceId = maintenaceId;
            Name = name;
            NumberOfDays = numberOfDays;
            Price = price;
            Comment = comment;
            Type=type;
        }
    }
}
