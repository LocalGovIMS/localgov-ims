using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.PaymentIntegration;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IPaymentIntegrationControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders

        IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; }
        IModelBuilder<IList<DetailsViewModel>, int> ListViewModelBuilder { get; }

        #endregion

        #region ModelCommands

        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }

        #endregion
    }
}
