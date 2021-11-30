using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Template;

namespace Admin.Controllers
{
    public interface ITemplateControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<ListViewModel, int> ListViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}