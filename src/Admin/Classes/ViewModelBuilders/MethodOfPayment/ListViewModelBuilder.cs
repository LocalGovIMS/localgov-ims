using Admin.Models.MethodOfPayment;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.MethodOfPayment
{
    public class ListViewModelBuilder : BaseViewModelBuilder<IList<DetailsViewModel>, string>
    {
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public ListViewModelBuilder(ILog log
            , IMethodOfPaymentService methodOfPaymentService)
            : base(log)
        {
            _methodOfPaymentService = methodOfPaymentService;
        }

        protected override IList<DetailsViewModel> OnBuild()
        {
            return _methodOfPaymentService
               .GetAllMops(true)
               .Select(x => new DetailsViewModel()
               {
                   Code = x.MopCode,
                   Name = x.MopName,
                   MaximumAmount = x.MaximumAmount,
                   MinimumAmount = x.MinimumAmount,
                   IsDisabled = x.Disabled
               })
               .ToList();
        }

        protected override IList<DetailsViewModel> OnBuild(string id)
        {
            throw new NotImplementedException();
        }
    }
}