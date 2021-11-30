using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Controller = Admin.Controllers.TemplateController;
using Dependencies = Admin.Controllers.TemplateControllerDependencies;

namespace Admin.UnitTests.Controllers.Template.Create
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Template.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.Template.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.Template.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.Template.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();

        private ActionResult GetResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.Template.ListViewModel, int>>();
            listViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Template.ListViewModel());

            var dependencies = new Dependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Create();
        }


        [TestMethod]
        public void ReturnsANewView()
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }
    }
}
