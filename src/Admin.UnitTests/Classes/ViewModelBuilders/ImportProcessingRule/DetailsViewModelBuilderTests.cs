using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRule.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRule.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRule
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        private void SetupService(Mock<IImportProcessingRuleService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRule()
                {
                    Id = 1
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockImportProcessingRuleService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
