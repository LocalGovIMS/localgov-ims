using Admin.Classes.Models;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Pay
{
    [TestClass]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockAddCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockRemoveCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockSetAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();


        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Pay")
                .FirstOrDefault();
        }

        private ActionResult GetResult(bool isSuccess, bool vaildSession)
        {
            var indexViewModelBuilder = new Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>>();
            indexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());

            var EmptyBasketCommand = new Mock<IModelCommand<string>>();
            EmptyBasketCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess));

            var CheckAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
            CheckAddressCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess)
            { Data = isSuccess });

            var CreatePaymentsCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
            CreatePaymentsCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(isSuccess)
            { Data = "MockTest" });

            var dependencies = new PaymentControllerDependencies(
                _mockLogger.Object,
                indexViewModelBuilder.Object,
                _mockAddCommand.Object,
                _mockRemoveCommand.Object,
                EmptyBasketCommand.Object,
                CheckAddressCommand.Object,
                CreatePaymentsCommand.Object,
                _mockSetAddressCommand.Object,
                _mockProcessPaymentCommand.Object);

            var controller = new Controller(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(vaildSession ? new Models.Payment.IndexViewModel() : null);

            controller.ControllerContext = controllerContext.Object;


            return controller.Pay();
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