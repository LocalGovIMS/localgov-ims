using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Shared;
using Admin.Models.UserFundGroup;

namespace Admin.Controllers
{
    public interface IUserFundGroupControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<BasicListViewModel, int> BasicListViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}
