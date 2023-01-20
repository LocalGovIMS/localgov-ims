using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.ImportProcessingRuleImportType.Delete
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
            return GetMethod(typeof(HttpGetAttribute), nameof(Controller.Delete));
        }

        private ActionResult GetResult()
        {
            MockDeleteCommand.Setup(x => x.Execute(It.IsAny<int>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            return Controller.Delete(1, 1);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsARedirectAction()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToTheCorrectController()
        {
            var result = GetResult() as RedirectToRouteResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.RouteValues["controller"], "ImportProcessingRule");
        }

        [TestMethod]
        public void RedirectsToTheCorrectAction()
        {
            var result = GetResult() as RedirectToRouteResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.RouteValues["action"], "Edit");
        }
    }
}
