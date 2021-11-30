using BusinessLogic.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class JournalTransaction
    {
        public ProcessedTransaction Credit { get; private set; }
        public ProcessedTransaction Debit { get; private set; }
        public List<ProcessedTransaction> CreditNotes { get; private set; }
        public string PspReference { get { return Credit.PspReference; } }
        public DateTime? EntryDate { get { return Credit.EntryDate; } }
        public Entities.User User { get { return Credit.User; } }
        public int UserCode { get { return Credit.UserCode; } }
        public string Narrative { get { return Credit.Narrative; } }
        public decimal? Amount { get { return Credit.Amount; } }

        public JournalTransaction(ProcessedTransaction credit, ProcessedTransaction debit, List<ProcessedTransaction> creditNotes)
        {
            Credit = credit;
            Debit = debit;
            CreditNotes = creditNotes;
        }
    }
}
