using Admin.Classes.Models;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Payment;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.PaymentControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PaymentControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<IndexViewModel, IndexViewModel>> _mockIndexViewModelBuider = new Mock<IModelBuilder<IndexViewModel, IndexViewModel>>();
        private readonly Mock<IModelCommand<IndexViewModel>> _mockAddCommand = new Mock<IModelCommand<IndexViewModel>>();
        private readonly Mock<IModelCommand<Guid>> _mockRemoveCommand = new Mock<IModelCommand<Guid>>();
        private readonly Mock<IModelCommand<string>> _mockEmptyBasketCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<IndexViewModel>> _mockCheckAddressCommand = new Mock<IModelCommand<IndexViewModel>>();
        private readonly Mock<IModelCommand<IndexViewModel>> _mockCreatePaymentsCommand = new Mock<IModelCommand<IndexViewModel>>();
        private readonly Mock<IModelCommand<IndexViewModel>> _mockSetAddressCommand = new Mock<IModelCommand<IndexViewModel>>();
        private readonly Mock<IModelCommand<ProcessPaymentCommandAgrs>> _mockProcessPaymentCommand = new Mock<IModelCommand<ProcessPaymentCommandAgrs>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenIndexViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenIndexViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: indexViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenAddCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    null,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenAddCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    null,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: addCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenRemoveCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    null,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenRemoveCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    null,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: removeCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEmptyBasketCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    null,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEmptyBasketCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    null,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: emptyBasketCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCheckAddressCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    null,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCheckAddressCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    null,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: checkAddressCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreatePaymentsCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    null,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCreatePaymentsCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    null,
                    _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: createPaymentsCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSetAddressCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    null,
                    _mockProcessPaymentCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenSetAddressCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                     _mockLogger.Object,
                     _mockIndexViewModelBuider.Object,
                     _mockAddCommand.Object,
                     _mockRemoveCommand.Object,
                     _mockEmptyBasketCommand.Object,
                     _mockCheckAddressCommand.Object,
                     _mockCreatePaymentsCommand.Object,
                     null,
                    _mockProcessPaymentCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: setAddressCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenProcessPaymentCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockIndexViewModelBuider.Object,
                    _mockAddCommand.Object,
                    _mockRemoveCommand.Object,
                    _mockEmptyBasketCommand.Object,
                    _mockCheckAddressCommand.Object,
                    _mockCreatePaymentsCommand.Object,
                    _mockSetAddressCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenProcessPaymentCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                     _mockLogger.Object,
                     _mockIndexViewModelBuider.Object,
                     _mockAddCommand.Object,
                     _mockRemoveCommand.Object,
                     _mockEmptyBasketCommand.Object,
                     _mockCheckAddressCommand.Object,
                     _mockCreatePaymentsCommand.Object,
                     _mockSetAddressCommand.Object,
                     null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: processPaymentCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                     _mockLogger.Object,
                     _mockIndexViewModelBuider.Object,
                     _mockAddCommand.Object,
                     _mockRemoveCommand.Object,
                     _mockEmptyBasketCommand.Object,
                     _mockCheckAddressCommand.Object,
                     _mockCreatePaymentsCommand.Object,
                     _mockSetAddressCommand.Object,
                    _mockProcessPaymentCommand.Object);

            Assert.IsNotNull(dependencies.IndexViewModelBuilder);
            Assert.IsNotNull(dependencies.AddCommand);
            Assert.IsNotNull(dependencies.RemoveCommand);
            Assert.IsNotNull(dependencies.EmptyBasketCommand);
            Assert.IsNotNull(dependencies.CheckAddressCommand);
            Assert.IsNotNull(dependencies.CreatePaymentsCommand);
            Assert.IsNotNull(dependencies.SetAddressCommand);
            Assert.IsNotNull(dependencies.ProcessPaymentCommand);
        }
    }
}
