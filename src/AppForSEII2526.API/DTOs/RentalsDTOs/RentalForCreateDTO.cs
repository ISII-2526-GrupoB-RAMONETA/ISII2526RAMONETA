using AppForSEII2526.API.Models;
using System.ComponentModel;

namespace AppForSEII2526.API.DTOs.RentalDTO
{
    public class RentalForCreateDTO
    {
        public RentalForCreateDTO(string customerUserName, string customerName, string customerSurname,
            string deliveryAddress, PaymentMethodTypes paymentMethod, bool deliveryCarDealer, DateTime rentalDateFrom, DateTime rentalDateTo, IList<RentalItemDTO> rentalItems)
        {
            CustomerUserName = customerUserName;
            CustomerName = customerName;
            CustomerSurname = customerSurname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            DeliveryCarDealer = deliveryCarDealer;
            RentalDateFrom = rentalDateFrom;
            RentalDateTo = rentalDateTo;
            RentalItems = rentalItems;
        }

        public RentalForCreateDTO()
        {
            RentalItems = new List<RentalItemDTO>();
        }

        public DateTime RentalDateFrom { get; set; }

        public DateTime RentalDateTo { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Delivery address must have at least 10 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [EmailAddress]
        [Required]
        public string CustomerUserName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name and Surname must have at least 5 characters")]
        public string CustomerName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name and Surname must have at least 5 characters")]
        public string CustomerSurname { get; set; }

        public IList<RentalItemDTO> RentalItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                var days = NumberOfDays;
                if (days <= 0 || RentalItems == null || RentalItems.Count == 0)
                    return 0m;

                return RentalItems.Sum(ri => ri.PriceForRenting * ri.Quantity * (decimal)days);
            }
        }

        [Required]
        public PaymentMethodTypes PaymentMethod { get; set; }
        public bool DeliveryCarDealer { get; set; }
        public DateTime RentalDate { get; set; }

        //public decimal TotalPrice { get; set; }

        private int NumberOfDays
        {
            get
            {
                // Usar TotalDays para incluir fracciones si las hubiera; truncar a int y garantizar >= 0
                var days = (int)(RentalDateTo - RentalDateFrom).TotalDays;
                return Math.Max(0, days);
            }
        }

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalForCreateDTO dTO &&
                   CompareDate(RentalDateFrom, dTO.RentalDateFrom) &&
                   CompareDate(RentalDateTo, dTO.RentalDateTo) &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   CustomerUserName == dTO.CustomerUserName &&
                   CustomerName == dTO.CustomerName &&
                   CustomerSurname == dTO.CustomerSurname &&
                   RentalItems.SequenceEqual(dTO.RentalItems) &&
                   PaymentMethod == dTO.PaymentMethod;
        }
    }
}
