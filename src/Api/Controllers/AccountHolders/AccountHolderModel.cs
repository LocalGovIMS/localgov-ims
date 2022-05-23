using BusinessLogic.Entities;

namespace Api.Controllers.AccountHolders
{
    public class AccountHolderModel
    {
        public string AccountReference { get; set; }

        public string FundCode { get; set; }

        public decimal? CurrentBalance { get; set; }

        public decimal? PeriodCredit { get; set; } // TODO: What does this mean? Rename it something more expressive

        public decimal? PeriodDebit { get; set; } // TODO: What does this mean? Rename it something more expressive

        public string Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string HouseNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string AddressLine4 { get; set; }

        public string Postcode { get; set; }

        public string RecordType { get; set; }

        public string UserField1 { get; set; }

        public string UserField2 { get; set; }

        public string UserField3 { get; set; }

        public string PersonalisedMessage { get; set; }

        public int? FundMessageId { get; set; }

        public AccountHolderModel() { }

        public AccountHolderModel(AccountHolder source)
        {
            AccountReference = source.AccountReference;
            FundCode = source.FundCode;
            CurrentBalance = source.CurrentBalance;
            PeriodCredit = source.PeriodCredit;
            PeriodDebit = source.PeriodDebit;
            Title = source.Title;
            Forename = source.Forename;
            Surname = source.Surname;
            AddressLine1 = source.AddressLine1;
            AddressLine2 = source.AddressLine2;
            AddressLine3 = source.AddressLine3;
            AddressLine4 = source.AddressLine4;
            Postcode = source.Postcode;
            RecordType = source.RecordType;
            UserField1 = source.UserField1;
            UserField2 = source.UserField2;
            UserField3 = source.UserField3;
            FundMessageId = source.FundMessageId;
        }

        public AccountHolder GetAccountHolder()
        {
            return new AccountHolder()
            {
                AccountReference = AccountReference,
                FundCode = FundCode,
                CurrentBalance = CurrentBalance,
                PeriodCredit = PeriodCredit,
                PeriodDebit = PeriodDebit,
                Title = Title,
                Forename = Forename,
                Surname = Surname,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                AddressLine3 = AddressLine3,
                AddressLine4 = AddressLine4,
                Postcode = Postcode,
                RecordType = RecordType,
                UserField1 = UserField1,
                UserField2 = UserField2,
                UserField3 = UserField3,
                FundMessageId = FundMessageId
            };
        }
    }
}