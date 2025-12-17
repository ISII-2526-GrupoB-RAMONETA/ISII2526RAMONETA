
namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class BookingItemDTO
    {
        public int MaintenanceId { get; set; }

        public string Name { get; set; }

        public int NumberOfDays { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Comment must be a string with a minimum length of 2 and a maximum length of 200")]
        public string Comment { get; set; }

        public string Type { get; set; }

        public BookingItemDTO(int maintenanceId, string name, int numberOfDays, decimal price, string comment,string type)
        {
            MaintenanceId = maintenanceId;
            Name = name;
            NumberOfDays = numberOfDays;
            Price = price;
            Comment = comment;
            Type=type;
        }

        public override bool Equals(object? obj)
        {
            return obj is BookingItemDTO dTO &&
                   MaintenanceId == dTO.MaintenanceId &&
                   Name == dTO.Name &&
                   NumberOfDays == dTO.NumberOfDays &&
                   Price == dTO.Price &&
                   Comment == dTO.Comment &&
                   Type == dTO.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MaintenanceId, Name, NumberOfDays, Price, Comment, Type);
        }
    }
}
