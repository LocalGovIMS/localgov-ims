using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRuleAction.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleActionService> _mockImportProcessingRuleActionService = new Mock<IImportProcessingRuleActionService>();

        private void SetupService(Mock<IImportProcessingRuleActionService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRuleAction()
                {
                    Id = 1,
                    ImportProcessingRuleId = 1,
                    ImportProcessingRuleFieldId = 1,
                    Value = "Test value",
                    Field = new BusinessLogic.Entities.ImportProcessingRuleField()
                    {
                        DisplayName = "Test display name"
                    }
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockImportProcessingRuleActionService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleActionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_returns_the_field_DisplayName()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleActionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.FieldName.Should().Be("Test display name");
        }
    }
}
