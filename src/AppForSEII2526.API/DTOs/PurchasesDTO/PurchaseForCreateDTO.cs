namespace AppForSEII2526.API.DTOs.PurchasesDTO
{
    public class PurchaseForCreateDTO
    {
        public PurchaseForCreateDTO(string customerUserName,string customerNameSurname,string deliveryAddress,IList<PurchaseItemDTO> purchaseItems)
        {
            CustomerUserName = customerUserName ?? throw new ArgumentNullException(nameof(customerUserName));
            CustomerNameSurname = customerNameSurname ?? throw new ArgumentNullException(nameof(customerNameSurname));
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            PurchaseItems = purchaseItems ?? throw new ArgumentNullException(nameof(purchaseItems));
        }

        public PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>();
        }

        public string CustomerUserName { get; set; }

        public string CustomerNameSurname { get; set; }

        public string DeliveryAddress { get; set; }

        public IList<PurchaseItemDTO> PurchaseItems { get; set; }


    }
}
