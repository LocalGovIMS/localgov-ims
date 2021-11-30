using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Vat;
using log4net;
using System;
using System.Collections.Generic;
using Unity;

namespace Admin.Controllers
{
    public class VatControllerDependencies : BaseControllerDependencies, IVatControllerDependencies
    {
        public VatControllerDependencies(
            ILog log
            , IModelBuilder<DetailsViewModel, string> detailsViewModelBuilder
            , IModelBuilder<EditViewModel, string> editViewModelBuilder
            , IModelBuilder<IList<DetailsViewModel>, string> listViewModelBuilder
            , [Dependency("Create")] IModelCommand<EditViewModel> createCommand
            , [Dependency("Edit")] IModelCommand<EditViewModel> editCommand)
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
        }

        #region ModelBuiders

        public IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, string> EditViewModelBuilder { get; private set; }
        public IModelBuilder<IList<DetailsViewModel>, string> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        public IModelCommand<EditViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }

        #endregion
    }
}