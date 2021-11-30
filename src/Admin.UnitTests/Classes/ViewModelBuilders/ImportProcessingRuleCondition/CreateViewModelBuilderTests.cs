using Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRuleCondition.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition.CreateViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleCondition

{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleConditionService> _mockImportProcessingRuleConditionService = new Mock<IImportProcessingRuleConditionService>();
        private readonly Mock<IImportProcessingRuleOperatorService> _mockImportProcessingRuleOperatorService = new Mock<IImportProcessingRuleOperatorService>();
        private readonly Mock<IImportProcessingRuleFieldService> _mockImportProcessingRuleFieldService = new Mock<IImportProcessingRuleFieldService>();

        private void SetupServices()
        {
            _mockImportProcessingRuleConditionService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.ImportProcessingRuleCondition.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.ImportProcessingRuleCondition>()
                {
                    Items = new List<BusinessLogic.Entities.ImportProcessingRuleCondition>()
                    {
                        new BusinessLogic.Entities.ImportProcessingRuleCondition()
                        {
                            Id = 1,
                            Group = 1,
                            ImportProcessingRuleId = 1,
                            ImportProcessingRuleFieldId = 1,
                            ImportProcessingRuleOperatorId = 1,
                            Value = "Test value",
                            Field = new BusinessLogic.Entities.ImportProcessingRuleField()
                            {
                                DisplayName = "Test display name"
                            }
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
                });

            _mockImportProcessingRuleFieldService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.ImportProcessingRuleField>()
                {
                    new BusinessLogic.Entities.ImportProcessingRuleField()
                    {
                        Id = 1,
                        Name = "TestField",
                        DisplayName = "Test Field",
                        DisplayOrder = 1,
                        Type = "Text"
                    }
                });

            _mockImportProcessingRuleOperatorService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.ImportProcessingRuleOperator>()
                {
                    new BusinessLogic.Entities.ImportProcessingRuleOperator()
                    {
                        Id = 1,
                        Name = "TestOperator",
                        DisplayName = "Test Operator",
                        DisplayOrder = 1,
                        Type = "Text"
                    }
                });
        }

        [TestMethod]
        public void Build_without_arguments_returns_null()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object,
                _mockImportProcessingRuleOperatorService.Object,
                _mockImportProcessingRuleFieldService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object,
                _mockImportProcessingRuleOperatorService.Object,
                _mockImportProcessingRuleFieldService.Object);

            // Act
            var result = editViewModelBuilder.Build(new CreateViewModelBuilderArgs() { ImportProcessingRuleId = 1, Group = 1 });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
