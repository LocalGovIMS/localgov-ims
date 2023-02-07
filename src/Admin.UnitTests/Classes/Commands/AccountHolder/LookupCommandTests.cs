using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.AccountHolder.LookupCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.AccountHolder.LookupViewModel;

namespace Admin.UnitTests.Classes.Commands.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LookupCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                AccountReference = "F1"
            };
        }

        private void SetupAccountHolderService()
        {
            _mockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new BusinessLogic.Entities.AccountHolder()
                {
                    Forename = "Forename",
                    Surname = "Surname",
                    CurrentBalance = 123.45M
                });
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenAccountHolderServiceIsNull()
        {
            try
            {
                var command = new Command(
                    _mockLogger.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionParamNameIfDependenciesIsNull()
        {
            try
            {
                var command = new Command(
                    _mockLogger.Object,
                    null);
            }
            catch (ArgumentNullException e)
            {
                e.ParamName.Should().Be("accountHolderService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var command = new Command(
                    _mockLogger.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: accountHolderService");
            }
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockAccountHolderService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsAccountDetailsWhenAccountExists()
        {
            // Arrange
            SetupAccountHolderService();

            var command = new Command(
                _mockLogger.Object,
                _mockAccountHolderService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Data.GetType().GetProperty("name").GetValue(result.Data, null), "Forename Surname");
            Assert.AreEqual(result.Data.GetType().GetProperty("outstandingBalance").GetValue(result.Data, null), 123.45M);
        }

        [TestMethod]
        public void OnExecuteReturnsDefaultDetailsWhenAccountDoesNotExists()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockAccountHolderService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Data.GetType().GetProperty("name").GetValue(result.Data, null), string.Empty);
            Assert.AreEqual(result.Data.GetType().GetProperty("outstandingBalance").GetValue(result.Data, null), 0);
        }
    }
}
