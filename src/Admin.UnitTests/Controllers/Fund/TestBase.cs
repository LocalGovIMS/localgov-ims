using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Fund;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.FundController;
using Dependencies = Admin.Controllers.FundControllerDependencies;

namespace Admin.UnitTests.Controllers.Fund
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, string>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockEditCommand = new Mock<IModelCommand<EditViewModel>>();

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
