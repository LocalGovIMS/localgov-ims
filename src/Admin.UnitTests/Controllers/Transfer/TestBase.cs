using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transfer;
using BusinessLogic.Models;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.TransferController;
using Dependencies = Admin.Controllers.TransferControllerDependencies;

namespace Admin.UnitTests.Controllers.Transfer
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<TransferViewModel, string>> MockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        protected readonly Mock<IModelCommand<TransferViewModel>> MockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        protected readonly Mock<IModelCommand<TransferItem>> MockValidateTransferItemCommand = new Mock<IModelCommand<TransferItem>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockTransferViewModelBuilder.Object,
                    MockTransferCommand.Object,
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
