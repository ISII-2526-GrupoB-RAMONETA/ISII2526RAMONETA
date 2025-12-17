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
        private const string carDescription1 = "Luxury business sedan";
        private const string carQuantity1 = "1";

        private const int carId2 = 5;
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
        public void UC1_3_4_FA1_filtering(string carModel,string carColor,string carFuelType,string carManufacturer,string carPrice,string filterColor,string filterModel)
        {
            //Arrange
            InitialStepsForPurchaseCars();
            var expectedCars = new List<string[]> { new string[] {carModel,carColor,carFuelType,carManufacturer,carPrice },};

            //Act
            selectCarsForPurchase_PO.SearchCars(filterColor,filterModel);

            //Assert
            Assert.True(selectCarsForPurchase_PO.CheckListOfCars(expectedCars));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_5_FA2_PurchasingNotAvailable()
        {
            //Arrange

            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1.ToString());
            selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(carId1.ToString());
            //Assert
            Assert.True(selectCarsForPurchase_PO.PurchasingNotAvailable());

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_6_FA3_ModifyPurchasingCart()
        {
            //Arrange
            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.SearchCars("", "");
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1.ToString());
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId2.ToString());
            selectCarsForPurchase_PO.ModifyPurchasingCart(carId2.ToString());

            //Assert
            Assert.True(selectCarsForPurchase_PO.CheckCarNotInPurchasingCart(carId2.ToString()));

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_11_FA5_ModifySelectedCars()
        {
            //Arrange
            var createPurchase_PO = new CreatePurchase_PO(_driver, _output);
            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.SearchCars("", "");
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1.ToString());
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId2.ToString());
            selectCarsForPurchase_PO.PurchaseCars();
            createPurchase_PO.PressModifyCars();
            selectCarsForPurchase_PO.RemoveCarFromPurchasingCart(carId2.ToString());
            selectCarsForPurchase_PO.PurchaseCars();
            //Assert
            var expectedPurchaseItems = new List<string[]> { new string[] { carModel1, carColor1, carDescription1, carPrice1 } };
            Assert.True(createPurchase_PO.CheckListOfPurchaseItems(expectedPurchaseItems)); //mirarlo bien

        }

        [Theory]
        [InlineData("" , "González" , "Blasco Ibáñez, 4" , "The Name field is required")]
        [InlineData("Tomás", "", "Blasco Ibáñez, 4", "The Surname field is required")]
        [InlineData("Tomás", "González", "", "The Address field is required")]
        [Trait("LevelTesting","Funcional Testing")]
        public void UC1_7_8_9_FA4_testingErrorsMandatorydata(string name,string surname,string address,string expectedMessageError)
        {
            //Arrange
            var createpurchase = new CreatePurchase_PO(_driver, _output);
            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.SearchCars("", "");
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1.ToString());
            selectCarsForPurchase_PO.PurchaseCars();
            createpurchase.FillInPurchaseInfo(name, surname, address, "Visa");
            createpurchase.PressPurchaseYourCars();
            //Assert
            Assert.True(createpurchase.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }

        [Fact(Skip = "First change the quantifyofpurchasing of the cars to 0 using script dbo.Cars.QuantityForPurchasing0")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_12_AF0_CarsNotAvailableForPurchase()
        {
            //Arrange

            var expectedMessage = "There are no cars available for being purchased";
            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.SearchCars("", "");

            //Assert
            //this message will be shown if assert fails
            Assert.True(selectCarsForPurchase_PO.CheckMessageError(expectedMessage), $"Car Model {carModel2} with color {carColor2} does not exist");

        }

        [Theory]
        [InlineData("Tomás", "González", "Blasco Ibáñez,4", "Visa")]
        [InlineData("Tomás", "González", "Blasco Ibáñez,4", "Google Pay")]
        [Trait("LevelTesting","FuncionalTesting")]
        public void UC1_1_2_BasicFlow(string name,string surname,string address,string paymentMethod)
        {
            //Arrange
            var createpurchase =new CreatePurchase_PO(_driver,_output);
            var detailpurchase =new DetailPurchase_PO(_driver,_output);
            //Act
            InitialStepsForPurchaseCars();
            selectCarsForPurchase_PO.SearchCars("", "");
            selectCarsForPurchase_PO.AddCarToPurchasingCart(carId1.ToString());
            selectCarsForPurchase_PO.PurchaseCars();

            createpurchase.FillInPurchaseInfo(name, surname, address, paymentMethod);
            createpurchase.FillInPurchaseQuantity(carQuantity1, carId1);
            createpurchase.PressPurchaseYourCars();
            createpurchase.PressOkModalDialog();

            //Assert
            Assert.True(detailpurchase.CheckPurchaseDetail(name +" "+ surname, address, DateTime.Now, carPrice1 + " €"), "Error:Detail purchase is not as expected");
            var expectedPurchaseItems = new List<string[]> { new string[] { carModel1, carPrice1 , carColor1, carQuantity1 }, };
            Assert.True(detailpurchase.CheckListOfCars(expectedPurchaseItems),"Error: purchase items are not as expected ");

        }
    }
}

