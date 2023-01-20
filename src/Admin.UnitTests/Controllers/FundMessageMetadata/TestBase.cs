using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FundMessageMetadataController;
using Dependencies = Admin.Controllers.FundMessageMetadataControllerDependencies;
using Admin.Classes.ViewModelBuilders.FundMessageMetadata;

namespace Admin.UnitTests.Controllers.FundMessageMetadata
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.FundMessageMetadata.ListViewModel, Models.FundMessageMetadata.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.FundMessageMetadata.ListViewModel, Models.FundMessageMetadata.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.FundMessageMetadata.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundMessageMetadata.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundMessageMetadata.EditViewModel, CreateViewModelBuilderArgs>> MockCreateViewModelBuilder = new Mock<IModelBuilder<Models.FundMessageMetadata.EditViewModel, CreateViewModelBuilderArgs>>();
        protected readonly Mock<IModelBuilder<Models.FundMessageMetadata.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundMessageMetadata.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.FundMessageMetadata.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.FundMessageMetadata.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.FundMessageMetadata.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.FundMessageMetadata.EditViewModel>>();
        protected readonly Mock<IModelCommand<int>> MockDeleteCommand = new Mock<IModelCommand<int>>();

        protected void SetupController()
        { 
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockCreateViewModelBuilder.Object,
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
