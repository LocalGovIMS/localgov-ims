using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.EReturnTemplateController;
using Dependencies = Admin.Controllers.EReturnTemplateControllerDependencies;

namespace Admin.UnitTests.Controllers.EReturnTemplate
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();

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
