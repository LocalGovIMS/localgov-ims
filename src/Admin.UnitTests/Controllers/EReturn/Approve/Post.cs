using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EditViewModel = Admin.Models.EReturn.EditViewModel;

namespace Admin.UnitTests.Controllers.EReturn.Approve
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post : TestBase
    {
        public Post()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Approve));
        }

        private ActionResult GetResult()
        {
            MockEditViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new EditViewModel());
            MockEditViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<EditViewModel>())).Returns(new EditViewModel());

            return Controller.Approve(new EditViewModel() { EReturn = new BusinessLogic.Models.EReturnWrapper() { EReturn = new BusinessLogic.Entities.EReturn() { Id = 1 } } });
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsApproveViewIfEditCommandFails()
        {
            // Arrange
            MockApproveCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            MockEditCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            // Act
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Approve");
        }

        [TestMethod]
        public void RedirectToBackIfApproveCommandIsSuccessful()
        {
            // Arrange
            MockApproveCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            MockEditCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true));

            // Act
            var result = GetResult() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Back");
        }
    }
}
