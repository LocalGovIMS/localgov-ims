using Admin.Classes.Models;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Fail
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>> _mockIndexViewModelBuilder = new Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockAddCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockRemoveCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<string>> _mockEmptyBasketCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCheckAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCreatePaymentsCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockSetAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();

        private ActionResult GetResult(bool vaildSession)
        {
            var dependencies = new PaymentControllerDependencies(
                _mockLogger.Object,
                _mockIndexViewModelBuilder.Object,
                _mockAddCommand.Object,
                _mockRemoveCommand.Object,
                _mockEmptyBasketCommand.Object,
                _mockCheckAddressCommand.Object,
                _mockCreatePaymentsCommand.Object,
                _mockSetAddressCommand.Object,
                _mockProcessPaymentCommand.Object);

            var controller = new Controller(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(vaildSession ? new Models.Payment.IndexViewModel() : null);

            controller.ControllerContext = controllerContext.Object;

            return controller.Fail();
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResult()
        {
            var result = GetResult(true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToCreate()
        {
            var result = GetResult(true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Create");
        }

    }
}