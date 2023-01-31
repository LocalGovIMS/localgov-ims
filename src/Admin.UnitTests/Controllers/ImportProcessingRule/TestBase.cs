using Admin.Classes.Commands.ImportProcessingRule;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.ImportProcessingRule;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.ImportProcessingRuleController;
using Dependencies = Admin.Controllers.ImportProcessingRuleControllerDependencies;


namespace Admin.UnitTests.Controllers.ImportProcessingRule
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<ChangeStatusCommandArgs>> MockChangeStatusCommand = new Mock<IModelCommand<ChangeStatusCommandArgs>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockEditViewModelBuilder.Object,
                    MockListViewModelBuilder.Object,
                    MockCreateCommand.Object,
                    MockEditCommand.Object,
                    MockChangeStatusCommand.Object);

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
