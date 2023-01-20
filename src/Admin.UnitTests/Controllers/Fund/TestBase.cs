using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FundController;
using Dependencies = Admin.Controllers.FundControllerDependencies;

namespace Admin.UnitTests.Controllers.Fund
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.Fund.ListViewModel, Models.Fund.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.Fund.ListViewModel, Models.Fund.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.Fund.DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.Fund.DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<Models.Fund.EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.Fund.EditViewModel, string>>();
        protected readonly Mock<IModelCommand<Models.Fund.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.Fund.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.Fund.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.Fund.EditViewModel>>();

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
