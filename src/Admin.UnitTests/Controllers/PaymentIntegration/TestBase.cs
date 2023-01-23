using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.PaymentIntegrationController;
using Dependencies = Admin.Controllers.PaymentIntegrationControllerDependencies;

namespace Admin.UnitTests.Controllers.PaymentIntegration
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.PaymentIntegration.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.PaymentIntegration.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.PaymentIntegration.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.PaymentIntegration.EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<IList<Models.PaymentIntegration.DetailsViewModel>, int>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.PaymentIntegration.DetailsViewModel>, int>>();
        protected readonly Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>>();

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
