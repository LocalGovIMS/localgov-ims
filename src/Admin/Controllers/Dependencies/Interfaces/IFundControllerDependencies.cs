using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Fund;

namespace Admin.Controllers
{
    public interface IFundControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, string> EditViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }

        #endregion
    }
}
