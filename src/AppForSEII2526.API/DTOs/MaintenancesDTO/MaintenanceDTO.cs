namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class MaintenanceDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int NumberOfDays { get; set; }

        public MaintenanceDTO(int id, string name, string type, decimal price, int days)
        {
            Id = id;
            Name = name;
            Type = type;
            Price = price;
            NumberOfDays = days;
        }

        public override bool Equals(object? obj)
        {
            return obj is MaintenanceDTO dTO &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   Type == dTO.Type &&
                   Price == dTO.Price &&
                   NumberOfDays == dTO.NumberOfDays;
        }
    }
}
    