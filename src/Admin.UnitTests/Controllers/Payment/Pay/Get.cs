using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Pay
{
    [TestClass]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(HttpGetAttribute), "Pay");
        }
        
        private ActionResult GetResult(bool isSuccess, bool vaildSession)
        {
            MockIndexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());
            MockEmptyBasketCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess));
            MockCheckAddressCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess) { Data = isSuccess });
            MockCreatePaymentsCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess) { Data = "MockTest" });

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(vaildSession ? new Models.Payment.IndexViewModel() : null);

            Controller.ControllerContext = controllerContext.Object;

            return Controller.Pay();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectToAddress()
        {
            var result = GetResult(true, true) as RedirectToRouteResult;

            Assert.IsNotNull(result);

            var s = result.RouteValues.Values.First();
            Assert.IsTrue(s.ToString() == "Address");
        }


        [TestMethod]
        public void ReturnsRedirectToIndex()
        {
            var result = GetResult(false, false) as RedirectToRouteResult;

            Assert.IsNotNull(result);

            var s = result.RouteValues.Values.First();
            Assert.IsTrue(s.ToString() == "Index");
        }

        [TestMethod]
        public void ReturnsRedirectToURL()
        {
            var result = GetResult(false, true) as RedirectResult;

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Url == "MockTest");
        }
    }
}