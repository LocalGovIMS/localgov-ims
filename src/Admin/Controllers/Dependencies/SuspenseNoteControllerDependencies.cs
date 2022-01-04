using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.SuspenseNote;
using log4net;
using System;

namespace Admin.Controllers
{
    public class SuspenseNoteControllerDependencies : BaseControllerDependencies, ISuspenseNoteControllerDependencies
    {
        public SuspenseNoteControllerDependencies(
            ILog log
            , IModelBuilder<ListViewModel, int> listViewModelBuilder
            , IModelCommand<EditViewModel> createCommand)
             : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            CreateCommand = createCommand ?? throw new ArgumentNullException("createCommand");
        }

        #region ModelBuiders

        public IModelBuilder<ListViewModel, int> ListViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        public IModelCommand<EditViewModel> CreateCommand { get; private set; }

        #endregion
    }
}