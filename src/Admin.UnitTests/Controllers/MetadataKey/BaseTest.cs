using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Import;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Controller = Admin.Controllers.MetadataKeyController;
using Dependencies = Admin.Controllers.MetadataKeyControllerDependencies;

namespace Admin.UnitTests.Controllers.MetadataKey
{
    public class BaseTest
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.MetadataKey.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.MetadataKey.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.MetadataKey.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.MetadataKey.EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.MetadataKey.ListViewModel, Models.MetadataKey.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.MetadataKey.ListViewModel, Models.MetadataKey.SearchCriteria>>();
        protected readonly Mock<IModelCommand<Models.MetadataKey.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.MetadataKey.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.MetadataKey.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.MetadataKey.EditViewModel>>();
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
    }
}
