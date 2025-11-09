using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseForCreateDTO
    {
        public PurchaseForCreateDTO(string name, string surname, string address, IList<PurchaseItemDTO> purchaseItems)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Surname = surname ?? throw new ArgumentNullException(nameof(Surname));
            Address = address ?? throw new ArgumentNullException(nameof(Address));
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));
        }

        public PurchaseForCreateDTO(string name,string surname,string address,IList<PurchaseItemDTO> purchaseItems,PaymentMethodTypes paymentMethod)
        {
            
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Surname = surname ?? throw new ArgumentNullException(nameof(Surname));
            Address = address ?? throw new ArgumentNullException(nameof(Address));
            PaymentMethod = paymentMethod;
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));

        }

        public PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        public string Surname { get; set; }

        
        public string Address { get; set; }

        //[Required]
        //public string Email { get; set; }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }

        public DateTime PurchaseDate { get; set; }


    }
}
