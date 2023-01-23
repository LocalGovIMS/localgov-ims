using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.Transaction.UndoTransfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(HttpGetAttribute), nameof(Controller.UndoTransfer));
        }

        private ActionResult GetResult(bool success)
        {
            MockUndoTransferCommand.Setup(x => x.Execute(
                It.IsAny<string>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success)
            );
            
            return Controller.UndoTransfer("test", "test");
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectResult()
        {
            var result = GetResult(true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToSearch()
        {
            var result = GetResult(true);

            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(Controller.Details), ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
