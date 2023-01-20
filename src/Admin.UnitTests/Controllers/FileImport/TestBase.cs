using Admin.Interfaces.Commands;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FileImportController;
using Dependencies = Admin.Controllers.FileImportControllerDependencies;
using Admin.Classes.Commands.FileImport;

namespace Admin.UnitTests.Controllers.FileImport
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelCommand<SaveCommandArgs>> MockSaveCommand = new Mock<IModelCommand<SaveCommandArgs>>();
        protected readonly Mock<IModelCommand<int>> MockProcessCommand = new Mock<IModelCommand<int>>();

        protected void SetupController()
        { 
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockSaveCommand.Object,
                    MockProcessCommand.Object);

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
