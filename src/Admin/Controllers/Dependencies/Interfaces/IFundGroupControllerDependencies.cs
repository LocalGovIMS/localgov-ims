using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.FundGroup;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IFundGroupControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> CreateViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<IList<DetailsViewModel>, int> ListViewModelBuilder { get; }
        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
        IModelCommand<int> DeleteCommand { get; }
    }
}
