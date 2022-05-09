using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Shared;
using Admin.Models.UserMethodOfPayment;

namespace Admin.Controllers
{
    public interface IUserMethodOfPaymentControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<BasicListViewModel, int> BasicListViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}
