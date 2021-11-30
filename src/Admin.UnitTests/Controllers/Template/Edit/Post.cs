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
    public class Post
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<Models.Template.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();

        private ActionResult GetResult(Models.Template.EditViewModel model, bool success, bool isModelValid)
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.Template.ListViewModel, int>>();
            listViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Template.ListViewModel());

            var editViewModelBuilder = new Mock<IModelBuilder<Models.Template.EditViewModel, int>>();
            editViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<Models.Template.EditViewModel>())).Returns(new Models.Template.EditViewModel());

            var editCommand = new Mock<IModelCommand<Models.Template.EditViewModel>>();
            if (success)
                editCommand.Setup(x => x.Execute(It.IsAny<Models.Template.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));
            else
                editCommand.Setup(x => x.Execute(It.IsAny<Models.Template.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success, "Test error"));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                editViewModelBuilder.Object,
                _mockCreateCommand.Object,
                editCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("Name", "error");
            }

            return controller.Edit(model);
        }


        [TestMethod]
        public void ReturnsviewIfModelInvalid()
        {
            var controller = new Models.Template.EditViewModel
            {
                TemplateRows = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                {
                    new BusinessLogic.Entities.TemplateRow()
                    {
                        Reference="123"
                    }
                }
            };

            var result = GetResult(controller, true, true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var controller = new Models.Template.EditViewModel
            {
                TemplateRows = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                {
                    new BusinessLogic.Entities.TemplateRow()
                    {
                        Reference="01234567891"
                    }
                }
            };

            var result = GetResult(controller, true, true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelInvalid()
        {
            var controller = new Models.Template.EditViewModel
            {
                TemplateRows = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                {
                    new BusinessLogic.Entities.TemplateRow()
                    {
                        Reference="0123456789*"
                    }
                }
            };

            var result = GetResult(controller, true, true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }


        [TestMethod]
        public void ReturnsviewIfModelStateInvalid()
        {
            var controller = new Models.Template.EditViewModel
            {
                TemplateRows = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                {
                    new BusinessLogic.Entities.TemplateRow()
                    {
                        Reference="01234567893"
                    }
                }
            };

            var result = GetResult(controller, true, false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }


        [TestMethod]
        public void ReturnsviewIfResultInvalid()
        {
            var controller = new Models.Template.EditViewModel
            {
                TemplateRows = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                {
                    new BusinessLogic.Entities.TemplateRow()
                    {
                        Reference="01234567893"
                    }
                }
            };

            var result = GetResult(controller, false, true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Template.EditViewModel));
        }
    }
}
