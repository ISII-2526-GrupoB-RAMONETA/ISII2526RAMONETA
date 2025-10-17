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

        [StringLength(20, ErrorMessage = "El nombre del model no puede tener más de 20 caracteres ni menos de 1.", MinimumLength=1)]
        public string Name { get; set; }

        public IList<Car> Cars { get; set; } 
    }
}


 


