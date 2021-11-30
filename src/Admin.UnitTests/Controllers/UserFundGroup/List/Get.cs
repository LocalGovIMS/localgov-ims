using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.UserFundGroupController;
using Dependencies = Admin.Controllers.UserFundGroupControllerDependencies;

namespace Admin.UnitTests.Controllers.UserFundGroup.List
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.UserFundGroup.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.UserFundGroup.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.UserFundGroup.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.UserFundGroup.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "_ListForUser")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var basicListViewModelBuilder = new Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>>();
            basicListViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.Shared.BasicListViewModel());

            var dependencies = new Dependencies(
                _mockLogger.Object,
                basicListViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            return controller._ListForUser(1);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_BasicList");
        }
    }
}
