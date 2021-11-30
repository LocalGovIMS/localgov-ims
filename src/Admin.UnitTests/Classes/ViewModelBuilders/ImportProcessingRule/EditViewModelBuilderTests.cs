using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRule.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRule.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRule

{
    [TestClass]
    public class EditViewModelBuilderTests
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
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }


        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
