using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturn;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.EReturnController;
using Dependencies = Admin.Controllers.EReturnControllerDependencies;

namespace Admin.UnitTests.Controllers.EReturn
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();

        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected readonly Mock<IModelBuilder<CreateViewModel, int>> MockCreateViewModelBuilder = new Mock<IModelBuilder<CreateViewModel, int>>();
        protected readonly Mock<IModelBuilder<EditViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        protected readonly Mock<IModelCommand<CreateViewModel>> MockCreateCommand = new Mock<IModelCommand<CreateViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockApproveCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<int>> MockDeleteCommand = new Mock<IModelCommand<int>>();
        protected readonly Mock<IModelCommand<int>> MockSubmitCommand = new Mock<IModelCommand<int>>();
        protected readonly Mock<IModelCommand<ApproveViewModel>> MockAuthoriseCommand = new Mock<IModelCommand<ApproveViewModel>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockListViewModelBuilder.Object,
                    MockCreateViewModelBuilder.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockEditViewModelBuilder.Object,
                    MockCreateCommand.Object,
                    MockEditCommand.Object,
                    MockApproveCommand.Object,
                    MockDeleteCommand.Object,
                    MockSubmitCommand.Object,
                    MockAuthoriseCommand.Object);

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
