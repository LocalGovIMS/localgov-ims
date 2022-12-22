using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.AccountHolder
{
    public class SearchCriteria
    {
        [Display(Name = "Account reference")]
        public string AccountReference { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Fund type")]
        public string FundCode { get; set; }

        public bool IsAPaymentSearch { get; set; }

        public int Page { get; set; }

        public SelectList Funds { get; set; }
    }
}