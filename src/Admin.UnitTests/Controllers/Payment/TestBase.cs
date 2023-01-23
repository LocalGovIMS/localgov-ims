using Admin.Classes.Models;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Payment;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;
using Dependencies = Admin.Controllers.PaymentControllerDependencies;

namespace Admin.UnitTests.Controllers.Payment
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<IndexViewModel, IndexViewModel>> MockIndexViewModelBuilder = new Mock<IModelBuilder<IndexViewModel, IndexViewModel>>();
        protected readonly Mock<IModelCommand<IndexViewModel>> MockAddCommand = new Mock<IModelCommand<IndexViewModel>>();
        protected readonly Mock<IModelCommand<Guid>> MockRemoveCommand = new Mock<IModelCommand<Guid>>();
        protected readonly Mock<IModelCommand<string>> MockEmptyBasketCommand = new Mock<IModelCommand<string>>();
        protected readonly Mock<IModelCommand<IndexViewModel>> MockCheckAddressCommand = new Mock<IModelCommand<IndexViewModel>>();
        protected readonly Mock<IModelCommand<IndexViewModel>> MockCreatePaymentsCommand = new Mock<IModelCommand<IndexViewModel>>();
        protected readonly Mock<IModelCommand<IndexViewModel>> MockSetAddressCommand = new Mock<IModelCommand<IndexViewModel>>();
        protected readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> MockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                MockLogger.Object,
                MockIndexViewModelBuilder.Object,
                MockAddCommand.Object,
                MockRemoveCommand.Object,
                MockEmptyBasketCommand.Object,
                MockCheckAddressCommand.Object,
                MockCreatePaymentsCommand.Object,
                MockSetAddressCommand.Object,
                MockProcessPaymentCommand.Object);

            Controller = new Controller(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(new IndexViewModel());

            Controller.ControllerContext = controllerContext.Object;
        }

        protected MethodInfo GetMethod(Type attributeType, string name)
        {
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == attributeType))
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}
