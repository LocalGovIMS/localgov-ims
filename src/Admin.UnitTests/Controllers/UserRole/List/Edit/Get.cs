using Admin.Classes.Attributes.Navigation;
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
    public class Get
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
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetTestEditResult()
        {
            var editViewModelBuilder = new Mock<IModelBuilder<Models.UserRole.EditViewModel, int>>();
            editViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.UserRole.EditViewModel());

            var dependencies = new UserRoleControllerDependencies(
                _mockLogger.Object,
                _mockBasicListViewModelBuilder.Object,
                editViewModelBuilder.Object,
                _mockEditCommand.Object);

            var controller = new UserRoleController(dependencies);

            return controller.Edit(1);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetEditMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetEditMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void NavigatablePageActionFilterAttributeHasCorrectDisplayText()
        {
            var attribute = GetEditMethod().CustomAttributes.Single(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute));

            var namedArgument = attribute.NamedArguments.Where(x => x.MemberName == "DisplayText").First();

            Assert.AreEqual("User Roles", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetEditMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
        {
            var result = GetTestEditResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetTestEditResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsAViewModel()
        {
            var result = GetTestEditResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType()
        {
            var result = GetTestEditResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.UserRole.EditViewModel));
        }
    }
}
