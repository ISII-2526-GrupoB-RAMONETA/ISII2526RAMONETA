namespace AppForSEII2526.API.Models
{
    public class PurchaseItem
    {

        public PurchaseItem()
        {

        }

        public PurchaseItem(int purchaseId, int quantity)
        {
            PurchaseId = purchaseId;
            Quantity = quantity;
        }

        public int CarId { get; set; }
        public int PurchaseId { get; set; }
        public int Quantity { get; set; }


    }

}
