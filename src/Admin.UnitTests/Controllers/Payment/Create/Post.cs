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

namespace Admin.UnitTests.Controllers.Payment.Create
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<string>> _mockRemoveCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<string>> _mockEmptyBasketCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCheckAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCreatePaymentsCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();


        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Create")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.Payment.IndexViewModel model, bool success)
        {
            var indexViewModelBuilder = new Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>>();
            indexViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Payment.IndexViewModel());

            var addCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
            addCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));

            var SetAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
            SetAddressCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));

            var dependencies = new PaymentControllerDependencies(
                _mockLogger.Object,
                indexViewModelBuilder.Object,
                addCommand.Object,
                _mockRemoveCommand.Object,
                _mockEmptyBasketCommand.Object,
                _mockCheckAddressCommand.Object,
                _mockCreatePaymentsCommand.Object,
                SetAddressCommand.Object,
                _mockProcessPaymentCommand.Object);

            var controller = new Controller(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(null);

            controller.ControllerContext = controllerContext.Object;

            return controller.Create(model);
        }


        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.IndexViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Create");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
