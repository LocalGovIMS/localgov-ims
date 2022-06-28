using Admin.Interfaces.ModelBuilders;
using Admin.Models.TransactionImport;
using log4net;
using System;

namespace Admin.Controllers
{
    public class TransactionImportControllerDependencies : BaseControllerDependencies, ITransactionImportControllerDependencies
    {
        public TransactionImportControllerDependencies(
            ILog log
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder)
             : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
        }

        #region ModelBuiders

        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        #endregion
    }
}