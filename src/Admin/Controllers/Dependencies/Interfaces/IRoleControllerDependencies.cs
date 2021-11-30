using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Role;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IRoleControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<IList<DetailsViewModel>, int> ListViewModelBuilder { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}
