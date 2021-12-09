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

namespace Admin.UnitTests.Controllers.AccountHolder.Search
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(AccountHolderController);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.LookupViewModel>> _mockLookupAccountHolderCommand = new Mock<IModelCommand<Models.AccountHolder.LookupViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(AcceptVerbsAttribute)))
                .Where(x => x.Name == "Search")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>>();
            listViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.AccountHolder.SearchCriteria>())).Returns(new Models.AccountHolder.ListViewModel());

            var dependencies = new AccountHolderControllerDependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockLookupAccountHolderCommand.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new AccountHolderController(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["AccountHolderController::IsAPaymentSearch"]).Returns(false);

            controller.ControllerContext = controllerContext.Object;

            return controller.Search(new Models.AccountHolder.SearchCriteria());
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleAcceptVerbsAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(AcceptVerbsAttribute)).Count());
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "List");
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.AccountHolder.ListViewModel));
        }
    }
}
