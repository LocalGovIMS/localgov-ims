using Admin.Models.Suspense;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Suspense
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ISuspenseService _suspenseService;

        public DetailsViewModelBuilder(ILog log
            , ISuspenseService suspenseService)
            : base(log)
        {
            _suspenseService = suspenseService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _suspenseService.GetSuspense(id);

            return new DetailsViewModel()
            {
                SuspenseId = data.Item.Id,
                CreatedAt = data.Item.CreatedAt,
                AccountNumber = data.Item.AccountNumber,
                Narrative = data.Item.Narrative,
                Amount = data.Item.Amount,
                AmountRemaining = data.AmountRemaining,
                AmountAllocated = data.AmountAllocated,
                AllocatedPayments = data.Item.SuspenseProcessedTransactions.ToList(),
                Notes = data.Item.Notes
            };
        }
    }
}