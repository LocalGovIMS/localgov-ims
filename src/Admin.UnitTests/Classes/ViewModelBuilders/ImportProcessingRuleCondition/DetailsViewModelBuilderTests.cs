using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRuleCondition.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleConditionService> _mockImportProcessingRuleConditionService = new Mock<IImportProcessingRuleConditionService>();

        private void SetupService(Mock<IImportProcessingRuleConditionService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRuleCondition()
                {
                    Id = 1,
                    ImportProcessingRuleId = 1,
                    ImportProcessingRuleFieldId = 1,
                    Value = "Test value",
                    Field = new BusinessLogic.Entities.ImportProcessingRuleField()
                    {
                        DisplayName = "Test field display name"
                    },
                    Operator = new BusinessLogic.Entities.ImportProcessingRuleOperator()
                    {
                        DisplayName = "Test operator display name"
                    }
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockImportProcessingRuleConditionService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleConditionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_returns_the_field_DisplayName()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleConditionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.FieldName.Should().Be("Test field display name");
        }

        [TestMethod]
        public void Build_returns_the_operator_DisplayName()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleConditionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.OperatorName.Should().Be("Test operator display name");
        }
    }
}
