using Admin.Interfaces.Commands;
using BusinessLogic.Models;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.ValidationController;
using Dependencies = Admin.Controllers.ValidationControllerDependencies;

namespace Admin.UnitTests.Controllers.Validation
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelCommand<TransferItem>> MockValidateTransferItemCommand = new Mock<IModelCommand<TransferItem>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockValidateTransferItemCommand.Object);

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
