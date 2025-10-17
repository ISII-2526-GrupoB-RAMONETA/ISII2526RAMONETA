namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(CarId), nameof(PurchaseId))]
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


        
        public Car Car { get; set; }
        public int CarId { get; set; }

        public Purchase Purchase { get; set; }
        public int PurchaseId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "You must provide a quantity higher than 1")]
        public int Quantity { get; set; }



    }

}
