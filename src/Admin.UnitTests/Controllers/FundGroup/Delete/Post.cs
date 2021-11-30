using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.FundGroupController;

namespace Admin.UnitTests.Controllers.FundGroup.Delete
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.FundGroup.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>> _mockCreateViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>>();
        private readonly Mock<IModelBuilder<IList<Models.FundGroup.DetailsViewModel>, int>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.FundGroup.DetailsViewModel>, int>>();
        private readonly Mock<IModelCommand<Models.FundGroup.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.FundGroup.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.FundGroup.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.FundGroup.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "DeleteFundGroup")
                .FirstOrDefault();
        }

        private ActionResult GetResult(int id, bool isModelValid)
        {
            var deleteCommand = new Mock<IModelCommand<int>>();
            deleteCommand.Setup(x => x.Execute(It.IsAny<int>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new FundGroupControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockCreateViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object,
                deleteCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("userId", "error");
            }

            return controller.DeleteFundGroup(id);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelInvalid()
        {
            var result = GetResult(1, false);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToListIfModelInvalid()
        {
            var result = GetResult(1, false) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "List");
        }

        [TestMethod]
        public void ReturnsNullModelInvalid()
        {
            var result = GetResult(1, false) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(1, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToListIfModelValid()
        {
            var result = GetResult(1, true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "List");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(1, true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
