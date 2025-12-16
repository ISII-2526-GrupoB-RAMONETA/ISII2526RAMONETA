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
        By inputColor= By.Id("carColor");
        By inputModel= By.Id("carModel");
        By btnSearch= By.Id("searchCarsBtn");
        By tableOfCarsBy= By.Id("carsTable");
        public SelectCarsForPurchase_PO(IWebDriver driver,ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string color,string model)
        {
            WaitForBeingClickable(inputModel);
            _driver.FindElement(inputModel).SendKeys(model);
            if (model == "") model = "All";
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputColor));
            selectElement.SelectByText(color);
            _driver.FindElement(btnSearch).Click();
        }

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }
    }
}
