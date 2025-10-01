namespace AppForSEII2526.API.Models
{
    public class PurchaseItem
    {

        public PurchaseItem()
        {

        }

        public PurchaseItem(int carId, int purchaseId, int quantity)
        {
            CarId = carId;
            PurchaseId = purchaseId;
            Quantity = quantity;
        }

        public int CarId { get; set; }
        public int PurchaseId { get; set; }
        public int Quantity { get; set; }


    }

}
