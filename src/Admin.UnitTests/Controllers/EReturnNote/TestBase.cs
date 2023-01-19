using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.EReturnNoteController;
using Dependencies = Admin.Controllers.EReturnNoteControllerDependencies;

namespace Admin.UnitTests.Controllers.EReturnNote
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Admin.Models.EReturnNote.ListViewModel, int>> MockListViewModelBuilder = new Mock<IModelBuilder<Admin.Models.EReturnNote.ListViewModel, int>>();
        protected readonly Mock<IModelCommand<Admin.Models.EReturnNote.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Admin.Models.EReturnNote.EditViewModel>>();

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
