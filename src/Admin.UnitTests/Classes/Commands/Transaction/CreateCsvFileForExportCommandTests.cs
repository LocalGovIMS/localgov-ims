using Admin.Classes.Commands.Transaction;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Command = Admin.Classes.Commands.Transaction.CreateCsvFileForExportCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCsvFileForExportCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var args = new CreateCsvFileForExportCommandArgs()
            {
                Transactions = new List<BusinessLogic.Entities.ProcessedTransaction>()
            };

            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(args);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResultWithAFileContentResultAsData()
        {
            // Arrange
            var args = new CreateCsvFileForExportCommandArgs()
            {
                Transactions = new List<BusinessLogic.Entities.ProcessedTransaction>()
            };

            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(args);

            // Assert
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(FileContentResult));
        }
    }
}
