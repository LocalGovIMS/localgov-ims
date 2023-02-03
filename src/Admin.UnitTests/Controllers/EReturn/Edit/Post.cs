using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EditViewModel = Admin.Models.EReturn.EditViewModel;

namespace Admin.UnitTests.Controllers.EReturn.Edit
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Edit));
        }

        private ActionResult GetResult(EditViewModel model, bool isModelValid)
        {
            MockEditCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));
            
            MockEditViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new EditViewModel());
            MockEditViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<EditViewModel>())).Returns(new EditViewModel());

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

            return Controller.Edit(model);
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
        public void ReturnsCorrectViewNameIfModelInvalid()
        {
            var result = GetResult(GetEditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(GetEditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditViewModel));
        }

        [TestMethod]
        public void ReturnsCorrectViewNameIfModelIsValid()
        {
            var result = GetResult(GetEditViewModel(), true) as ViewResult;

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
