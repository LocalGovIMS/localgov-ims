using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Suspense;
using log4net;
using System;

namespace Admin.Controllers
{
    public class SuspenseControllerDependencies : BaseControllerDependencies, ISuspenseControllerDependencies
    {
        public SuspenseControllerDependencies(
            ILog log
            , IModelBuilder<DetailsViewModel, int> detailsViewModelBuilder
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , IModelBuilder<JournalViewModel, string> journalViewModelBuilder
            , IModelCommand<JournalViewModel> journalCommand
            , IModelCommand<SaveNoteViewModel> saveNoteCommand)
             : base(log)
        {
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            JournalViewModelBuilder = journalViewModelBuilder ?? throw new ArgumentNullException("journalViewModelBuilder");
            JournalCommand = journalCommand ?? throw new ArgumentNullException("journalCommand");
            SaveNoteCommand = saveNoteCommand ?? throw new ArgumentNullException("saveNoteCommand");
        }

        #region ModelBuiders

        public IModelBuilder<DetailsViewModel, int> DetailsViewModelBuilder { get; private set; }
        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }
        public IModelBuilder<JournalViewModel, string> JournalViewModelBuilder { get; private set; }

        #endregion

        #region ModelCommands

        public IModelCommand<JournalViewModel> JournalCommand { get; private set; }
        public IModelCommand<SaveNoteViewModel> SaveNoteCommand { get; private set; }

        #endregion
    }
}