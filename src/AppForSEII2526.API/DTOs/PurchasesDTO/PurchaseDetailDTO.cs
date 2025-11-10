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

        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

    }
}
