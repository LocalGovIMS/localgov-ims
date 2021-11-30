using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Payment.EmptyBasketCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmptyBasketCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void ReturnedModelIsViewModel()
        {
            // Arrange
            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
        }

        [TestMethod]
        public void ReturnedModelAddressIsNull()
        {
            // Arrange
            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
            Assert.IsNull(((ViewModel)result.Data).Address);
        }

        [TestMethod]
        public void ReturnedModelBasketHasNoItems()
        {
            // Arrange
            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
            Assert.AreEqual(((ViewModel)result.Data).Basket.Count, 0);
        }
    }
}
