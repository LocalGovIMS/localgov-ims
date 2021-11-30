using Admin.Models.MethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.MethodOfPayment
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public EditViewModelBuilder(ILog log
            , IMethodOfPaymentService methodOfPaymentService)
            : base(log)
        {
            _methodOfPaymentService = methodOfPaymentService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(string id)
        {
            var data = _methodOfPaymentService.GetMop(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Code = data.MopCode;
            model.Name = data.MopName;
            model.MaximumAmount = data.MaximumAmount;
            model.MinimumAmount = data.MinimumAmount;
            model.IsDisabled = data.Disabled;

            return model;
        }
    }
}