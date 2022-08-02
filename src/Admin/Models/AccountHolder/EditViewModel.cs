using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.AccountHolder
{
    public class EditViewModel
    {
        [DisplayName("Account reference")]
        [Required(ErrorMessage = "Account reference is required")]
        public string AccountReference { get; set; }

        [DisplayName("Fund type")]
        public string FundCode { get; set; }

        [DisplayName("Current balance")]
        public decimal? CurrentBalance { get; set; }

        [DisplayName("Period debit")] // TODO: What does this really represent? Period Debit means nothing
        public decimal? PeriodDebit { get; set; }

        [DisplayName("Period credit")] // TODO: What does this really represent? Period Credit means nothing
        public decimal? PeriodCredit { get; set; }

        public string Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [DisplayName("Address line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address line 2")]
        public string AddressLine2 { get; set; }

        [DisplayName("Address line 3")]
        public string AddressLine3 { get; set; }

        [DisplayName("Address line 4")]
        public string AddressLine4 { get; set; }

        [DisplayName("Postcode")]
        public string Postcode { get; set; }

        [DisplayName("Record type")]
        public string RecordType { get; set; }

        [DisplayName("User field 1")]
        public string UserField1 { get; set; }

        [DisplayName("User field 2")]
        public string UserField2 { get; set; }

        [DisplayName("User field 3")]
        public string UserField3 { get; set; }

        [DisplayName("Message")]
        public int? FundMessageId { get; set; }
        
        public SelectList Funds { get; set; }

        public SelectList FundMessages { get; set; }
    }
}