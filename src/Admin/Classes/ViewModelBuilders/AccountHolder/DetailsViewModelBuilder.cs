using Admin.Models.AccountHolder;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.AccountHolder
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly IAccountHolderService _accountHolderService;

        public DetailsViewModelBuilder(ILog log
            , IAccountHolderService accountHolderService)
            : base(log)
        {
            _accountHolderService = accountHolderService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string accountReference)
        {
            var item = _accountHolderService.GetByAccountReference(accountReference);

            return new DetailsViewModel()
            {
                AccountHolder = item
            };
        }
    }
}