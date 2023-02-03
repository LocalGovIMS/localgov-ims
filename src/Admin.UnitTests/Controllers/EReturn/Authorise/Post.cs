using Admin.Models.EReturn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.EReturn.Authorise
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Authorise));
        }

        private ActionResult GetResult(ApproveViewModel model, bool sucess)
        {
            MockAuthoriseCommand.Setup(x => x.Execute(It.IsAny<ApproveViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(sucess, "test"));
            
            return Controller.Authorise(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetResult(new ApproveViewModel(), true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}

