using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Controller = Admin.Controllers.TemplateController;
using Dependencies = Admin.Controllers.TemplateControllerDependencies;

namespace Admin.UnitTests.Controllers.Template.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Template.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.Template.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.Template.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();

        private ActionResult GetResult(bool useSession)
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.Template.ListViewModel, int>>();
            listViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Template.ListViewModel());

            var editCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<Models.Template.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(useSession));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockCreateCommand.Object,
                editCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Edit(1);
        }

        [TestMethod]
        public void ReturnsANewView()
        {
            var result = GetResult(true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
