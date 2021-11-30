using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Suspense;

namespace Admin.Controllers
{
    public interface ISuspenseControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }
        IModelBuilder<JournalViewModel, string> JournalViewModelBuilder { get; }
        #endregion

        #region ModelCommands

        IModelCommand<JournalViewModel> JournalCommand { get; }
        IModelCommand<SaveNoteViewModel> SaveNoteCommand { get; }

        #endregion
    }
}
