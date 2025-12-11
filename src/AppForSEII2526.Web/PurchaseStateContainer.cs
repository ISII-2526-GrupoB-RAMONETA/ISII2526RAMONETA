using AppForSEII2526.Web.API;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        //creamos una instancia de Purchase cuando se crea una instancia de PurchaseState Container
        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {
            PurchaseItems = new List<PurchaseItemDTO>()
        };

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddCarToPurchase(CarForPurchaseDTO car)
        {
            if (!Purchase.PurchaseItems.Any(pi => pi.CarID == car.Id))
                //we add it if it is not in the list
                Purchase.PurchaseItems.Add(new PurchaseItemDTO()
                {
                    CarID = purchaseItem.CarID,
                    Model = purchaseItem.Model,
                    PurchasingPrice = purchaseItem.PurchasingPrice,
                    Color = purchaseItem.Color,
                    Quantity = 1,
                    Description = purchaseItem.Description,
                    TotalPrice = purchaseItem.TotalPrice
                }
            );
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
