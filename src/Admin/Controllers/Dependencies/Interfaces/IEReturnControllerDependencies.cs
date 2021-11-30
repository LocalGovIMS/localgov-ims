using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturn;

namespace Admin.Controllers
{
    public interface IEReturnControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<CreateViewModel, int> CreateViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<CreateViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
        IModelCommand<EditViewModel> ApproverEditCommand { get; }
        IModelCommand<int> DeleteCommand { get; }
        IModelCommand<int> SubmitCommand { get; }
        IModelCommand<ApproveViewModel> AuthoriseCommand { get; }

        #endregion
    }
}