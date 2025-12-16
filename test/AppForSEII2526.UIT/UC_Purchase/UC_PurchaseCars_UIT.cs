using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Purchase
{
    public class UC_PurchaseCars_UIT : UC_UIT 
    {
        private SelectCarsForPurchase_PO selectCarsForPurchase_PO;
        public UC_PurchaseCars_UIT(ITestOutputHelper output) : base(output)
        {
        }
        private void Precondition_perform_login()
        {
            Perform_login("tomas.gonzalez@uclm.es", "OtherPass12$");
        }

        private void InitialStepsForPurchaseCars()
        {
            Precondition_perform_login();
            //we wait for the option of the menu to be visible
            selectCarsForPurchase_PO.WaitForBeingVisible(By.Id("CreatePurchase"));
            //we click on the menu
            _driver.FindElement(By.Id("CreatePurchase")).Click();
        }
    }
}

