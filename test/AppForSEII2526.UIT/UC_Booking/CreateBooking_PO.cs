using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Booking
{
    public class CreateBooking_PO : PageObject
    {
        private By _nameBy = By.Id("Name");
        private IWebElement _name() => _driver.FindElement(By.Id("Name"));
        private IWebElement _surname() => _driver.FindElement(By.Id("Surname"));
        private IWebElement _address() => _driver.FindElement(By.Id("AddressCreate"));
        private IWebElement _paymentMethod() => _driver.FindElement(By.Id("PaymentMethodCreate"));
        private IWebElement _phoneNumber() => _driver.FindElement(By.Id("PhoneNumberCreate"));

        public CreateBooking_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void FillBookingInfo(string name, string surname, string address, string paymentMethod, string? phoneNumber)
        {
            WaitForBeingVisible(_nameBy);
            _name().SendKeys(name);
            ;
            _surname().SendKeys(surname);
            Thread.Sleep(1000);
            _address().SendKeys(address);
            Thread.Sleep(1000);
            _phoneNumber().SendKeys(phoneNumber);
            

            //Create select element object
            SelectElement selectElement = new SelectElement(_paymentMethod());

            //Select Action from dropdown menu
            selectElement.SelectByText(paymentMethod);
        }

        public void FillInBookingComent(string bookingComment, int maintenanceId)
        {
            Thread.Sleep(1000);
            _driver.FindElement(By.Id("comment_" + maintenanceId)).SendKeys(bookingComment);
        }

        public void PressBookYourMaintenances()
        {
            _driver.FindElement(By.Id("Submit")).Click();
        }

        public void PressModifyMaintenances()
        {
            _driver.FindElement(By.Id("ModifyMaintenances")).Click();

        }

        public bool CheckListOfBookingItems(List<string[]> expectedBookingItems)
        {
            return CheckBodyTable(expectedBookingItems, By.Id("TableOfBookingItems"));
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public bool CheckTotalPrice(string expectedPrice)
        {
            WaitForBeingVisible(By.Id("TotalPriceCreate"));
            string actualPrice = _driver.FindElement(By.Id("TotalPriceCreate")).Text;
            return int.Parse(expectedPrice) == int.Parse(actualPrice);

        }

    }
}
