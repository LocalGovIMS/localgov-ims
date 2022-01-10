using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.TransactionController;

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
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "UndoTransfer")
                .FirstOrDefault();
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
            Assert.AreEqual("Details", ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
