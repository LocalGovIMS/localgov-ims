using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FundMessageController;
using Dependencies = Admin.Controllers.FundMessageControllerDependencies;

namespace Admin.UnitTests.Controllers.FundMessage
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.FundMessage.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundMessage.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundMessage.ListViewModel, Models.FundMessage.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.ListViewModel, Models.FundMessage.SearchCriteria>>();
        protected readonly Mock<IModelCommand<Models.FundMessage.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.FundMessage.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.FundMessage.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.FundMessage.EditViewModel>>();

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
