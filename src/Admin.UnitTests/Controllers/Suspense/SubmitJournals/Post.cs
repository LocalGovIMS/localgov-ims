using Admin.Models.Suspense;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.Suspense.SubmitJournals
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.SubmitJournals));
        }

        private ActionResult GetResult(JournalViewModel model, bool sucess)
        {
            MockJournalCommand.Setup(x => x.Execute(It.IsAny<JournalViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(sucess, "test"));
            
            return Controller.SubmitJournals(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetResult(new JournalViewModel(), true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}

