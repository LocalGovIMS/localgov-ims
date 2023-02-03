using Admin.Interfaces.Commands;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.ValidationControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidationControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        private readonly Mock<IModelCommand<TransferItem>> _mockValidateTransferItemCommand = new Mock<IModelCommand<TransferItem>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenValidateTransferItemCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenValidateTransferItemCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: validateTransferItemCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockLogger.Object,
                _mockValidateTransferItemCommand.Object);

            Assert.IsNotNull(dependencies.ValidateTransferItemCommand);
        }
    }
}
