using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Suspense;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Suspense.JournalAllocation
{
    public class JournalAllocationStrategyValidator : IJournalAllocationStrategyValidator
    {
        private readonly IFundService _fundService;
        
        private string _suspenseFundCode;
        private List<string> _creditNoteFundCodes;

        public JournalAllocationStrategyValidator(IFundService fundService)
        {
            _fundService = fundService ?? throw new ArgumentNullException("fundService");

            var funds = _fundService.GetAllFunds();

            _suspenseFundCode = funds
                .FirstOrDefault(y => y.IsASuspenseJournalFund())
                .FundCode;
            
            _creditNoteFundCodes = funds
                .Where(x => x.IsACreditNoteEnabledFund())
                .Select(x => x.FundCode)
                .ToList();
        }

        public void Validate(JournalAllocationStrategyValidatorArgs args)
        {
            if (args.Suspenses.IsNullOrEmpty())
                throw new SuspenseJournalAllocationException("You must choose some suspense items");

            if (args.Suspenses.Sum(x => x.AmountRemaining) <= 0)
                throw new SuspenseJournalAllocationException("Value of chosen suspense items must be greater than zero");

            var creditNoteTotal = args.CreditNotes == null || !args.CreditNotes.Any()
                ? 0
                : args.CreditNotes.Sum(c => c.Amount);

            if (args.JournalItems.Sum(x => x.Amount) != (args.Suspenses.Sum(x => x.AmountRemaining) + creditNoteTotal))
                throw new SuspenseJournalAllocationException("Value of chosen suspense items and credit notes must match amount to be journalled");

            if (args.JournalItems.Any(x => x.FundCode == _suspenseFundCode))
                throw new SuspenseJournalAllocationException("You cannot journal a suspense item back to suspense");

            if (args.CreditNotes != null)
            {
                var creditNotesWithIvalidFunds = args.CreditNotes.Where(i => !_creditNoteFundCodes.Contains(i.FundCode));
                if (creditNotesWithIvalidFunds.Any())
                    throw new SuspenseJournalAllocationException("Credit notes exist with invalid funds");
            }
        }
    }

    public class JournalAllocationStrategyValidatorArgs
    {
        public List<SuspenseWrapper> Suspenses { get; set; }
        public List<Journal> JournalItems { get; set; }
        public List<CreditNote> CreditNotes { get; set; }
    }
}
