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
            , IModelBuilder<ListViewModel, SearchViewModel> listViewModelBuilder
            , IModelBuilder<DetailsViewModel, string> detailsViewModelBuilder
            , [Dependency("Lookup")] IModelCommand<LookupViewModel> lookupAccountHolderCommand
            )
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            LookupAccountHolderCommand = lookupAccountHolderCommand ?? throw new ArgumentNullException("lookupAccountHolderCommand");
        }

        public IModelBuilder<ListViewModel, SearchViewModel> ListViewModelBuilder { get; private set; }
        public IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; set; }
        public IModelCommand<LookupViewModel> LookupAccountHolderCommand { get; private set; }
    }
}