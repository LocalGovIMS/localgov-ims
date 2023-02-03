using Admin.Models.EReturn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.EReturn.Submit
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Submit));
        }

        private ActionResult GetResult(EditViewModel model, bool sucess, bool isModelValid)
        {
            MockEditCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(sucess, "test"));
            MockSubmitCommand.Setup(x => x.Execute(It.IsAny<int>())).Returns(new Admin.Classes.Commands.CommandResult(sucess, "test"));
            
            MockEditViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<EditViewModel>())).Returns(GetEditViewModel());

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

            return Controller.Submit(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void WhenModelStateIsInvalidThenReturnsToSubmitView()
        {
            var result = GetResult(GetEditViewModel(), false, false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Submit");
        }

        [TestMethod]
        public void WhenSubmitIsSuccessfulThenTheUserIsRedirectedToBack()
        {
            var result = GetResult(GetEditViewModel(), true, true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void WhenSubmitIsUnsuccessfulThenTheUserIsRedirectedToEdit()
        {
            var result = GetResult(GetEditViewModel(), false, true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        private EditViewModel GetEditViewModel()
        {
            return new EditViewModel()
            {
                EReturn = new BusinessLogic.Models.EReturnWrapper()
                {
                    EReturn = new BusinessLogic.Entities.EReturn() { Id = 1 }
                }
            };
        }
    }
}

