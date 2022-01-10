using Admin.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.TransactionController;

namespace Admin.UnitTests.Controllers.Transaction.Transfer
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
                .Where(x => x.Name == "_Transfer")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            MockTransferViewModelBuilder.Setup(x => x.Build(
                It.IsAny<string>()))
                .Returns(new TransferViewModel()
                {
                    Funds = null,
                    PspReference = "PspReference",
                    TransactionReference = "TransactionReference",
                    TransferItem = null,
                    TransferItems = null
                }
            );

            return Controller._Transfer("test");
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
        public void ReturnsAView()
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_Transfer");
        }

        [TestMethod]
        public void ReturnsAViewModel()
        {
            var result = GetResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType()
        {
            var result = GetResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(TransferViewModel));
        }
    }
}
