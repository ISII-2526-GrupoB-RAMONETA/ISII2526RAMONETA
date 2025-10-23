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

        public Purchase(int id, PaymentMethodTypes paymentMethod, DateTime purchasingDate, decimal purchasingPrice, bool deliveryCarDealer)
        {
            PaymentMethod = paymentMethod;
            PurchasingDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            DeliveryCarDealer = deliveryCarDealer;
            
        }

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
