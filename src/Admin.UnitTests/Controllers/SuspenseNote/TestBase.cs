using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.SuspenseNote;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.SuspenseNoteController;
using Dependencies = Admin.Controllers.SuspenseNoteControllerDependencies;

namespace Admin.UnitTests.Controllers.SuspenseNote
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<ListViewModel, int>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, int>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockCreateCommand = new Mock<IModelCommand<EditViewModel>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockListViewModelBuilder.Object,
                    MockCreateCommand.Object);

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
