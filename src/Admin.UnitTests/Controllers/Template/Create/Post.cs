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
    public class Post
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Template.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.Template.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.Template.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();

        private ActionResult GetResult(Models.Template.EditViewModel model, bool success, bool isModelValid)
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.Template.ListViewModel, int>>();
            listViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Template.ListViewModel());

            var createCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();
            if (success)
                createCommand.Setup(x => x.Execute(It.IsAny<Models.Template.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));
            else
                createCommand.Setup(x => x.Execute(It.IsAny<Models.Template.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success, "Test error"));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                createCommand.Object,
                _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("Name", "error");
            }

            return controller.Create(model);
        }

        [TestMethod]
        public void ReturnsviewIfModelInvalid()
        {
            var result = GetResult(new Models.Template.EditViewModel(), true, false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Template.EditViewModel(), true, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelInvalid()
        {
            var result = GetResult(new Models.Template.EditViewModel(), false, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
