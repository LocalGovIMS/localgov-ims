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
        private readonly Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, string>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.LookupViewModel>> _mockLookupAccountHolderCommand = new Mock<IModelCommand<Models.AccountHolder.LookupViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(AcceptVerbsAttribute)))
                .Where(x => x.Name == "Search")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchViewModel>>();
            listViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.AccountHolder.SearchViewModel>())).Returns(new Models.AccountHolder.ListViewModel());

            var dependencies = new AccountHolderControllerDependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockLookupAccountHolderCommand.Object);

            var controller = new AccountHolderController(dependencies);

            return controller.Search(new Models.AccountHolder.SearchViewModel());
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
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
