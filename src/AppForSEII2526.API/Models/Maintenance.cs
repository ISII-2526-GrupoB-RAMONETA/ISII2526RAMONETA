namespace AppForSEII2526.API.Models
{
    public class Maintenance
    {
        public Maintenance()
        {
        }

        public Maintenance(string name,int NumberOfDays,float Price) { 
        name = name;
        NumberOfDays = NumberOfDays;
        Price = Price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfDays { get; set; }

        public float Price { get; set; }



    }
}
