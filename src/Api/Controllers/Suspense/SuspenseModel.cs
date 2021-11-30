using System;

namespace Api.Controllers.Suspense
{
    public class SuspenseModel
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string AccountNumber { get; set; }

        public string Narrative { get; set; }

        public decimal Amount { get; set; }

        public string BatchReference { get; set; }

        public string ProcessId { get; set; }

        public string Notes { get; set; }

        public SuspenseModel() { }

        public SuspenseModel(BusinessLogic.Entities.Suspense source)
        {
            Id = source.Id;
            TransactionDate = source.TransactionDate;
            CreatedAt = source.CreatedAt;
            AccountNumber = source.AccountNumber;
            Narrative = source.Narrative;
            Amount = source.Amount;
            BatchReference = source.BatchReference;
            ProcessId = source.ProcessId;
            Notes = source.Notes;
        }

        public BusinessLogic.Entities.Suspense GetSuspense()
        {
            return new BusinessLogic.Entities.Suspense()
            {
                Id = Id,
                TransactionDate = TransactionDate,
                CreatedAt = CreatedAt,
                AccountNumber = AccountNumber,
                Narrative = Narrative,
                Amount = Amount,
                BatchReference = BatchReference,
                ProcessId = ProcessId,
                Notes = Notes
            };
        }
    }
}