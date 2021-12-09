using System.ComponentModel.DataAnnotations;

namespace Admin.Models.AccountHolder
{
    public class SearchCriteria
    {
        [Display(Name = "Account reference")]
        public string AccountReference { get; set; }

        [Display(Name = "House No")]
        public string HouseNumberName { get; set; }

        public string Street { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public string Surname { get; set; }

        public string FundCode { get; set; }

        public bool IsAPaymentSearch { get; set; }

        public int Page { get; set; }
    }
}