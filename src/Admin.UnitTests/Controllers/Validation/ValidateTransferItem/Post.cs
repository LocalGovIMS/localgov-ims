using Admin.Interfaces.Commands;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.ValidationController;
using Dependencies = Admin.Controllers.ValidationControllerDependencies;

namespace Admin.UnitTests.Controllers.Validation.ValidateTransferItem
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "ValidateTransferItem")
                .FirstOrDefault();
        }

        private ActionResult GetResult(TransferItem model, bool success)
        {
            var validateTransferItemCommand = new Mock<IModelCommand<TransferItem>>();

            validateTransferItemCommand.Setup(x => x.Execute(
                It.IsAny<TransferItem>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success, "TestMessage"));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                validateTransferItemCommand.Object);

            var controller = new Controller(dependencies);

            return controller.ValidateTransferItem(model);
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
        public void SucccessJsonResult()
        {
            var result = GetResult(null, true);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = True, message = TestMessage }");
        }

        [TestMethod]
        public void FailureJsonResult()
        {
            var result = GetResult(null, false);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = False, message = TestMessage }");
        }
    }
}
