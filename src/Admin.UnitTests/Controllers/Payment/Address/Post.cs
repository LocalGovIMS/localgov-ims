using Admin.Classes.Models;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Address
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockAddCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockRemoveCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCheckAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCreatePaymentsCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockSetAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Address")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.Payment.Address model, bool isModelValid)
        {
            var indexViewModelBuilder = new Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>>();
            indexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());

            var EmptyBasketCommand = new Mock<IModelCommand<string>>();
            EmptyBasketCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new PaymentControllerDependencies(
                _mockLogger.Object,
                indexViewModelBuilder.Object,
                _mockAddCommand.Object,
                _mockRemoveCommand.Object,
                EmptyBasketCommand.Object,
                _mockCheckAddressCommand.Object,
                _mockCreatePaymentsCommand.Object,
                _mockSetAddressCommand.Object,
                _mockProcessPaymentCommand.Object);

            var controller = new Controller(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(new Models.Payment.IndexViewModel());

            controller.ControllerContext = controllerContext.Object;

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("userId", "error");
            }

            return controller.Address(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Address(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Address(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.Address));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Pay");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
