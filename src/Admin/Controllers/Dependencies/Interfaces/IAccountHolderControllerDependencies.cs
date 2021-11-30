using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.AccountHolder;

namespace Admin.Controllers
{
    public interface IAccountHolderControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders
        IModelBuilder<ListViewModel, SearchViewModel> ListViewModelBuilder { get; }
        IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; }
        IModelCommand<LookupViewModel> LookupAccountHolderCommand { get; }
        #endregion
    }
}