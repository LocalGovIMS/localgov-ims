using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Controllers;
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
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.AccountHolderController;

namespace Admin.UnitTests.Controllers.AccountHolder.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.LookupViewModel>> _mockLookupAccountHolderCommand = new Mock<IModelCommand<Models.AccountHolder.LookupViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Create")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var editViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>>();
            editViewModelBuilder.Setup(x => x.Build(It.IsAny<string>())).Returns(new Models.AccountHolder.EditViewModel());

            var dependencies = new AccountHolderControllerDependencies(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                editViewModelBuilder.Object,
                _mockLookupAccountHolderCommand.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Edit("Test");
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void NavigatablePageActionFilterAttributeHasCorrectDisplayText()
        {
            var attribute = GetMethod().CustomAttributes.Single(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute));

            var namedArgument = attribute.NamedArguments.Where(x => x.MemberName == "DisplayText").First();

            Assert.AreEqual("Create Account Holder", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsAViewModel()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.AccountHolder.EditViewModel));
        }
    }
}
