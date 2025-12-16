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
        private const int carId1 = 2;
        private const string carModel1 = "Coupe";
        private const string carColor1 = "Black";
        private const string carFuelType1 = "Hybrid";
        private const string carManufacturer1 = "Mercedes-Benz";
        private const string carPrice1 = "4800000";

        private const int carId2 = 3;
        private const string carModel2 = "Sedan";
        private const string carColor2 = "Red";
        private const string carFuelType2 = "Gasoline";
        private const string carManufacturer2 = "Audi";
        private const string carPrice2 = "6200000";

        public UC_PurchaseCars_UIT(ITestOutputHelper output) : base(output)
        {
            selectCarsForPurchase_PO = new SelectCarsForPurchase_PO(_driver, _output);
        }
        private void Precondition_perform_login()
        {
            Perform_login("tomas.gonzalez@uclm.es", "OtherPass12$");
        }

        private void InitialStepsForPurchaseCars()
        {
            Precondition_perform_login();
            Thread.Sleep(1000);
            //we wait for the option of the menu to be visible
            selectCarsForPurchase_PO.WaitForBeingVisible(By.Id("CreatePurchase"));
            //we click on the menu
            _driver.FindElement(By.Id("CreatePurchase")).Click();
        }

        [Theory]
        [InlineData(carModel1, carColor1, carFuelType1, carManufacturer1,carPrice1, "Black", "")]
        [InlineData(carModel2, carColor2, carFuelType2, carManufacturer2,carPrice2, "", "Sedan")]
        [Trait("LevelTesting","Funcional Testing")]
        public void UC1_FA1_UC1_2_3_filtering(string carModel,string carColor,string carFuelType,string carManufacturer,string carPrice,string filterColor,string filterModel)
        {
            //Arrange
            InitialStepsForPurchaseCars();
            var expectedCars = new List<string[]> { new string[] {carModel,carColor,carFuelType,carManufacturer,carPrice },};

            //Act
            selectCarsForPurchase_PO.SearchCars(filterColor,filterModel);

            //Assert
            Assert.True(selectCarsForPurchase_PO.CheckListOfCars(expectedCars));
        }
    }
}

