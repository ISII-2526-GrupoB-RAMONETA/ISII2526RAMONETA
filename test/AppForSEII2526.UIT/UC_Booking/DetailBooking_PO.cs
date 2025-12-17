using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Booking
{
    public class DetailBooking_PO : PageObject
    {

        public DetailBooking_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckBookingDetail(string nameSurname, string address, string paymentMethod, DateTime date, string phoneNumber, string totalPrice, string totalDays)
        {
            Thread.Sleep(1000);
            WaitForBeingVisible(By.Id("DetailsPrice"));
            WaitForBeingVisible(By.Id("TotalDays"));
            bool result = true;

            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nameSurname);
            result = result && _driver.FindElement(By.Id("Address")).Text.Contains(address);
            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(paymentMethod);
            result = result && _driver.FindElement(By.Id("PhoneNumber")).Text.Contains(phoneNumber);
            result = result && _driver.FindElement(By.Id("DetailsPrice")).Text.Contains(totalPrice);
            result = result && _driver.FindElement(By.Id("TotalDays")).Text.Contains(totalDays);

            var actualBookingDate = DateTime.Parse(_driver.FindElement(By.Id("BookingDate")).Text);
            result = result && ((actualBookingDate - date) < new TimeSpan(0, 1, 0));

            return result;
        }


        public bool CheckListOfMaintenances(List<string[]> expectedBookingItems)
        {
            return CheckBodyTable(expectedBookingItems, By.Id("BookedMaintenances"));

        }


    }
}
