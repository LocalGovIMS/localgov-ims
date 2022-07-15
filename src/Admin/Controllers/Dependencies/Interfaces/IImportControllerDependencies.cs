using Admin.Interfaces.ModelBuilders;
using Admin.Models.Import;

namespace Admin.Controllers
{
    public interface IImportControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        #endregion
    }
}
