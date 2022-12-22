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

namespace Admin.UnitTests.Controllers.EReturnTemplate.List
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(EReturnTemplateController);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "List")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var dependencies = new EReturnTemplateControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new EReturnTemplateController(dependencies);

            return controller.List();
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
        public void ReturnsViewResult()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
