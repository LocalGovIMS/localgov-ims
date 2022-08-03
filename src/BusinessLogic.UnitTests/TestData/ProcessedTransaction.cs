using System;

namespace BusinessLogic.UnitTests.TestData
{
    internal class ProcessedTransaction
    {
        public static Entities.ProcessedTransaction Get()
        {
            return new Entities.ProcessedTransaction()
            {
                TransactionReference = "ABC123",
                InternalReference = "DEF456",
                PspReference = "GHI789",
                OfficeCode = "O1",
                EntryDate = new DateTime(2021, 1, 2, 12, 30, 55, 123),
                TransactionDate = new DateTime(2021, 1, 1, 12, 30, 55, 123),
                AccountReference = "AR12345",
                UserCode = 1,
                FundCode = "F1",
                MopCode = "M1",
                Amount = 10.00M,
                VatCode = "V1",
                VatRate = 0.25F,
                VatAmount = 2.00M,
                Narrative = "A narrative"
            };
        }
    }
}
