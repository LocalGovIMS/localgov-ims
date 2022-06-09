using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.FundMetadata.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.FundMetadata.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.FundMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMetadataService> _mockFundMetadataService = new Mock<IFundMetadataService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1,
                Key = "Test key",
                Value = "Test value"
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockFundMetadataService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockFundMetadataService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockFundMetadataService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.FundMetadata>()), Times.Once);
        }
    }
}
