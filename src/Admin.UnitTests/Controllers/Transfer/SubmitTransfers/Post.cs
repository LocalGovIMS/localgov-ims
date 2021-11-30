using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transfer;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.TransferController;
using Dependencies = Admin.Controllers.TransferControllerDependencies;

namespace Admin.UnitTests.Controllers.Transfer.SubmitTransfers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelCommand<TransferItem>> _mockValidateTransferItemCommand = new Mock<IModelCommand<TransferItem>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "SubmitTransfers")
                .FirstOrDefault();
        }

        private ActionResult GetResult(TransferViewModel model, bool throwException)
        {
            var transferCommand = new Mock<IModelCommand<TransferViewModel>>();

            transferCommand.Setup(x => x.Execute(
                It.IsAny<TransferViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true) { Data = "TestMessage" });

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockTransferViewModelBuilder.Object,
                transferCommand.Object,
                _mockValidateTransferItemCommand.Object);

            var controller = new Controller(dependencies);

            return controller.SubmitTransfers(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsJsonResult()
        {
            var result = GetResult(null, false);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = True, message = TestMessage }");
        }
    }
}
