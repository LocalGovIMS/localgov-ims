using System;
using System.ComponentModel;

namespace Admin.Models.AccountHolder
{
    public class DetailsViewModel
    {
        [DisplayName("Account reference")]
        public string AccountReference { get; set; }

        [DisplayName("Fund code")]
        public string FundCode { get; set; }

        [DisplayName("Fund name")]
        public string FundName { get; set; }

        [DisplayName("Current balance")]
        public decimal? CurrentBalance { get; set; }

        [DisplayName("Period debit")] // TODO: What does this really represent? Period Debit means nothing
        public decimal? PeriodDebit { get; set; }

        [DisplayName("Period credit")] // TODO: What does this really represent? Period Credit means nothing
        public decimal? PeriodCredit { get; set; }

        public string Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [DisplayName("Name")]
        public string FullNameAndTitle { get; set; }

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

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Record type")]
        public string RecordType { get; set; }

        [DisplayName("User defined information")]
        public string UserField1 { get; set; }

        [DisplayName("User defined information")]
        public string UserField2 { get; set; }

        [DisplayName("User defined information")]
        public string UserField3 { get; set; }

        [DisplayName("Message")]
        public int? FundMessageId { get; set; }

        [DisplayName("Message")]
        public string FundMessage { get; set; }

        [DisplayName("Last updated")]
        public DateTime? LastUpdated { get; set; }

        public bool ShowSelect { get; set; }
    }
}