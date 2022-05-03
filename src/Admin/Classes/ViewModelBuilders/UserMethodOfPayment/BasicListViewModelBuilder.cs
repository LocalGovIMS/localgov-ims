using Admin.Models.Shared;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.UserMethodOfPayment
{
    public class BasicListViewModelBuilder : BaseViewModelBuilder<BasicListViewModel, int>
    {
        private readonly IUserMethodOfPaymentService _userMethodOfPaymentService;

        public BasicListViewModelBuilder(ILog log
            , IUserMethodOfPaymentService userMethodOfPaymentService)
            : base(log)
        {
            _userMethodOfPaymentService = userMethodOfPaymentService;
        }

        protected override BasicListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override BasicListViewModel OnBuild(int id)
        {
            var data = _userMethodOfPaymentService.GetByUserId(id).OrderBy(x => x.Mop.MopName);

            var model = new BasicListViewModel()
            {
                ListTitle = "User methods of payment",
                ColumnTitle = "Method of payment",
                NoItemsMessage = "There are no methods of payment specified",
                Items = data.Select(x => x.Mop.MopName).ToList()
            };

            return model;
        }
    }
}