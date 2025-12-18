using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppForSEII2526.UIT.UC_Booking
{
    public class UC_BookMaintenances_UIT : UC_UIT
    {

        private SelectMaintenancesForBooking_PO selectMaintenancesForBooking_PO;

        private const int maintenanceId1 = 1;
        private const string maintenanceName1 = "Air filter cleaning";
        private const string maintenanceType1 = "Preventive";
        private const string maintenanceNumberOfDays1 = "3";
        private const string maintenancePrice1 = "80";

        private const int maintenanceId2 = 5;
        private const string maintenanceName2 = "Scale calibration";
        private const string maintenanceType2 = "Calibration";
        private const string maintenanceNumberOfDays2 = "4";
        private const string maintenancePrice2 = "180";

        private const string customerName = "Pablo";
        private const string customerSurname = "Ramón";
        private const string customerAddress = "Val General, 12";
        private const string customerPhoneNumber = "+34 622";
        private const string paymentMethod1 = "Efectivo";
        private const string paymentMethod2 = "TarjetaCredito";
        private const string paymentMethod3 = "PayPal";
        private const string comment1 = "Air filter cleaned, dust and debris removed";
        private const string comment2 = "Please fix it";

        private const string filter1 = "filter";

        public UC_BookMaintenances_UIT(ITestOutputHelper output) : base(output)
        {
            selectMaintenancesForBooking_PO = new SelectMaintenancesForBooking_PO(_driver, _output);

        }

        private void Precondition_perform_login()
        {
            Perform_login("pablo.ramon@uclm.es", "APassword1234%");
        }


        private void InitialStepsForBookingMaintenances()
        {
            Precondition_perform_login();
            Thread.Sleep(1000);

            //we wait for the option of the menu to be visible
            selectMaintenancesForBooking_PO.WaitForBeingVisible(By.Id("CreateBooking"));
            //we click on the menu

            _driver.FindElement(By.Id("CreateBooking")).Click();

        }

        [Theory]
        [InlineData(customerName, customerSurname, customerAddress, paymentMethod1, customerPhoneNumber, comment1)]
        [InlineData(customerName, customerSurname, customerAddress, paymentMethod2, customerPhoneNumber, comment1)]
        [InlineData(customerName, customerSurname, customerAddress, paymentMethod3, customerPhoneNumber, comment1)]
        public void UC3_1_2_3_BasicFlow(string name, string surname, string address, string paymentMethod, string phoneNumber, string comment)
        {
            //Arrange
            var createBooking = new CreateBooking_PO(_driver, _output);
            var detailBooking = new DetailBooking_PO(_driver, _output);
            InitialStepsForBookingMaintenances();

            //Act
            selectMaintenancesForBooking_PO.SearchMaintenances("", "");
            selectMaintenancesForBooking_PO.SelectMaintenances(new List<string> { maintenanceName1 });
            selectMaintenancesForBooking_PO.BookMaintenances();

            createBooking.FillBookingInfo(name, surname, address, paymentMethod, phoneNumber);
            createBooking.FillInBookingComent(comment, maintenanceId1);
            createBooking.PressBookYourMaintenances();
            createBooking.PressOkModalDialog();

            //Assert
            // the expected error is shown in the view
            Assert.True(detailBooking.CheckBookingDetail(name + " " + surname, address, paymentMethod, DateTime.Now, phoneNumber,
                maintenancePrice1, maintenanceNumberOfDays1), "Error: detail booking is not as expected");

            var expectedBookingItems = new List<string[]>
            {
                new string[] { maintenanceName1, comment, maintenancePrice1 + " €",maintenanceNumberOfDays1 },
            };

            Assert.True(detailBooking.CheckListOfMaintenances(expectedBookingItems), "Error: booking items are not as expected");
        }




        [Theory]
        [InlineData(maintenanceName1, maintenanceType1, maintenanceNumberOfDays1, maintenancePrice1, "filter", "")]
        [InlineData(maintenanceName2, maintenanceType2, maintenanceNumberOfDays2, maintenancePrice2, "", "Calibration")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF0_UC3_4_5_filtering(string maintenanceName, string maintenanceType, string maintenanceNumberOfDays, string maintenancePrice, string filterName, string filterType)
        {
            //Arrange
            InitialStepsForBookingMaintenances();
            var expectedMaintenances = new List<string[]> { new string[] { maintenanceName, maintenanceType, maintenanceNumberOfDays, maintenancePrice }, };

            //Act
            selectMaintenancesForBooking_PO.SearchMaintenances(filterName, filterType);

            //Assert
            Assert.True(selectMaintenancesForBooking_PO.CheckListOfMaintenances(expectedMaintenances));


        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF1_UC3_6_BookingNotAvailable()
        {
            //Arrange
            InitialStepsForBookingMaintenances();

            //Act
            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName1);
            selectMaintenancesForBooking_PO.RemoveMaintenanceFromBookingCart(maintenanceName1);

            //Assert
            Assert.True(selectMaintenancesForBooking_PO.BookingNotAvailable());

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF2_UC3_7_ModifyBookingCart()
        {

            //Arrange
            InitialStepsForBookingMaintenances();

            //Act
            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName1);
            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName2);
            Thread.Sleep(1000);
            selectMaintenancesForBooking_PO.RemoveMaintenanceFromBookingCart(maintenanceName2);

            //Assert
            Assert.True(selectMaintenancesForBooking_PO.ModifyBookingCart(maintenancePrice1));
        }

        [Theory]
        [InlineData("", customerSurname, customerAddress, paymentMethod1, customerPhoneNumber, comment1, "The field CustomerName must be a string with a minimum length of 2 and a maximum length of 20.")]
        [InlineData(customerName, "", customerAddress, paymentMethod1, customerPhoneNumber, comment1, "The field CustomerSurname must be a string with a minimum length of 2 and a maximum length of 30.")]
        [InlineData(customerName, customerSurname, "", paymentMethod1, customerPhoneNumber, comment1, "The field Address must be a string with a minimum length of 5 and a maximum length of 50.")]
        public void UC3_AF3_UC3_8_9_10_testingErrorsMandatorydata(string name, string surname,
            string address, string paymentMethod, string phoneNumber, string comment, string expectedMessageError)
        {
            //Arrange
            var createBooking = new CreateBooking_PO(_driver, _output);
            InitialStepsForBookingMaintenances();

            //Act
            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName1);
            selectMaintenancesForBooking_PO.BookMaintenances();

            Thread.Sleep(1000);
            createBooking.FillBookingInfo(name, surname, address, paymentMethod, phoneNumber);
            createBooking.FillInBookingComent(comment, maintenanceId1);
            Thread.Sleep(1000);
            createBooking.PressBookYourMaintenances();


            //Assert
            Assert.True(createBooking.CheckValidationError(expectedMessageError), $"Expected error: {expectedMessageError}");

        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_AF4_UC3_12_ModifyBookingItems()
        {
            //Arrange
            var createBooking = new CreateBooking_PO(_driver, _output);
            InitialStepsForBookingMaintenances();

            var expectedBookingItemsInitial = new List<string[]>
            {
                new string[] { maintenanceName1, maintenancePrice1, maintenanceNumberOfDays1 }
            };
            //Act

            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName1);
            selectMaintenancesForBooking_PO.AddMaintenanceToBookingCart(maintenanceName2);
            selectMaintenancesForBooking_PO.BookMaintenances();

            Thread.Sleep(1000);
            createBooking.FillBookingInfo(customerName, customerSurname, customerAddress, paymentMethod1, customerPhoneNumber);
            createBooking.FillInBookingComent(comment1, maintenanceId1);
            createBooking.PressModifyMaintenances();

            selectMaintenancesForBooking_PO.RemoveMaintenanceFromBookingCart(maintenanceName2);
            selectMaintenancesForBooking_PO.BookMaintenances();

            //Assert

            Assert.True(createBooking.CheckListOfBookingItems(expectedBookingItemsInitial));


        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void sprint3ExamenMantenimiento()
        {
            //Arrange
            var createBooking = new CreateBooking_PO(_driver, _output);
            var detailBooking = new DetailBooking_PO(_driver, _output);
            InitialStepsForBookingMaintenances();

            //Act
            selectMaintenancesForBooking_PO.SearchMaintenances(filter1, "");
            selectMaintenancesForBooking_PO.SelectMaintenances(new List<string> { maintenanceName1 });
            selectMaintenancesForBooking_PO.LimpiarPrimerInput();
            selectMaintenancesForBooking_PO.SearchMaintenances("", maintenanceType2);
            selectMaintenancesForBooking_PO.SelectMaintenances(new List<string> { maintenanceName2 });

            selectMaintenancesForBooking_PO.BookMaintenances();
            Thread.Sleep(1000);
            createBooking.PressModifyMaintenances();

            selectMaintenancesForBooking_PO.RemoveMaintenanceFromBookingCart(maintenanceName1);
            Thread.Sleep(1000);
            selectMaintenancesForBooking_PO.BookMaintenances();

            createBooking.FillBookingInfo(customerName, customerSurname, customerAddress, paymentMethod1, customerPhoneNumber);
            createBooking.FillInBookingComent(comment2, maintenanceId2);
            createBooking.PressBookYourMaintenances();
            createBooking.PressOkModalDialog();

            //Assert
            // the expected error is shown in the view
            Assert.True(detailBooking.CheckBookingDetail(customerName + " " + customerSurname, customerAddress, paymentMethod1, DateTime.Now, customerPhoneNumber,
                maintenancePrice2, maintenanceNumberOfDays2), "Error: detail booking is not as expected");

            var expectedBookingItems = new List<string[]>
            {
                new string[] { maintenanceName2, comment2, maintenancePrice2 + " €",maintenanceNumberOfDays2 },
            };

            Assert.True(detailBooking.CheckListOfMaintenances(expectedBookingItems), "Error: booking items are not as expected");
        }



    }
}