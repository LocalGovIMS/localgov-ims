using Admin.Classes.Commands.ImportProcessingRule;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.ImportProcessingRule;

namespace Admin.Controllers
{
    public interface IImportProcessingRuleControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
        IModelCommand<ChangeStatusCommandArgs> ChangeStatusCommand { get; }

        #endregion
    }
}
