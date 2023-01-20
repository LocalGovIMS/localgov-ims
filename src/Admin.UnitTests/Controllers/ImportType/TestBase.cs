using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.ImportTypeController;
using Dependencies = Admin.Controllers.ImportTypeControllerDependencies;

namespace Admin.UnitTests.Controllers.ImportType
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.ImportType.ListViewModel, Models.ImportType.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.ImportType.ListViewModel, Models.ImportType.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.ImportType.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.ImportType.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.ImportType.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.ImportType.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.ImportType.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.ImportType.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.ImportType.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.ImportType.EditViewModel>>();

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
