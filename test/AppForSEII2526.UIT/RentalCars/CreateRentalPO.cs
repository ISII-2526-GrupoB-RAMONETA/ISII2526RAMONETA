using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.RentalCars
{
    public class CreateRental_PO : PageObject
    {

        private By _nameBy = By.Id("Name");
        private By _surnameBy = By.Id("Surname");
        private IWebElement _name() => _driver.FindElement(_nameBy);
        private IWebElement _surname() => _driver.FindElement(_surnameBy);
        private IWebElement _deliveryAddress() => _driver.FindElement(By.Id("DeliveryAddress"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethodRental"));




        public CreateRental_PO(IWebDriver driver, ITestOutputHelper output)
            : base(driver, output)
        {
        }

        public void FillInRentalInfo(string name,string surname, string deliveryAddress, string paymentMethod)
        {
            WaitForBeingVisible(_nameBy);
            _name().SendKeys(name);

            WaitForBeingVisible(_surnameBy);
            _surname().SendKeys(surname);
            _deliveryAddress().SendKeys(deliveryAddress);

            //create select element object 
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //select Action from the dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInRentalDescription(string quantity, int movieId)
        {
            _driver.FindElement(By.Id("quantity_" + movieId)).SendKeys(quantity);
        }


        public void PressRentYourCars()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }



        public void PressModifyCars()
        {
            _driver.FindElement(By.Id("ModifyCars")).Click();
        }

        public bool CheckListOfRentalItems(List<string[]> expectedRentalItems)
        {
            return CheckBodyTable(expectedRentalItems, By.Id("TableOfRentalItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

    }
}
