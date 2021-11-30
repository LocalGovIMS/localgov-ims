using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Shared;
using Admin.Models.UserTemplate;

namespace Admin.Controllers
{
    public interface IUserTemplateControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<BasicListViewModel, int> BasicListViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}
