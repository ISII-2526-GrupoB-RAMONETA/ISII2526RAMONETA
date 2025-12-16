using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseForCreateDTO
    {
        public PurchaseForCreateDTO(string name, string surname, string userName,string address, IList<PurchaseItemDTO> purchaseItems)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Surname = surname ?? throw new ArgumentNullException(nameof(Surname));
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));
            Address = address ?? throw new ArgumentNullException(nameof(Address));
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));
        }

        public PurchaseForCreateDTO(string name,string surname, string userName,string address,IList<PurchaseItemDTO> purchaseItems,PaymentMethodTypes paymentMethod)
        {
            
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Surname = surname ?? throw new ArgumentNullException(nameof(Surname));
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));
            Address = address ?? throw new ArgumentNullException(nameof(Address));
            PaymentMethod = paymentMethod;
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));

        }

        public PurchaseForCreateDTO(string name, string surname, string userName, string address, IList<PurchaseItemDTO> purchaseItems, PaymentMethodTypes paymentMethod, DateTime purchaseDate)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Address = address;
            PaymentMethod = paymentMethod;
            PurchaseItems = purchaseItems;
            PurchaseDate = purchaseDate;
        }

        public PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your UserName")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Address")]
        public string Address { get; set; }


        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }

        public DateTime PurchaseDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   UserName == dTO.UserName &&
                   Address == dTO.Address &&
                   PaymentMethod == dTO.PaymentMethod &&
                   PurchaseItems.SequenceEqual(dTO.PurchaseItems) &&
                   PurchaseDate == dTO.PurchaseDate;
        }

        
    }
}
