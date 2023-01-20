using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.MethodOfPaymentController;
using Dependencies = Admin.Controllers.MethodOfPaymentControllerDependencies;
using System.Collections.Generic;

namespace Admin.UnitTests.Controllers.MethodOfPayment
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.MethodOfPayment.DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.MethodOfPayment.DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<Models.MethodOfPayment.EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.MethodOfPayment.EditViewModel, string>>();
        protected readonly Mock<IModelBuilder<IList<Models.MethodOfPayment.DetailsViewModel>, string>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.MethodOfPayment.DetailsViewModel>, string>>();
        protected readonly Mock<IModelCommand<Models.MethodOfPayment.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.MethodOfPayment.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.MethodOfPayment.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.MethodOfPayment.EditViewModel>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockEditViewModelBuilder.Object,
                    MockListViewModelBuilder.Object,
                    MockCreateCommand.Object,
                    MockEditCommand.Object);

            Controller = new Controller(dependencies);
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
