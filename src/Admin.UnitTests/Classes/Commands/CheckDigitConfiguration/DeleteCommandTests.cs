using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.CheckDigitConfiguration.DeleteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ICheckDigitConfigurationService> _mockCheckDigitConfigurationService = new Mock<ICheckDigitConfigurationService>();

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockCheckDigitConfigurationService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
