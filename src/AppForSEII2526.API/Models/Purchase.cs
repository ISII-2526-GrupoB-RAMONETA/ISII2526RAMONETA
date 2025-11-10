namespace AppForSEII2526.API.Models
{

    public enum PaymentMethodTypes
    {//Metodos de pago}
        Efectivo,
        TarjetaCredito,
        TarjetaDebito,
        TransferenciaBancaria,
        PagoMovil,
        PayPal,
        Criptomoneda,
        Cheque

    }
    public class Purchase
    {
        
        public Purchase()
        {

        }

        public Purchase(PaymentMethodTypes paymentMethod, DateTime purchasingDate, ApplicationUser applicationUser,List<PurchaseItem> purchaseItems)
        {
            PaymentMethod = paymentMethod;
            PurchasingDate = purchasingDate;
            ApplicationUser = applicationUser;
            PurchaseItems = new List<PurchaseItem>();
            DeliveryCarDealer = true;
        }


        //public Purchase(PaymentMethodTypes paymentMethod, List<PurchaseItem> purchaseItems,ApplicationUser applicationUser)
        //{
        //    PaymentMethod = paymentMethod;
        //    ApplicationUser = applicationUser;
        //    PurchaseItems = new List<PurchaseItem>();
        //}

        [Key]
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethodTypes PaymentMethod { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchasingDate { get; set; }


        [Precision(10, 2)]
        public decimal PurchasingPrice { get; set; }

        public bool DeliveryCarDealer { get; set; }
        
        public IList<PurchaseItem> PurchaseItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }

}
