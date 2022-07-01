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
            , IModelBuilder<DetailsViewModel, int> detailsViewModelBuilder
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder)
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
        }

        #region ModelBuiders

        public IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        #endregion
    }
}