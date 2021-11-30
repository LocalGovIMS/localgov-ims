using Admin.Classes.Dependencies;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ControllerDependencies.Fakes;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.UserRole.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private Type _controller = typeof(UserRoleController);
        private UserRoleController _controllerInstance = new UserRoleController(new StubIUserRoleControllerDependencies());
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>> _mockBasicListViewModelBuilder = new Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.UserRole.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.UserRole.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.UserRole.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.UserRole.EditViewModel>>();

        private MethodInfo GetEditMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetTestEditResult(Models.UserRole.EditViewModel model, bool isModelValid)
        {
            var editCommand = new Mock<IModelCommand<Models.UserRole.EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<Models.UserRole.EditViewModel>())).Returns(new Classes.Commands.CommandResult(true));

            var dependencies = new UserRoleControllerDependencies(
                _mockLogger.Object,
                _mockBasicListViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                editCommand.Object);

            var controller = new UserRoleController(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("userId", "error");
            }

            return controller.Edit(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetEditMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetEditMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }
        
        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetTestEditResult(new Models.UserRole.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetTestEditResult(new Models.UserRole.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.UserRole.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetTestEditResult(new Models.UserRole.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetTestEditResult(new Models.UserRole.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }
        
        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetTestEditResult(new Models.UserRole.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
