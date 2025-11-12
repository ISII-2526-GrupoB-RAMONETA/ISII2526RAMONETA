
namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseDetailDTO : PurchaseForCreateDTO
    {
        public PurchaseDetailDTO(int id,DateTime purchaseDate,string Name, string Surname, string Address,  
            IList<PurchaseItemDTO> purchaseItems): base(Name, Surname, Address, purchaseItems)
        {
            Id = id;
            PurchaseDate = purchaseDate;
        }

        public PurchaseDetailDTO(int id, DateTime purchaseDate, string Name, string Surname, string Address,PaymentMethodTypes paymentMethod,
            IList<PurchaseItemDTO> purchaseItems) : base(Name, Surname, Address, purchaseItems, (PaymentMethodTypes)paymentMethod)
        {
            Id = id;
            PurchaseDate = purchaseDate;
        }

        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseDetailDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   Address == dTO.Address &&
                   PaymentMethod == dTO.PaymentMethod &&
                   PurchaseItems.SequenceEqual(dTO.PurchaseItems) &&
                   PurchaseDate == dTO.PurchaseDate &&
                   Id == dTO.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Address, PaymentMethod, PurchaseItems, PurchaseDate, Id, PurchaseDate);
        }
    }
}
