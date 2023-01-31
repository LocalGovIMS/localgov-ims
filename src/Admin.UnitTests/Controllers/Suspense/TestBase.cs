using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Suspense;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.SuspenseController;
using Dependencies = Admin.Controllers.SuspenseControllerDependencies;

namespace Admin.UnitTests.Controllers.Suspense
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected readonly Mock<IModelBuilder<JournalViewModel, string>> MockJournalModelViewBuilder = new Mock<IModelBuilder<JournalViewModel, string>>();
        protected readonly Mock<IModelCommand<JournalViewModel>> MockJournalCommand = new Mock<IModelCommand<JournalViewModel>>();
        protected readonly Mock<IModelCommand<SaveNoteViewModel>> MockSaveNoteCommand = new Mock<IModelCommand<SaveNoteViewModel>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                MockLogger.Object,
                MockDetailsViewModelBuilder.Object,
                MockListViewModelBuilder.Object,
                MockJournalModelViewBuilder.Object,
                MockJournalCommand.Object,
                MockSaveNoteCommand.Object);

            Controller = new Controller(dependencies);
        }

        protected MethodInfo GetMethod(Type attributeType, string name)
        {
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == attributeType))
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}
