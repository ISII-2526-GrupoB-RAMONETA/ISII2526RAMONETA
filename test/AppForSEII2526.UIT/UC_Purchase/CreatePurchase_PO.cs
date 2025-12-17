using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class CreatePurchase_PO:PageObject
    {
        private By _nameBy = By.Id("Name");
        private By _SurnameBy = By.Id("Surname");
        private By _totalPriceBy = By.Id("totalPrice");
        private IWebElement _name() => _driver.FindElement(_nameBy);
        private IWebElement _surname() => _driver.FindElement(_SurnameBy);
        private IWebElement _address() => _driver.FindElement(By.Id("Address"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethod"));




        public CreatePurchase_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInPurchaseInfo(string name,string surname, string address, string paymentMethod)
        {
            WaitForBeingVisible(_nameBy);
            _name().SendKeys(name);
            _surname().SendKeys(surname);
            _address().SendKeys(address);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInPurchaseQuantity(string quantity, int carId)
        {
            By quantityInput = By.Id($"quantity_{carId}");

            WaitForBeingVisible(quantityInput);
            var input = _driver.FindElement(quantityInput);
            string currentValue = input.GetAttribute("value");

            if(currentValue == quantity)
            {
                return;
            }
            input.SendKeys(Keys.Control +"a");
            input.SendKeys(Keys.Backspace);
            input.SendKeys(quantity);
        }


        public void PressPurchaseYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public decimal GetTotalPrice()
        {
            WaitForBeingVisible(_totalPriceBy);
            string totalPriceText = _driver.FindElement(_totalPriceBy).Text;
            //we remove the $ sign and any spaces
            return decimal.Parse(totalPriceText);
        }



        public void PressModifyCars()
        {
            Thread.Sleep(1000); //wait for a while to ensure the button is clickable
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfPurchaseItems(List<string[]> expectedPurchaseItems)
        {
            return CheckBodyTable(expectedPurchaseItems, By.Id("TableOfPurchaseItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }

}

