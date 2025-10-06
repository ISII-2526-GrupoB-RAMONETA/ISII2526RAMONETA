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

        [StringLength(50, ErrorMessage = "Type cannot be longer than 50 characters.")]
        public string Type { get; set; }

        public Maintenance Maintenance { get; set; }
    }
}
