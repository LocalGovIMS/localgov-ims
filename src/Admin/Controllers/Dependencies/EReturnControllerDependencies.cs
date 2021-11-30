using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturn;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class EReturnControllerDependencies : BaseControllerDependencies, IEReturnControllerDependencies
    {
        public EReturnControllerDependencies(ILog log
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , IModelBuilder<CreateViewModel, int> createViewModelBuilder
            , [Dependency("EReturn.ViewModelBuilder.Details")] IModelBuilder<EditViewModel, int> detailsViewModelBuilder
            , [Dependency("EReturn.ViewModelBuilder.Edit")] IModelBuilder<EditViewModel, int> editViewModelBuilder
            , IModelCommand<CreateViewModel> createCommand
            , [Dependency("EReturn.Command.Edit")] IModelCommand<EditViewModel> editCommand
            , [Dependency("EReturn.Command.ApproverEdit")] IModelCommand<EditViewModel> approverEditCommand
            , [Dependency("EReturn.Command.Delete")] IModelCommand<int> deleteCommand
            , [Dependency("EReturn.Command.Submit")] IModelCommand<int> submitCommand
            , IModelCommand<ApproveViewModel> authoriseCommand
            )
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            CreateViewModelBuilder = createViewModelBuilder ?? throw new ArgumentNullException("createViewModelBuilder");
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            EditViewModelBuilder = editViewModelBuilder ?? throw new ArgumentNullException("editViewModelBuilder");

            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
            EditCommand = editCommand ?? throw new ArgumentNullException("editCommand");
            ApproverEditCommand = approverEditCommand ?? throw new ArgumentNullException("approverEditCommand");
            DeleteCommand = deleteCommand ?? throw new ArgumentNullException("deleteCommand");
            SubmitCommand = submitCommand ?? throw new ArgumentNullException("submitCommand");
            AuthoriseCommand = authoriseCommand ?? throw new ArgumentNullException("authoriseCommand");
        }

        #region ViewModelBuidlers

        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }
        public IModelBuilder<CreateViewModel, int> CreateViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> DetailsViewModelBuilder { get; private set; }

        #endregion

        #region Commands

        public IModelCommand<CreateViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
        public IModelCommand<EditViewModel> ApproverEditCommand { get; private set; }
        public IModelCommand<int> DeleteCommand { get; private set; }
        public IModelCommand<int> SubmitCommand { get; private set; }
        public IModelCommand<ApproveViewModel> AuthoriseCommand { get; private set; }

        #endregion
    }
}