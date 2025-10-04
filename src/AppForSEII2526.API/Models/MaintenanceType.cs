namespace AppForSEII2526.API.Models
{
    public class MaintenanceType
    {

        public MaintenanceType()
        {
        }

        public MaintenanceType(string type)
        {
            Type = type;
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public Maintenance Maintenance { get; set; }
    }
}
