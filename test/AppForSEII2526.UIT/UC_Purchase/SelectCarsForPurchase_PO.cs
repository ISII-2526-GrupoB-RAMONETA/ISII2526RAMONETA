using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class SelectCarsForPurchase_PO : PageObject
    {
        By inputColor= By.Id("carColor");
        By inputModel= By.Id("carModel");
        By btnSearch= By.Id("searchCarsBtn");
        public SelectCarsForPurchase_PO(IWebDriver driver,ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchCars(string model)
        {
            WaitForBeingClickable(inputModel);
            _driver.FindElement(inputModel).SendKeys(model);
            _driver.FindElement(btnSearch).Click();
        }
    }
}
