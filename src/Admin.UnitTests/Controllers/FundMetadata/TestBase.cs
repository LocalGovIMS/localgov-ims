using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FundMetadataController;
using Dependencies = Admin.Controllers.FundMetadataControllerDependencies;
using Admin.Classes.ViewModelBuilders.FundMetadata;

namespace Admin.UnitTests.Controllers.FundMetadata
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.FundMetadata.ListViewModel, Models.FundMetadata.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.FundMetadata.ListViewModel, Models.FundMetadata.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.FundMetadata.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundMetadata.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundMetadata.EditViewModel, CreateViewModelBuilderArgs>> MockCreateViewModelBuilder = new Mock<IModelBuilder<Models.FundMetadata.EditViewModel, CreateViewModelBuilderArgs>>();
        protected readonly Mock<IModelBuilder<Models.FundMetadata.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundMetadata.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.FundMetadata.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.FundMetadata.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.FundMetadata.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.FundMetadata.EditViewModel>>();
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
