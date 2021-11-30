using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Role;
using log4net;
using System;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public class RoleControllerDependencies : BaseControllerDependencies, IRoleControllerDependencies
    {
        public RoleControllerDependencies(
            ILog log
            , IModelBuilder<DetailsViewModel, int> detailsViewModelBuilder
            , IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelBuilder<IList<DetailsViewModel>, int> listViewModelBuilder
            , IModelCommand<EditViewModel> editCommand)
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
        }

        public IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelBuilder<IList<DetailsViewModel>, int> ListViewModelBuilder { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
    }
}