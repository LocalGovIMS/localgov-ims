using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturnNote;

namespace Admin.Controllers
{
    public interface IEReturnNoteControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<ListViewModel, int> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }

        #endregion
    }
}
