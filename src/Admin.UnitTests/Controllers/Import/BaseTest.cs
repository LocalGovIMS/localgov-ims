using Admin.Interfaces.ModelBuilders;
using Admin.Models.Import;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.ImportController;
using Dependencies = Admin.Controllers.ImportControllerDependencies;

namespace Admin.UnitTests.Controllers.Import
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

        protected MethodInfo GetMethod(Type attributeType, string name)
        {
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == attributeType))
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }
    }
}
