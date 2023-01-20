﻿using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Moq;
using System.Linq;
using System.Reflection;
using System;
using Controller = Admin.Controllers.EReturnTemplateRowController;
using Dependencies = Admin.Controllers.EReturnTemplateRowControllerDependencies;
using Admin.Classes.ViewModelBuilders.EReturnTemplateRow;

namespace Admin.UnitTests.Controllers.EReturnTemplateRow
{
    public class TestBase
    {
        protected Controller Controller;

        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplateRow.ListViewModel, Models.EReturnTemplateRow.SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplateRow.ListViewModel, Models.EReturnTemplateRow.SearchCriteria>>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplateRow.DetailsViewModel, int>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplateRow.DetailsViewModel, int>>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplateRow.EditViewModel, CreateViewModelBuilderArgs>> MockCreateViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplateRow.EditViewModel, CreateViewModelBuilderArgs>>();
        protected readonly Mock<IModelBuilder<Models.EReturnTemplateRow.EditViewModel, int>> MockEditViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplateRow.EditViewModel, int>>();
        protected readonly Mock<IModelCommand<Models.EReturnTemplateRow.EditViewModel>> MockCreateCommand = new Mock<IModelCommand<Models.EReturnTemplateRow.EditViewModel>>();
        protected readonly Mock<IModelCommand<Models.EReturnTemplateRow.EditViewModel>> MockEditCommand = new Mock<IModelCommand<Models.EReturnTemplateRow.EditViewModel>>();
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