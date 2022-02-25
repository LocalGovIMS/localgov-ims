using Admin.Models.PaymentIntegration;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.PaymentIntegration
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IPaymentIntegrationService _paymentIntegrationService;

        public EditViewModelBuilder(ILog log
            , IPaymentIntegrationService paymentIntegrationService)
            : base(log)
        {
            _paymentIntegrationService = paymentIntegrationService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _paymentIntegrationService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.BaseUri = data.BaseUri;
            
            return model;
        }
    }
}