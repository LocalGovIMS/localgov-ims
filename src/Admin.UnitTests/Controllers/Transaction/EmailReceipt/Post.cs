using Admin.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.TransactionController;

namespace Admin.UnitTests.Controllers.Transaction.EmailReceipt
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
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "EmailReceipt")
                .FirstOrDefault();
        }

        private ActionResult GetResult(EmailReceiptViewModel model, bool success)
        {
            MockEmailReceiptCommand.Setup(x => x.Execute(
                It.IsAny<EmailReceiptViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success, "TestMessage"));

            return Controller.EmailReceipt(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void SucccessJsonResult()
        {
            var result = GetResult(null, true);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = True }");
        }

        [TestMethod]
        public void FailureJsonResult()
        {
            var result = GetResult(null, false);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = False }");
        }
    }
}
