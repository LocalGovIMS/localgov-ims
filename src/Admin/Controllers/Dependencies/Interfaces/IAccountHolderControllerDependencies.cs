using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.AccountHolder;

namespace Admin.Controllers
{
    public interface IAccountHolderControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }
        IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs> DetailsViewModelBuilder { get; }
        IModelBuilder<EditViewModel, string> EditViewModelBuilder { get; }

        IModelCommand<LookupViewModel> LookupAccountHolderCommand { get; }
        IModelCommand<EditViewModel> CreateCommand { get; }
        IModelCommand<EditViewModel> EditCommand { get; }
    }
}