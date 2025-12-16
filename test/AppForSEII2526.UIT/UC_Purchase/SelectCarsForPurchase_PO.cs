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

        public bool CheckListOfCars(List<string[]> expectedCars)
        {
            return CheckBodyTable(expectedCars, tableOfCarsBy);
        }
    }
}
