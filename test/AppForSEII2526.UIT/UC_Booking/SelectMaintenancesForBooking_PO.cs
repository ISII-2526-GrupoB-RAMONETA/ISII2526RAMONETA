using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Booking
{
    public class SelectMaintenancesForBooking_PO : PageObject { 
    
        By inputName = By.Id("inputName");
        By selectType = By.Id("selectType");
        By buttonSeachMaintenances = By.Id("searchMaintenances");
        By tableOfMaintenancesBy = By.Id("TableOfMaintenances");
        By buttonBookMaintenances = By.Id("bookMaintenanceButton");
        By totalPrice = By.Id("totalPrice");

        public SelectMaintenancesForBooking_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }


        public void SearchMaintenances(String name, string type)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputName);
            _driver.FindElement(inputName).SendKeys(name);
            if (type == "") type = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(selectType));
            selectElement.SelectByText(type);

            _driver.FindElement(buttonSeachMaintenances).Click();
        }
        
        public bool CheckListOfMaintenances(List<string[]> expectedMaintenances)
        {
            return CheckBodyTable(expectedMaintenances, tableOfMaintenancesBy);

        }

        public void AddMaintenanceToBookingCart(string maintenanceName)
        {
            WaitForBeingClickable(By.Id("maintenanceToBook_"+ maintenanceName));

            _driver.FindElement(By.Id("maintenanceToBook_" + maintenanceName)).Click();

        }

        public void RemoveMaintenanceFromBookingCart(string maintenanceName)
        {
            WaitForBeingClickable(By.Id("removeMaintenance_" + maintenanceName));

            _driver.FindElement(By.Id("removeMaintenance_" + maintenanceName)).Click();

        }

        public bool BookingNotAvailable()
        {
            //the button is not Displayed=hidden
            return _driver.FindElement(buttonBookMaintenances).Displayed == false;

        }

        public void BookMaintenances()
        {
            WaitForBeingClickable(buttonBookMaintenances);
            _driver.FindElement(buttonBookMaintenances).Click();
        }

        public bool CheckMessageErrorNotAvaibleMaintenances(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);

        }

        public bool ModifyBookingCart(string price)
        {
            WaitForBeingVisible(totalPrice);

            int currentPrice = int.Parse(_driver.FindElement(totalPrice).Text);

            int expectedPrice = int.Parse(price);

            return currentPrice == expectedPrice;

        }
    }
}
