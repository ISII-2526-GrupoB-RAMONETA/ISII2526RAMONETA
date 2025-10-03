namespace AppForSEII2526.API.Models
{
    public class Purchase
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

        public int Id { get; set; }
        public PaymentMethodTypes PaymentMethod { get; set; }
        public DateTime PurchasingDate { get; set; }
        public decimal PurchasingPrice { get; set; }
        public bool DeliveryCarDealer { get; set; }
        
        public IList<PurchaseItem> PurchaseItems { get; set; }

    }

}
