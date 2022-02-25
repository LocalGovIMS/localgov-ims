using Admin.Models.PaymentIntegration;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.PaymentIntegration
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, int>
    {
        private readonly IPaymentIntegrationService _paymentIntegrationService;

        public ListViewModelBuilder(ILog log
            , IPaymentIntegrationService paymentIntegrationService)
            : base(log)
        {
            _paymentIntegrationService = paymentIntegrationService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _paymentIntegrationService
               .GetAll()
               .Select(x => new DetailsViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   BaseUri = x.BaseUri
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(int id)
        {
            throw new NotImplementedException();
        }
    }
}