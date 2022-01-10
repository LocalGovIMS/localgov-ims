using Admin.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.TransactionController;

namespace Admin.UnitTests.Controllers.Transaction.Refund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "_Refund")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            MockRefundViewModelBuilder.Setup(x => x.Build(
                It.IsAny<string>()))
                .Returns(new RefundViewModel()
                {
                    Reference = "Reference",
                    RefundItems = null,
                    RefundReason = string.Empty
                }
                );

            return Controller._Refund("test");
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_Refund");
        }
    }
}
