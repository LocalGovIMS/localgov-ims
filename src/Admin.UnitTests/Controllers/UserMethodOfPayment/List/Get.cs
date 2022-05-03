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
using Controller = Admin.Controllers.UserMethodOfPaymentController;
using Dependencies = Admin.Controllers.UserMethodOfPaymentControllerDependencies;

namespace Admin.UnitTests.Controllers.UserMethodOfPayment.List
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.UserMethodOfPayment.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.UserMethodOfPayment.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.UserMethodOfPayment.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.UserMethodOfPayment.EditViewModel>>();

        private MethodInfo GetListMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "_ListForUser")
                .FirstOrDefault();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        private ActionResult GetTestListResult()
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
        public void ReturnsAPartialView()
        {
            var result = GetTestListResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetTestListResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_BasicList");
        }
    }
}
