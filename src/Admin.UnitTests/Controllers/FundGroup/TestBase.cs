using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.FundGroupController;
using Dependencies = Admin.Controllers.FundGroupControllerDependencies;
using System.Collections.Generic;

namespace Admin.UnitTests.Controllers.FundGroup
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.FundGroup.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>> MockCreateViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundGroup.EditViewModel, int>>();
        protected readonly Mock<IModelBuilder<IList<Models.FundGroup.DetailsViewModel>, int>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.FundGroup.DetailsViewModel>, int>>();
        protected readonly Mock<IModelCommand<Models.FundGroup.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.FundGroup.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.FundGroup.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.FundGroup.EditViewModel>>();
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
