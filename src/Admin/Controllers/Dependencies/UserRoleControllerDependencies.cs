using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Shared;
using Admin.Models.UserRole;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class UserRoleControllerDependencies : BaseControllerDependencies, IUserRoleControllerDependencies
    {
        public UserRoleControllerDependencies(
            ILog log
            , [Dependency("UserRole.ViewModelBuider.BasicList")] IModelBuilder<BasicListViewModel, int> basicListViewModelBuilder
            , IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelCommand<EditViewModel> editCommand)
             : base(log)
        {
            BasicListViewModelBuilder = basicListViewModelBuilder ?? throw new ArgumentNullException("basicListViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
        }

        public IModelBuilder<BasicListViewModel, int> BasicListViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
    }
}