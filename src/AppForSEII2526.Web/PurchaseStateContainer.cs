using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        //creamos una instancia de Purchase cuando se crea una instancia de RentalState Container
        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>()
        };

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddCarToPurchase(PurchaseItemDTO purchaseItem)
        {
            var existingItem = Purchase.PurchaseItems.FirstOrDefault(pi => pi.CarID == purchaseItem.CarID);
            //Buscamos si el coche ya está en la lista
            if (existingItem == null)
            {
                //lo agregamos si no está en la lista
                Purchase.PurchaseItems.Add(new PurchaseItemDTO()
                {
                    CarID = purchaseItem.CarID,
                    Model = purchaseItem.Model,
                    PurchasingPrice = purchaseItem.PurchasingPrice,
                    Color = purchaseItem.Color,
                    Quantity = purchaseItem.Quantity,
                    Description = purchaseItem.Description
                }
                );
            }
            else
            {
                //si ya está en la lista, aumentamos la cantidad
                existingItem.Quantity += purchaseItem.Quantity;
            }
        }

        //para eliminar un coche del carrito de compras
        public void RemovePurchaseItemToPurchase(PurchaseItemDTO purchaseItem)
        {
            Purchase.PurchaseItems.Remove(purchaseItem);
        }

        //para vaciar el carrito de compras
        public void ClearPurchasingCart()
        {
            Purchase.PurchaseItems.Clear();
        }

        //para resetear la compra después de que se haya procesado
        public void PurchaseProcessed()
        {
            Purchase = new PurchaseForCreateDTO()
            {
                PurchaseItems = new List<PurchaseItemDTO>()
            };
        }

    }
}
