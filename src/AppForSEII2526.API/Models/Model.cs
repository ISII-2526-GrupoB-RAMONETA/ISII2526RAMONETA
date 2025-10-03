namespace AppForSEII2526.API.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Model
    {

        public Model()
        {

        }

        public Model(string name)
        {
                       Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}


 


