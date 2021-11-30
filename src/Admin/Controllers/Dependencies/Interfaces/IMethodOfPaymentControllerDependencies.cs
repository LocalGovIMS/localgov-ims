using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.MethodOfPayment;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IMethodOfPaymentControllerDependencies : IBaseControllerDependencies
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
