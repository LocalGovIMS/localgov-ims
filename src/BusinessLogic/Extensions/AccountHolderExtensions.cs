using BusinessLogic.Entities;

namespace BusinessLogic.Extensions
{
    public static class AccountHolderExtensions
    {
        public static string FullNameAndTitle(this AccountHolder accountHolder)
        {
            return $"{accountHolder.Title} {accountHolder.Forename} {accountHolder.Surname}".Trim();
        }

        public static string Address(this AccountHolder accountHolder)
        {
            return $@"{accountHolder.AddressLine1?.Trim()}, {accountHolder.AddressLine2?.Trim()}, {accountHolder.AddressLine3?.Trim()}, {accountHolder.AddressLine4?.Trim()}, {accountHolder.Postcode?.Trim()}"
                .Replace(", , , ,", "")
                .Replace(", , ,", ",")
                .Replace(", ,", ",")
                .Trim(',', ' ');
        }

        public static void Update(this AccountHolder existingAccountHolder, AccountHolder newAccountHolder)
        {
            existingAccountHolder.FundCode = newAccountHolder.FundCode;
            existingAccountHolder.CurrentBalance = newAccountHolder.CurrentBalance;
            existingAccountHolder.PeriodCredit = newAccountHolder.PeriodCredit;
            existingAccountHolder.PeriodDebit = newAccountHolder.PeriodDebit;
            existingAccountHolder.Title = newAccountHolder.Title;
            existingAccountHolder.Forename = newAccountHolder.Forename;
            existingAccountHolder.Surname = newAccountHolder.Surname;
            existingAccountHolder.AddressLine1 = newAccountHolder.AddressLine1;
            existingAccountHolder.AddressLine2 = newAccountHolder.AddressLine2;
            existingAccountHolder.AddressLine3 = newAccountHolder.AddressLine3;
            existingAccountHolder.AddressLine4 = newAccountHolder.AddressLine4;
            existingAccountHolder.Postcode = newAccountHolder.Postcode;
            existingAccountHolder.RecordType = newAccountHolder.RecordType;
            existingAccountHolder.UserField1 = newAccountHolder.UserField1;
            existingAccountHolder.UserField2 = newAccountHolder.UserField2;
            existingAccountHolder.UserField3 = newAccountHolder.UserField3;
            existingAccountHolder.StopMessageReference = newAccountHolder.StopMessageReference;
        }
    }
}

