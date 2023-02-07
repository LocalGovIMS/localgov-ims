using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.AccountReferenceValidator.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.AccountReferenceValidator.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.AccountReferenceValidator
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 0,
                Name = "Account Reference Validator"
            };
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
                e.ParamName.Should().Be("accountReferenceValidatorService");
            }
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockAccountReferenceValidatorService.Object);

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
            _mockAccountReferenceValidatorService.Setup(x => x.Update(It.IsAny<BusinessLogic.Entities.AccountReferenceValidator>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockAccountReferenceValidatorService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
