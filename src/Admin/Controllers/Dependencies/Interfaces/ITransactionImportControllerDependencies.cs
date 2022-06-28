using Admin.Interfaces.ModelBuilders;
using Admin.Models.TransactionImport;

namespace Admin.Controllers
{
    public interface ITransactionImportControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        #endregion
    }
}
