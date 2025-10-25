namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseDetailDTO : PurchaseForCreateDTO
    {
        public PurchaseDetailDTO(int id,DateTime purchaseDate,string customerUserName, string customerNameSurname, string deliveryAddress,  
            IList<PurchaseItemDTO> purchaseItems): base(customerUserName, customerNameSurname, deliveryAddress, purchaseItems)
        {
            Id = id;
            PurchaseDate = purchaseDate;
        }

        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

    }
}
