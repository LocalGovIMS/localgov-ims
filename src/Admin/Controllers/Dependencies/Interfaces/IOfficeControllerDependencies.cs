using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Office;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IOfficeControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, string> EditViewModelBuilder { get; }
        IModelBuilder<IList<DetailsViewModel>, string> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }

        #endregion
    }
}
