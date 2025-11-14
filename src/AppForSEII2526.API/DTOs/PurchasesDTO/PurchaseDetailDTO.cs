

namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseDetailDTO : PurchaseForCreateDTO
    {
        public PurchaseDetailDTO(int id,DateTime purchaseDate,string Name, string Surname, string UserName,string Address,  
            IList<PurchaseItemDTO> purchaseItems): base(Name, Surname, UserName,Address, purchaseItems)
        {
            Id = id;
            PurchaseDate = purchaseDate;
        }

        public PurchaseDetailDTO(int id, DateTime purchaseDate, string Name, string Surname, string UserName,string Address,PaymentMethodTypes paymentMethod,
            IList<PurchaseItemDTO> purchaseItems) : base(Name, Surname, Address, UserName, purchaseItems, (PaymentMethodTypes)paymentMethod)
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
                   UserName == dTO.UserName &&
                   Address == dTO.Address &&
                   PaymentMethod == dTO.PaymentMethod && 
                   PurchaseItems.SequenceEqual(dTO.PurchaseItems)&&
                   PurchaseDate == dTO.PurchaseDate &&
                   Id == dTO.Id &&
                   PurchaseDate == dTO.PurchaseDate;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(UserName);
            hash.Add(Address);
            hash.Add(PaymentMethod);
            hash.Add(PurchaseItems);
            hash.Add(PurchaseDate);
            hash.Add(Id);
            hash.Add(PurchaseDate);
            return hash.ToHashCode();
        }
    }
}
