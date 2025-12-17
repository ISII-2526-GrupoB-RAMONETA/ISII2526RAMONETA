using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class SelectCarsForPurchase_PO : PageObject
    {
        By inputColor= By.Id("inputColor");
        By inputModel= By.Id("selectModel");
        By btnSearch= By.Id("searchCars");
        By tableOfCarsBy= By.Id("TableOfCars");
        By errorShownBy= By.Id("errorShown");
        By buttonPurchaseCars= By.Id("purchaseCarButton");

        private IWebElement _carColor() => _driver.FindElement(inputColor);
        private IWebElement _carModel() => _driver.FindElement(inputModel);
        private IWebElement _btnSearch() => _driver.FindElement(btnSearch);
        private IWebElement _tableOfCars() => _driver.FindElement(tableOfCarsBy);
        private IWebElement _buttonPurchaseCars() => _driver.FindElement(buttonPurchaseCars);


        public SelectCarsForPurchase_PO(IWebDriver driver,ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string color,string model)
        {
            WaitForBeingClickable(inputColor);
            _driver.FindElement(inputColor).SendKeys(color);
            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputModel));
            selectElement.SelectByText(model);
            _driver.FindElement(btnSearch).Click();
            Thread.Sleep(1000); //wait for the table to be updated
        }

        public void SelectCars(List<string> carModels)
        {
            //we wait for till the movies are available to be selected 
            foreach (var carModel in carModels)
            {
                WaitForBeingVisible(By.Id($"carToPurchase_{carModel}"));
                _driver.FindElement(By.Id($"carToPurchase_{carModel}")).Click();
            }
        }
        public void PurchaseCars()
        {
            WaitForBeingClickable(buttonPurchaseCars);
            _driver.FindElement(buttonPurchaseCars).Click();
        }

        public void ModifyPurchasingCart(string id)
        {
            RemoveCarFromPurchasingCart(id);
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }

        public bool CheckMessageError(string errorMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown:{actualErrorShown.Text}");
            return actualErrorShown.Text.Contains(errorMessage);
        }

        public void AddCarToPurchasingCart(string id)
        {
            WaitForBeingClickable(By.Id("carToPurchase_" + id));
            _driver.FindElement(By.Id("carToPurchase_" + id)).Click();
        }
        public void RemoveCarFromPurchasingCart(string id)
        {
            WaitForBeingClickable(By.Id("removeCar_" + id));
            _driver.FindElement(By.Id("removeCar_" + id)).Click();
            Thread.Sleep(1000); //wait for the table to be updated
        }
        public bool PurchasingNotAvailable()
        {
            return _driver.FindElement(buttonPurchaseCars).Displayed == false;
        }
        public bool CheckCarNotInPurchasingCart(string id)
        {
            // Usamos FindElements (plural) con el ID del botón de eliminar.
            // Si la lista está vacía (Count == 0), significa que el botón ya no existe en la página.
            var elements = _driver.FindElements(By.Id($"removeCar_{id}"));

            return elements.Count == 0;
        }
    }
}
