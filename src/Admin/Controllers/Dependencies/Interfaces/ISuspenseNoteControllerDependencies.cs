using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.SuspenseNote;

namespace Admin.Controllers
{
    public interface ISuspenseNoteControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<ListViewModel, int> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }

        #endregion
    }
}
