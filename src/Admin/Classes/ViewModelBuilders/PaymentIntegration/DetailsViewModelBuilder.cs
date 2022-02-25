using Admin.Models.PaymentIntegration;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.PaymentIntegration
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IPaymentIntegrationService _paymentIntegrationService;

        public DetailsViewModelBuilder(ILog log
            , IPaymentIntegrationService paymentIntegrationService)
            : base(log)
        {
            _paymentIntegrationService = paymentIntegrationService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _paymentIntegrationService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                BaseUri = data.BaseUri
            };
        }
    }
}