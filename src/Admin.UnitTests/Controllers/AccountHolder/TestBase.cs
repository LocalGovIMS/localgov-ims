using Admin.Classes.ViewModelBuilders.AccountHolder;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.AccountHolder;
using log4net;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.AccountHolderController;
using Dependencies = Admin.Controllers.AccountHolderControllerDependencies;

namespace Admin.UnitTests.Controllers.AccountHolder
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected readonly Mock<IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs>>();
        protected readonly Mock<IModelBuilder<EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, string>>();
        protected readonly Mock<IModelCommand<LookupViewModel>> MockLookupAccountHolderCommand = new Mock<IModelCommand<LookupViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        protected readonly Mock<IModelCommand<EditViewModel>> MockEditCommand = new Mock<IModelCommand<EditViewModel>>();

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
