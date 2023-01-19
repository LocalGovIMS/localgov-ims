using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.CheckDigitConfigurationController;
using Dependencies = Admin.Controllers.CheckDigitConfigurationControllerDependencies;

namespace Admin.UnitTests.Controllers.CheckDigitConfiguration
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.ListViewModel, Models.CheckDigitConfiguration.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.ListViewModel, Models.CheckDigitConfiguration.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>>();
        protected readonly Mock<IModelCommand<int>> MockDeleteCommand = new Mock<IModelCommand<int>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockEditViewModelBuilder.Object,
                    MockListViewModelBuilder.Object,
                    MockCreateCommand.Object,
                    MockEditCommand.Object,
                    MockDeleteCommand.Object);

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
