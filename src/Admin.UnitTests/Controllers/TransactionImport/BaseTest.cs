using Admin.Interfaces.ModelBuilders;
using Admin.Models.TransactionImport;
using log4net;
using Moq;
using Controller = Admin.Controllers.TransactionImportController;
using Dependencies = Admin.Controllers.TransactionImportControllerDependencies;

namespace Admin.UnitTests.Controllers.TransactionImport
{
    public class BaseTest
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockListViewModelBuilder.Object                    
                    );

            Controller = new Controller(dependencies);
        }
    }
}
