using Admin.Interfaces.ModelBuilders;
using Admin.Models.TransactionImport;

namespace Admin.Controllers
{
    public interface ITransactionImportControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        #endregion
    }
}
