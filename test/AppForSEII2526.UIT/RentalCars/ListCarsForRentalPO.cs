using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.RentalCars
{
    internal class ListCarsForRentalPO : PageObject
    {
        private By _carPrecioBy = By.Id("inputprecio");
        private By _carModelBy = By.Id("selectModel");
        private By _fromBy = By.Id("fromDate");
        private By _toBy = By.Id("toDate");
        private By _DateBy = By.Id("rentalDate");


        private By _ShowRentingCartBy = By.Id("showRentingCart");
        private IWebElement _showRentingCartButton() => _driver.FindElement(_ShowRentingCartBy);
        private By _searchCarsBy = By.Id("searchCars");
        private By _rentButtonBy = By.Id("rent");

        private By _tableOfCarsBy = By.Id("TableOfCars");
        private By _modalBy = By.Id("DialogOKSaveDelete");

        private IWebElement _carPrecio() => _driver.FindElement(_carPrecioBy);
        //this code is a shortcut for:
        // private IWebElement _carTitle() {return _driver.FindElement(By.Id("carTitle"));}

        private IWebElement _carModel() => _driver.FindElement(_carModelBy);
        //private IWebElement _showRentingCartButton() => _driver.FindElement(_ShowRentingCartBy);
        private IWebElement _searchCarsButton() => _driver.FindElement(_searchCarsBy);
        private IWebElement _rentButton() => _driver.FindElement(_rentButtonBy);

        public ListCarsForRentalPO(IWebDriver driver, ITestOutputHelper output) :
            base(driver, output)
        {
        }

        public void FilterCars(string precioFiltro, string ModelFiltro,
           DateTime from, DateTime to, DateTime rentaldate)
        {
            WaitForBeingVisible(_carPrecioBy);


            _carPrecio().SendKeys(precioFiltro);

            //if no model is selected then all the models are applicable
            if (ModelFiltro == "") ModelFiltro = "All";

            //create select element object 
            SelectElement selectElement = new SelectElement(_carModel());
            //select Action from the dropdown menu
            selectElement.SelectByText(ModelFiltro);

            InputDateInDatePicker(_fromBy, from);
            InputDateInDatePicker(_toBy, to);
            InputDateInDatePicker(_DateBy, rentaldate);

            _searchCarsButton().Click();
            //we wait for 2 seconds (2000 milliseconds) till the table is reloaded as we have to wait for the API service to be called
            System.Threading.Thread.Sleep(2000);

        }

        public void SelectCars(List<string> carIds)
        {
            //we wait for till the cars are available to be selected 
            foreach (var carId in carIds)
            {
                WaitForBeingVisible(By.Id($"carToRent_{carId}"));
                _driver.FindElement(By.Id($"carToRent_{carId}")).Click();
            }
        }
        public void RentCars()
        {
            WaitForBeingClickable(_rentButtonBy);
            _rentButton().Click();
        }

        public void ModifyRentingCart(string id)
        {
            //_showRentingCartButton().Click();
            WaitForBeingVisible(By.Id($"removeCar_{id}"));
            _driver.FindElement(By.Id($"removeCar_{id}")).Click();

        }

        //public bool ModifyRentingCart (string price)
        //{
        //    WaitForBeingVisible(totalprice);

        //    int currentprice = int.Parse(_driver.FindElement(totalPrice).Text);

        //    int expectedprice = int.Parse(price);

        //    return currentprice == expectedprice;
        //}

        public bool CheckListOfCars(List<string[]> expectedCars)
        {

            return CheckBodyTable(expectedCars, _tableOfCarsBy);
        }

        public bool CheckRentCarsDisabled()
        {
            //we return true if the button is disabled
            return _driver.FindElement(_rentButtonBy).Displayed == false;
        }

        public bool CheckShoppingCart(string price)
        {
            //string texto = _showRentingCartButton().Text;
            //WaitForTextToBePresentInElement(_rentButtonBy, $"Renting Cart: {price} €" );
            return _showRentingCartButton().Text.Contains(price);
        }

        public bool CheckMessageErrorNotAvailableCars(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);

        }

        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, _modalBy);
        }
    }
}