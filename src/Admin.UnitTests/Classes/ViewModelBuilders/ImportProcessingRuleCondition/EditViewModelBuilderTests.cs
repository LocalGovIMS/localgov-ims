using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRuleCondition.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleConditionService> _mockImportProcessingRuleConditionService = new Mock<IImportProcessingRuleConditionService>();
        private readonly Mock<IImportProcessingRuleFieldService> _mockImportProcessingRuleFieldService = new Mock<IImportProcessingRuleFieldService>();
        private readonly Mock<IImportProcessingRuleOperatorService> _mockImportProcessingRuleOperatorService = new Mock<IImportProcessingRuleOperatorService>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object,
                _mockImportProcessingRuleFieldService.Object,
                _mockImportProcessingRuleOperatorService.Object,
                _mockImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockImportProcessingRuleConditionService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRuleCondition()
                {
                    Id = 1,
                    Group = 1,
                    ImportProcessingRuleId = 1,
                    ImportProcessingRuleFieldId = 1,
                    ImportProcessingRuleOperatorId = 1,
                    Value = "Test value"
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

            _mockImportProcessingRuleService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRule()
                {
                    Id = 1,
                    Conditions = new List<BusinessLogic.Entities.ImportProcessingRuleCondition>()
                    {
                        new BusinessLogic.Entities.ImportProcessingRuleCondition()
                        {
                            Group = 1
                        }
                    }
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_sets_the_Id_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleFieldId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleFieldId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleOperatorId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleOperatorId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_Value_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Value.Should().Be("Test value");
        }
    }
}
