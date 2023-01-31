using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.MethodOfPayment;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.MethodOfPaymentController;
using Dependencies = Admin.Controllers.MethodOfPaymentControllerDependencies;

namespace Admin.UnitTests.Controllers.MethodOfPayment
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, string>>();
        protected readonly Mock<IModelBuilder<IList<DetailsViewModel>, string>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<DetailsViewModel>, string>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockEditCommand = new Mock<IModelCommand<EditViewModel>>();

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
