using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.AccountHolder;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class AccountHolderControllerDependencies : BaseControllerDependencies, IAccountHolderControllerDependencies
    {
        public AccountHolderControllerDependencies(ILog log
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs> detailsViewModelBuilder
            , IModelBuilder<EditViewModel, string> editViewModelBuilder
            , [Dependency("Lookup")] IModelCommand<LookupViewModel> lookupAccountHolderCommand
            , [Dependency("Create")] IModelCommand<EditViewModel> createCommand
            , [Dependency("Edit")] IModelCommand<EditViewModel> editCommand
            )
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            LookupAccountHolderCommand = lookupAccountHolderCommand ?? throw new ArgumentNullException("lookupAccountHolderCommand");
            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
        }

        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }
        public IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs> DetailsViewModelBuilder { get; set; }
        public IModelBuilder<EditViewModel, string> EditViewModelBuilder { get; set; }

        public IModelCommand<LookupViewModel> LookupAccountHolderCommand { get; private set; }
        public IModelCommand<EditViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
    }
}