using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Web.Mvc;
using Command = Admin.Classes.Commands.CheckDigitConfiguration.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.CheckDigitConfiguration.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ICheckDigitConfigurationService> _mockCheckDigitConfigurationService = new Mock<ICheckDigitConfigurationService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 0,
                Name = "New Check Digit Configuration"
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockCheckDigitConfigurationService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResultFromResult()
        {
            // Arrange
            _mockCheckDigitConfigurationService.Setup(x => x.Create(It.IsAny<BusinessLogic.Entities.CheckDigitConfiguration>()))
               .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockCheckDigitConfigurationService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
