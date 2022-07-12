using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.TransactionImportProcessor
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        private BusinessLogic.ImportProcessing.TransactionImportProcessor _transactionImportProcessor;

        private const string ExceptionMessage = "A message";

        private void Setup()
        {
            MockTransactionImportValidator.Setup(x => x.Validate(It.IsAny<TransactionImport>()));

            MockUnitOfWork.Setup(x => x.TransactionImports.Add(It.IsAny<TransactionImport>()));
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.ResetChanges());

            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()));

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()));

            _transactionImportProcessor = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                MockLog.Object,
                MockSecurityContext.Object,
                MockUnitOfWork.Object,
                MockRuleEngine.Object,
                MockTransactionService.Object,
                MockTransactionImportValidator.Object);
        }

        [TestMethod]
        public void Process_ValidImport_ReturnsExpectedResult()
        {
            // Arrange
            Setup();

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            result.Should().BeOfType(typeof(Result));
        }

        [TestMethod]
        public void Process_ValidImport_CreatesAReceivedStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            args.TransactionImport.StatusHistories.Any(x => x.StatusId == (int)TransactionImportStatusEnum.Received)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_ValidImport_CreatesAnInProgressStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            args.TransactionImport.StatusHistories.Any(x => x.StatusId == (int)TransactionImportStatusEnum.InProgress)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_ValidImport_CreatesACompletedStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            args.TransactionImport.StatusHistories.Any(x => x.StatusId == (int)TransactionImportStatusEnum.Completed)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_WhenValidationFails_ReturnsAResultWithTheExceptionMessage()
        {
            // Arrange
            Setup();
            MockTransactionImportValidator.Setup(x => x.Validate(It.IsAny<TransactionImport>()))
                .Throws(new TransactionImportProcessorException(ExceptionMessage));

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            result.Error.Should().Be(ExceptionMessage);
        }

        [TestMethod]
        public void Process_WheAnUnexpectedExceptionTypeOccurs_ReturnsAResultWithTheExceptionMessage()
        {
            // Arrange
            Setup();
            MockTransactionImportValidator.Setup(x => x.Validate(It.IsAny<TransactionImport>()))
                .Throws(new NotImplementedException());

            var args = GetTransactionImportProcessorArgs();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            result.Error.Should().Be("The import is not valid");
        }

        [TestMethod]
        public void Process_WhenErrorsSentInArgs_ShouldNotTryToProcessAnyRows()
        {
            // Arrange
            Setup();

            var args = GetTransactionImportProcessorArgsWithErrors();

            // Act
            var result = _transactionImportProcessor.Process(args);

            // Assert
            MockRuleEngine.Verify(x => x.Process(It.IsAny<ProcessedTransaction>(), It.IsAny<int>()), Times.Never);
            MockTransactionService.Verify(x => x.CreateProcessedTransaction(It.IsAny<ProcessedTransaction>()), Times.Never);
        }

        private TransactionImportProcessorArgs GetTransactionImportProcessorArgsWithErrors()
        {
            var transactionImportProcessorArgs = GetTransactionImportProcessorArgs();

            transactionImportProcessorArgs.TransactionImport.AddError("Test");

            return transactionImportProcessorArgs;
        }

        private TransactionImportProcessorArgs GetTransactionImportProcessorArgs()
        {
            return new TransactionImportProcessorArgs()
            {
                TransactionImport = new TransactionImport()
                {
                    TransactionImportTypeId = 1,
                    Rows = new List<TransactionImportRow>()
                    {
                       new TransactionImportRow()
                       {
                           Data = GetTransactionData()
                       }
                    },
                    EventLogs = new List<TransactionImportEventLog>()
                }
            };
        }

        private string GetTransactionData()
        {
            var transaction = new ProcessedTransaction() { };

            return Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
        }
    }
}
