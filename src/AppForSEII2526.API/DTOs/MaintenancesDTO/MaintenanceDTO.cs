namespace AppForSEII2526.API.DTOs.MaintenancesDTO
{
    public class MaintenanceDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public MaintenanceDTO(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
    