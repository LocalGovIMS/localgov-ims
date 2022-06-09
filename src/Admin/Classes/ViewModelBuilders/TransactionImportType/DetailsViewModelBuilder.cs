using Admin.Models.TransactionImportType;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.TransactionImportType
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public DetailsViewModelBuilder(ILog log
            , ITransactionImportTypeService transactionImportTypeService)
            : base(log)
        {
            _transactionImportTypeService = transactionImportTypeService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _transactionImportTypeService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                ExternalReference = data.ExternalReference
            };
        }
    }
}