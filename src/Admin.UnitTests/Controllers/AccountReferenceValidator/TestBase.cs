﻿using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.AccountReferenceValidatorController;
using Dependencies = Admin.Controllers.AccountReferenceValidatorControllerDependencies;

namespace Admin.UnitTests.Controllers.AccountReferenceValidator
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.AccountReferenceValidator.ListViewModel, Models.AccountReferenceValidator.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.ListViewModel, Models.AccountReferenceValidator.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.AccountReferenceValidator.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.AccountReferenceValidator.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>>();
        protected readonly Mock<IModelCommand<int>> MockDeleteCommand = new Mock<IModelCommand<int>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockDetailsViewModelBuilder.Object,
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
