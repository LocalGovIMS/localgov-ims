using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.ImportProcessor
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        // Check transaction service called correct number of times
        private BusinessLogic.ImportProcessing.ImportProcessor _importProcessor;

        private void Initialise()
        {
            MockUnitOfWork.Setup(x => x.Imports.GetByBatchReference(It.IsAny<string>()))
                .Returns(GetImport());

            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(a => a);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<ProcessedTransaction>(), It.IsAny<bool>()))
                .Returns(new Result());

            MockProcessedTransactionModelBuilder.Setup(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(GetProcessedTransactionModel());
        }

        private void SetupBadImportData()
        {
            MockProcessedTransactionModelBuilder.Setup(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new InvalidCastException());
        }

        [TestMethod]
        public void Process_returns_a_Result()
        {
            // Arrange
            Initialise();
            SetupImportProcessor();
            SetupImportProcessorArgs();

            // Act
            var result = ImportProcessor.Process(Args);

            // Assert
            result.Should().BeOfType(typeof(Result));
        }

        [TestMethod]
        public void Processing_will_stop_if_ten_errors_are_encountered()
        {
            // Arrange
            Initialise();
            SetupImportProcessor();
            SetupImportProcessorArgs();
            SetupBadImportData();

            // Act
            var result = ImportProcessor.Process(Args);

            // Assert
            MockProcessedTransactionModelBuilder.Verify(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(10));
        }

        [TestMethod]
        public void UnitOfWork_will_save_changes_if_no_errors_occur()
        {
            // Arrange
            Initialise();
            SetupImportProcessor();
            SetupImportProcessorArgs();

            // Act
            var result = ImportProcessor.Process(Args);

            // Assert
            MockUnitOfWork.Verify(x => x.Complete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void UnitOfWork_will_not_save_changes_if_no_errors_occur()
        {
            // Arrange
            Initialise();
            SetupImportProcessor();
            SetupImportProcessorArgs();
            SetupBadImportData();

            // Act
            var result = ImportProcessor.Process(Args);

            // Assert
            MockUnitOfWork.Verify(x => x.Complete(It.IsAny<int>()), Times.Never);
        }

    }
}
