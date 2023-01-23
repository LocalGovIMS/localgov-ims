using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Controller = Admin.Controllers.VatController;
using Dependencies = Admin.Controllers.VatControllerDependencies;

namespace Admin.UnitTests.Controllers.Vat
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.Vat.DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.Vat.DetailsViewModel, string>>();
        protected readonly Mock<IModelBuilder<Models.Vat.EditViewModel, string>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.Vat.EditViewModel, string>>();
        protected readonly Mock<IModelBuilder<IList<Models.Vat.DetailsViewModel>, string>> MockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.Vat.DetailsViewModel>, string>>();
        protected readonly Mock<IModelCommand<Models.Vat.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.Vat.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.Vat.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.Vat.EditViewModel>>();

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
