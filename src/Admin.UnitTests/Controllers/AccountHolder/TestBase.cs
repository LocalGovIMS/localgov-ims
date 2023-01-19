using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.AccountHolderController;
using Dependencies = Admin.Controllers.AccountHolderControllerDependencies;

namespace Admin.UnitTests.Controllers.AccountHolder
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>>();
        protected readonly Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>>();
        protected readonly Mock<IModelCommand<Models.AccountHolder.LookupViewModel>> MockLookupAccountHolderCommand = new Mock<IModelCommand<Models.AccountHolder.LookupViewModel>>();
        protected readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockListViewModelBuilder.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockEditViewModelBuilder.Object,
                    MockLookupAccountHolderCommand.Object,
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
