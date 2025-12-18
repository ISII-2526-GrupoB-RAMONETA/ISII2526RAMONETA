using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.RentalCars
{
    public class CURentalCars_UIT : UC_UIT
    {
        public CURentalCars_UIT(ITestOutputHelper output) : base(output)
        {
            Initial_step_opening_the_web_page();
            listcars = new ListCarsForRentalPO(_driver, _output);
        }

        private const string carId1="4";
        private const int carId1number = 4;
        private const string carModel1 = "Pickup Truck";
        private const string fuelType1 = "Gasoline";
        private const string manufacturer1 = "Volkswagen";
        private const string color1 = "White";
        private const string priceForRenting1 = "80";
        private const string quantity1 = "1";

        private const string carId2 = "5";
        private const int carId2number = 5;
        private const string carModel2 = "Sedan";
        private const string fuelType2 = "Gasoline";
        private const string manufacturer2 = "Audi";
        private const string color2 = "Red";
        private const string priceForRenting2 = "280";

        private ListCarsForRentalPO listcars;

        private void Precondition_perform_login()
        {
            Perform_login("pablo.ballestero@uclm.es", "OtherPass12$");
        }

        private void InitialStepsForRentalCars_UIT()
        {
            Precondition_perform_login();
            //Thread.Sleep(1000);
            listcars.WaitForBeingVisibleIgnoringExeptionTypes(By.Id("CreateRenting"));
            _driver.FindElement(By.Id("CreateRenting")).Click();
        }

        [Theory]
        [InlineData(carModel1, fuelType1, manufacturer1, color1,priceForRenting1, "100", "")]
        [InlineData(carModel2, fuelType2, manufacturer2, color2,priceForRenting2, "", carModel2)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_filteringbyPriceandModel(string model, string fueltype,
            string manufacturer, string color, string price, string filterprice, string filtermodel)
        {
            //Arrange

            var rentaldate = DateTime.Today.AddDays(1);
            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            var expectedCars = new List<string[]> { new string[] { model, fueltype, manufacturer, color, price }, };
            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars(filterprice, filtermodel, from, to, rentaldate);

            //Assert            
            Assert.True(listcars.CheckListOfCars(expectedCars));

        }

        //public static IEnumerable<object[]> TestCasesFor_UC2_4_5_AF2_errorindates()
        //{
        //    var allTests = new List<object[]> {
        //        new object[] { DateTime.Today.AddDays(-1), DateTime.Today.AddDays(2), DateTime.Today.AddDays(2), "Your rental period must be later",  },
        //        //cannot be checked if datetime is before today, because the next condition is checked before
        //        new object[] { DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(2), "Your rental period must be later", },
        //        new object[] { DateTime.Today.AddDays(7), DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), "Your rental must end after than its starts", },
        //    };

        //    return allTests;
        //}

        //[Theory]
        //[MemberData(nameof(TestCasesFor_UC2_4_5_AF2_errorindates))]
        //[Trait("LevelTesting", "Funcional Testing")]
        //public void UC2_7_8_9_AF2_errorindates(DateTime from, DateTime to, DateTime rentaldate, string error)
        //{
        //    //Arrange
        //    //var rentaldate = DateTime.Today.AddDays(1);

        //    //Act
        //    InitialStepsForRentalCars_UIT();

        //    listcars.FilterCars("", "", from, to, rentaldate);

        //    //Assert

        //    //this message will be shown if assert fails
        //    Assert.True(listcars.CheckMessageError(error), $"Error in the message box for test {from} - {to}");

        //}

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_4_CarsNotAvailable()
        {
            //Arrange
            InitialStepsForRentalCars_UIT();
            var expectedMessage = "There are no cars available for being rented.";

            //Act
            var rentaldate = DateTime.Today.AddDays(1);
            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            listcars.FilterCars("1", carModel2, from, to, rentaldate);

            //Assert
            Assert.True(listcars.CheckMessageErrorNotAvailableCars(expectedMessage));
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_8_ModifySelectedCars()
        {
            //Arrange
            var rentaldate = DateTime.Today.AddDays(1);
            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", "", from, to,rentaldate);
            Thread.Sleep(1000);
            listcars.SelectCars(new List<string> { carId1, carId2 });
            Thread.Sleep(1000);
            listcars.ModifyRentingCart(carId2);


            //Assert            
            Assert.True(listcars.CheckShoppingCart(priceForRenting1));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_7_RentButtonNotAvailable()
        {
            //Arrange
            var rentaldate = DateTime.Today.AddDays(1);
            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", "", from, to, rentaldate);
            listcars.SelectCars(new List<string> { carId1});
            listcars.ModifyRentingCart(carId1);


            //Assert            
            Assert.True(listcars.CheckRentCarsDisabled(), "Rent button should be disabled");
        }

        [Theory]
        [InlineData("", "Ballestero", "Calle Cervantes,8", "Visa", "1", "The CustomerName field is required.")]
        [InlineData("Pablo", "", "Calle Cervantes,8", "Visa", "1", "The CustomerSurname field is required.")]
        [InlineData("Pablo", "Ballestero", "", "Visa", "1", "The DeliveryAddress field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_9_10_11_testingErrorsMandatorydata(string name, string surname, string deliveryAddress, string paymentmethod, string quantity,
                    string expectedMessageError)
        {
            //Arrange

            var createrental = new CreateRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            var rentaldate = DateTime.Today.AddDays(1);
            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", "", from, to,rentaldate);
            listcars.SelectCars(new List<string> { carId1 });
            listcars.RentCars();
            createrental.FillInRentalInfo(name, surname, deliveryAddress, "Visa");
            createrental.PressRentYourCars();

            //Assert
            //the expected error is shown in the view
            Assert.True(createrental.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_12_ModifyRentalItems()
        {
            //Arrange

            var createrental = new CreateRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            var rentaldate = DateTime.Today.AddDays(1);
            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", "", from, to, rentaldate);
            listcars.SelectCars(new List<string> { carId1, carId2 });
            listcars.RentCars();
            Thread.Sleep(1000);
            createrental.PressModifyCars();
            //we remove cartitle2 from the rentingcart
            listcars.ModifyRentingCart(carId2);
            listcars.RentCars();

            //Assert
            //the list of cars must change
            var expectedRentalItems = new List<string[]> { new string[] { carModel1, manufacturer1, priceForRenting1 }, };
            Assert.True(createrental.CheckListOfRentalItems(expectedRentalItems));
        }

        [Theory]
        [InlineData("Pablo", "Ballestero", "Calle Cervantes,8", "Visa", "1")]
        [InlineData("Pablo", "Ballestero", "Calle Cervantes,8", "GooglePay", "1")]
        [InlineData("Pablo", "Ballestero", "Calle Cervantes,8", "PayPal", "1")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_1_2_3(string name, string surname, string deliveryAddress, string paymentMethod, string quantity)
        {
            //Arrange

            var createrental = new CreateRental_PO(_driver, _output);
            var detailRental = new DetailRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(2);
            var to = DateTime.Today.AddDays(3);
            var rentaldate = DateTime.Today.AddDays(1);



            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", "", from, to, rentaldate);
            listcars.SelectCars(new List<string> { carId1 });
            listcars.RentCars();

            createrental.FillInRentalInfo(name, surname, deliveryAddress, paymentMethod);
            createrental.FillInRentalDescription(quantity1, carId1number);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            var totalprice = int.Parse(quantity) * int.Parse(priceForRenting1);

            //Assert
            //the expected error is shown in the view
            Assert.True(detailRental.CheckRentalDetail(name, surname,
                deliveryAddress, paymentMethod, from, to,rentaldate, totalprice.ToString() + " €"),
                "Error: detail rental is not as expected");

            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, manufacturer1, priceForRenting1 + " €", quantity1}, };

            Assert.True(detailRental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected");

        }



        [Theory]
        [InlineData("Pablo", "Ballestero", "Calle Cervantes,8", "Visa", "1")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2__alquilar_examen(string name, string surname, string deliveryAddress, string paymentMethod, string quantity)
        {
            //Arrange

            var createrental = new CreateRental_PO(_driver, _output);
            var detailRental = new DetailRental_PO(_driver, _output);

            var from = DateTime.Today.AddDays(1);
            var to = DateTime.Today.AddDays(2);
            var rentaldate = DateTime.Today.AddDays(0);



            //Act
            InitialStepsForRentalCars_UIT();

            listcars.FilterCars("", carModel2, from, to, rentaldate);
            listcars.SelectCars(new List<string> { carId2 });

            listcars.FilterCars("100", "");
            listcars.SelectCars(new List<string> { carId1 });

            listcars.RentCars();

            Thread.Sleep(100);

            createrental.PressModifyCars();
            Thread.Sleep(100);
            //we remove cartitle2 from the rentingcart
            listcars.ModifyRentingCart(carId2);
            Thread.Sleep(100);
            listcars.RentCars();

            createrental.FillInRentalInfo(name, surname, deliveryAddress, paymentMethod);
            createrental.FillInRentalDescription(quantity, carId1number);
            createrental.PressRentYourCars();
            createrental.PressOkModalDialog();

            var totalprice = int.Parse(quantity) * int.Parse(priceForRenting1);

            //Assert
            //the expected error is shown in the view
            Assert.True(detailRental.CheckRentalDetail(name, surname,
                deliveryAddress, paymentMethod, from, to, rentaldate, totalprice.ToString() + " €"),
                "Error: detail rental is not as expected");

            var expectedRentalItems = new List<string[]>
                    { new string[] { carModel1, manufacturer1, priceForRenting1 + " €", quantity}, };

            Assert.True(detailRental.CheckListOfCars(expectedRentalItems),
                "Error: rental items are not as expected");

        }

    }

}