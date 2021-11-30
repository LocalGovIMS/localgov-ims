using BusinessLogic.Models.Suspense;
using System.Collections.Generic;
using Web.Mvc;

namespace Admin.Models.Suspense
{
    public class JournalViewModel
    {
        public List<int> SuspenseItems { get; set; }
        public List<Journal> JournalItems { get; set; }
        public List<CreditNote> CreditNotes { get; set; }

        public SelectList Funds { get; set; }
        public SelectList CreditNoteFunds { get; set; }
        public SelectList VatCodes { get; set; }
        public SelectList MopCodes { get; set; }
        public Journal JournalItem { get; set; }
        public CreditNote CreditNote { get; set; }

        public string DefaultJournalReallocationMopCode { get; set; }
    }
}