using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.ImportController;

namespace Admin.UnitTests.Controllers.Import.Details
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
            return GetMethod(typeof(HttpGetAttribute), nameof(Controller.Details));
        }

        private ActionResult GetResult()
        {
            MockDetailsViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.Import.DetailsViewModel());

            return Controller.Details(1);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void NavigatablePageActionFilterAttributeHasCorrectDisplayText()
        {
            var attribute = GetMethod().CustomAttributes.Single(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute));

            var namedArgument = attribute.NamedArguments.Where(x => x.MemberName == "DisplayText").First();

            Assert.AreEqual("Import Details", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsViewResult()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
