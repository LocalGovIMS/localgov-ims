using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Shared;
using Admin.Models.UserMethodOfPayment;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class UserMethodOfPaymentControllerDependencies : BaseControllerDependencies, IUserMethodOfPaymentControllerDependencies
    {
        public UserMethodOfPaymentControllerDependencies(
            ILog log
            , [Dependency("UserMethodOfPayment.ViewModelBuider.BasicList")] IModelBuilder<BasicListViewModel, int> basicListViewModelBuilder
            , IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelCommand<EditViewModel> editCommand)
             : base(log)
        {
            BasicListViewModelBuilder = basicListViewModelBuilder ?? throw new ArgumentNullException("basicListViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
        }

        public IModelBuilder<BasicListViewModel, int> BasicListViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
    }
}