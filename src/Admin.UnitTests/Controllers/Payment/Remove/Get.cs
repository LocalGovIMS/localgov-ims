﻿using Admin.Classes.Models;
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

namespace Admin.UnitTests.Controllers.Payment.Remove
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockAddCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockEmptyBasketCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCheckAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<Models.Payment.IndexViewModel>> _mockCreatePaymentsCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "RemoveItem")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var indexViewModelBuilder = new Mock<IModelBuilder<Models.Payment.IndexViewModel, Models.Payment.IndexViewModel>>();
            indexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());

            var RemoveCommand = new Mock<IModelCommand<string>>();
            RemoveCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var SetAddressCommand = new Mock<IModelCommand<Models.Payment.IndexViewModel>>();
            SetAddressCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new PaymentControllerDependencies(
                _mockLogger.Object,
                indexViewModelBuilder.Object,
                _mockAddCommand.Object,
                RemoveCommand.Object,
                _mockEmptyBasketCommand.Object,
                _mockCheckAddressCommand.Object,
                _mockCreatePaymentsCommand.Object,
                SetAddressCommand.Object,
                _mockProcessPaymentCommand.Object);

            var controller = new Controller(dependencies);

            return controller.RemoveItem("123");
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsANewView()
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.IndexViewModel));
        }
    }
}
