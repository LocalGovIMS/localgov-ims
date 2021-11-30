using Admin.Classes.Commands.ImportProcessingRule;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.ImportProcessingRule;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class ImportProcessingRuleControllerDependencies : BaseControllerDependencies, IImportProcessingRuleControllerDependencies
    {
        public ImportProcessingRuleControllerDependencies(
            ILog log
            , IModelBuilder<DetailsViewModel, int> detailsViewModelBuilder
            , IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , [Dependency("Create")] IModelCommand<EditViewModel> createCommand
            , [Dependency("Edit")] IModelCommand<EditViewModel> editCommand
            , IModelCommand<ChangeStatusCommandArgs> changeStatusCommand)
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
            ChangeStatusCommand = changeStatusCommand ?? throw new ArgumentNullException("changeStatusCommand");
        }

        #region ModelBuiders

        public IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        public IModelCommand<EditViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
        public IModelCommand<ChangeStatusCommandArgs> ChangeStatusCommand { get; private set; }
        #endregion
    }
}