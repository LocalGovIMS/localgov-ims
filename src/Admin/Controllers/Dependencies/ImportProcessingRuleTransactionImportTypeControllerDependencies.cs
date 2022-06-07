using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.ImportProcessingRuleTransactionImportType;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class ImportProcessingRuleTransactionImportTypeControllerDependencies : BaseControllerDependencies, IImportProcessingRuleTransactionImportTypeControllerDependencies
    {
        public ImportProcessingRuleTransactionImportTypeControllerDependencies(
            ILog log
            , IModelBuilder<DetailsViewModel, int> detailsViewModelBuilder
            , [Dependency("Create")] IModelBuilder<EditViewModel, int> createViewModelBuilder
            , [Dependency("Edit")] IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , [Dependency("Create")] IModelCommand<EditViewModel> createCommand
            , [Dependency("Edit")] IModelCommand<EditViewModel> editCommand
            , [Dependency("ImportProcessingRuleTransactionImportType.Command.Delete")] IModelCommand<int> deleteCommand
            )
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            CreateViewModelBuilder = createViewModelBuilder ?? throw new ArgumentNullException("createViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
            DeleteCommand = deleteCommand ?? throw new ArgumentNullException("deleteCommand");
        }

        #region ModelBuiders

        public IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> CreateViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        public IModelCommand<EditViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
        public IModelCommand<int> DeleteCommand { get; private set; }

        #endregion
    }
}