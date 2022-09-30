using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.ImportProcessingRuleActionController;

namespace Admin.UnitTests.Controllers.MetadataKey.Delete
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : BaseTest
    {
        private readonly Type _controller = typeof(Controller);

        [TestInitialize]
        public void Initialise()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Delete")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            MockDeleteCommand.Setup(x => x.Execute(It.IsAny<int>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            return Controller.Delete(1);
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

            Assert.AreEqual(result.RouteValues["controller"], "MetadataKey");
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
