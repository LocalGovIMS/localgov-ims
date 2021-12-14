using Admin.Models.MethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.MethodOfPayment
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public DetailsViewModelBuilder(ILog log
            , IMethodOfPaymentService methodOfPaymentService)
            : base(log)
        {
            _methodOfPaymentService = methodOfPaymentService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string id)
        {
            var data = _methodOfPaymentService.GetMop(id);

            return new DetailsViewModel()
            {
                Code = data.MopCode,
                Name = data.MopName,
                MaximumAmount = data.MaximumAmount,
                MinimumAmount = data.MinimumAmount,
                IsDisabled = data.Disabled,
            };
        }
    }
}