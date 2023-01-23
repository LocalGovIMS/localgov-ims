using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.OfficeController;
using Dependencies = Admin.Controllers.OfficeControllerDependencies;

namespace Admin.UnitTests.Controllers.Office
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.Office.DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.Office.DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<Models.Office.EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.Office.EditViewModel, string>>();
        protected readonly Mock<IModelBuilder<IList<Models.Office.DetailsViewModel>, string>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.Office.DetailsViewModel>, string>>();
        protected readonly Mock<IModelCommand<Models.Office.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.Office.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();

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
