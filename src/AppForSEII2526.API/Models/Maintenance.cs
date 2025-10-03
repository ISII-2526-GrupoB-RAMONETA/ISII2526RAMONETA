namespace AppForSEII2526.API.Models
{
    public class Maintenance
    {
        public Maintenance()
        {
        }

        public Maintenance(string name,int numberOfDays,float price) { 
            Name = name;
            NumberOfDays = NumberOfDays;
            Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfDays { get; set; }

        public float Price { get; set; }



    }
}
