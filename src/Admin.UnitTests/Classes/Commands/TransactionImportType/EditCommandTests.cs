using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.TransactionImportType.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.TransactionImportType.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.TransactionImportType
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_TransactionImportTypeService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockTransactionImportTypeService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.TransactionImportType>()), Times.Once);
        }
    }
}
