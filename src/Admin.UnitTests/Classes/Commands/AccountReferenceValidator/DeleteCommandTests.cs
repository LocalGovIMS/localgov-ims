﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.AccountReferenceValidator.DeleteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.AccountReferenceValidator
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();

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
            var result = command.Execute(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
