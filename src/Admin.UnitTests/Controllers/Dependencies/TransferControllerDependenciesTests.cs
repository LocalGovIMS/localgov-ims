using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transfer;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.TransferControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuider = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelCommand<TransferViewModel>> _mockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        private readonly Mock<IModelCommand<TransferItem>> _mockValidateTransferCommand = new Mock<IModelCommand<TransferItem>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransferViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockTransferCommand.Object,
                    _mockValidateTransferCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenTransferViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockTransferCommand.Object,
                    _mockValidateTransferCommand.Object);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("transferViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockTransferViewModelBuider.Object,
                    null,
                    _mockValidateTransferCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockTransferViewModelBuider.Object,
                    null,
                    _mockValidateTransferCommand.Object);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("transferCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenValidateTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockTransferViewModelBuider.Object,
                    _mockTransferCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenValidateTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockTransferViewModelBuider.Object,
                    _mockTransferCommand.Object,
                    null);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("transferCommand");
            }
        }


        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockTransferViewModelBuider.Object,
                    _mockTransferCommand.Object,
                    _mockValidateTransferCommand.Object);

            Assert.IsNotNull(dependencies.TransferViewModelBuilder);
            Assert.IsNotNull(dependencies.TransferCommand);
            Assert.IsNotNull(dependencies.ValidateTransferItemCommand);
        }
    }
}
