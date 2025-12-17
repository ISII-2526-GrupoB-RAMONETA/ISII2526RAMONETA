using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium; // Necesario
using Xunit.Abstractions; // Necesario para ITestOutputHelper

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class DetailPurchase_PO : PageObject
    {
        public DetailPurchase_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckPurchaseDetail(string expectedName, string expectedAddress, DateTime purchaseDate, string expectedPrice)
        {
            // 1. Mejora: Usar espera explicita en vez de Thread.Sleep si es posible.
            // Si tu clase base tiene WaitForBeingVisible, úsalo. Si no, deja el Sleep.
            // WaitForBeingVisible(By.Id("NameSurname")); 
            Thread.Sleep(1000);

            bool result = true;

            // 2. Obtenemos los valores reales de la web
            string actualName = _driver.FindElement(By.Id("NameSurname")).Text;
            string actualAddress = _driver.FindElement(By.Id("Address")).Text;
            string actualPrice = _driver.FindElement(By.Id("TotalPrice")).Text;
            string dateText = _driver.FindElement(By.Id("PurchaseDate")).Text;

            // 3. Comprobamos NOMBRE e imprimimos si falla
            if (!actualName.Contains(expectedName))
            {
                _output.WriteLine($"[FALLO] Nombre incorrecto. Esperaba contener: '{expectedName}', Encontrado: '{actualName}'");
                result = false;
            }

            // 4. Comprobamos DIRECCIÓN
            if (!actualAddress.Contains(expectedAddress))
            {
                _output.WriteLine($"[FALLO] Dirección incorrecta. Esperaba contener: '{expectedAddress}', Encontrado: '{actualAddress}'");
                result = false;
            }

            // 5. Comprobamos PRECIO (Aquí es donde probablemente fallará tu test actual)
            if (!actualPrice.Contains(expectedPrice))
            {
                _output.WriteLine($"[FALLO] Precio incorrecto. Esperaba contener: '{expectedPrice}', Encontrado: '{actualPrice}'");
                result = false;
            }

            // 6. Comprobamos FECHA
            var actualPurchaseDate = DateTime.Parse(dateText);
            if ((actualPurchaseDate - purchaseDate) >= new TimeSpan(0, 5, 0)) // Damos 5 min de margen
            {
                _output.WriteLine($"[FALLO] Fecha incorrecta. Esperaba: {purchaseDate}, Encontrada: {actualPurchaseDate}");
                result = false;
            }

            return result;
        }

        public bool CheckListOfCars(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("PurchasedCars"));
        }
    }
}