using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.ImportController;

namespace Admin.UnitTests.Controllers.Import.Search
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : BaseTest
    {
        public Get()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(AcceptVerbsAttribute), nameof(Controller.Search));
        }

        private ActionResult GetResult()
        {
            MockListViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Import.SearchCriteria>())).Returns(new Models.Import.ListViewModel());

            return Controller.Search(new Models.Import.SearchCriteria());
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleAcceptVerbsAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(AcceptVerbsAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "List");
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.Import.ListViewModel));
        }
    }
}
