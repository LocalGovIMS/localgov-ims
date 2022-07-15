using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategy
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        [TestMethod]
        public void Process_CallsRuleEngineProcessOnce()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockRuleEngine.Verify(x => x.Process(It.IsAny<ProcessedTransaction>()), Times.Once);
        }

        [TestMethod]
        public void Process_CallsTransactionServiceCreateProcessedTransactionOnce()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockTransactionService.Verify(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Another Test")]
        public void Process_WhenCreateProcessedTransactionReturnsAnUnsuccessfulResult_ThrowsAnExceptionWithTheResultError(string errorMessage)
        {
            // Arrange
            SetupDependenciesForFailure(errorMessage);
            SetupStrategy();

            // Act
            Action action = () => Strategy.Process(GetArgs());

            // Assert
            action.Should()
                .Throw<ImportProcessingException>()
                .WithMessage(errorMessage);
        }
    }

    //[TestClass]
    //public class ProcessTests : TestBase
    //{
    //    private BusinessLogic.ImportProcessing.ImportProcessor _ImportProcessor;

    //    private const string ExceptionMessage = "A message";

    //    private void Setup()
    //    {
    //        MockImportValidator.Setup(x => x.Validate(It.IsAny<Import>()));

    //        MockUnitOfWork.Setup(x => x.Imports.Add(It.IsAny<Import>()));
    //        MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>()));
    //        MockUnitOfWork.Setup(x => x.ResetChanges());

    //        MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()));

    //        MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()));

    //        _ImportProcessor = new BusinessLogic.ImportProcessing.ImportProcessor(
    //            MockLog.Object,
    //            MockSecurityContext.Object,
    //            MockUnitOfWork.Object,
    //            MockRuleEngine.Object,
    //            MockTransactionService.Object,
    //            MockImportValidator.Object);
    //    }

    //    [TestMethod]
    //    public void Process_ValidImport_ReturnsExpectedResult()
    //    {
    //        // Arrange
    //        Setup();

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        result.Should().BeOfType(typeof(Result));
    //    }

    //    [TestMethod]
    //    public void Process_ValidImport_CreatesAReceivedStatusHistory()
    //    {
    //        // Arrange
    //        Setup();

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.Received)
    //            .Should()
    //            .BeTrue();
    //    }

    //    [TestMethod]
    //    public void Process_ValidImport_CreatesAnInProgressStatusHistory()
    //    {
    //        // Arrange
    //        Setup();

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.InProgress)
    //            .Should()
    //            .BeTrue();
    //    }

    //    [TestMethod]
    //    public void Process_ValidImport_CreatesACompletedStatusHistory()
    //    {
    //        // Arrange
    //        Setup();

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.Completed)
    //            .Should()
    //            .BeTrue();
    //    }

    //    [TestMethod]
    //    public void Process_WhenValidationFails_ReturnsAResultWithTheExceptionMessage()
    //    {
    //        // Arrange
    //        Setup();
    //        MockImportValidator.Setup(x => x.Validate(It.IsAny<Import>()))
    //            .Throws(new ImportProcessorException(ExceptionMessage));

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        result.Error.Should().Be(ExceptionMessage);
    //    }

    //    [TestMethod]
    //    public void Process_WheAnUnexpectedExceptionTypeOccurs_ReturnsAResultWithTheExceptionMessage()
    //    {
    //        // Arrange
    //        Setup();
    //        MockImportValidator.Setup(x => x.Validate(It.IsAny<Import>()))
    //            .Throws(new NotImplementedException());

    //        var args = GetImportProcessorArgs();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        result.Error.Should().Be("The import is not valid");
    //    }

    //    [TestMethod]
    //    public void Process_WhenErrorsSentInArgs_ShouldNotTryToProcessAnyRows()
    //    {
    //        // Arrange
    //        Setup();

    //        var args = GetImportProcessorArgsWithErrors();

    //        // Act
    //        var result = _ImportProcessor.Process(args);

    //        // Assert
    //        MockRuleEngine.Verify(x => x.Process(It.IsAny<ProcessedTransaction>()), Times.Never);
    //        MockTransactionService.Verify(x => x.CreateProcessedTransaction(It.IsAny<ProcessedTransaction>()), Times.Never);
    //    }

    //    private ImportProcessorArgs GetImportProcessorArgsWithErrors()
    //    {
    //        var ImportProcessorArgs = GetImportProcessorArgs();

    //        ImportProcessorArgs.Import.AddError("Test");

    //        return ImportProcessorArgs;
    //    }

    //    private ImportProcessorArgs GetImportProcessorArgs()
    //    {
    //        return new ImportProcessorArgs()
    //        {
    //            Import = new Import()
    //            {
    //                ImportTypeId = 1,
    //                Rows = new List<ImportRow>()
    //                {
    //                   new ImportRow()
    //                   {
    //                       Data = GetTransactionData()
    //                   }
    //                },
    //                EventLogs = new List<ImportEventLog>()
    //            }
    //        };
    //    }

    //    private string GetTransactionData()
    //    {
    //        var transaction = new ProcessedTransaction() { };

    //        return Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
    //    }
    //}
}
