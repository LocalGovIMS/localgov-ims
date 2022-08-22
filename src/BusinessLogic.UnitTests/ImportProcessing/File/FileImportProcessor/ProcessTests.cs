using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.File.FileImportProcessor
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        private void Initialise()
        {
            MockUnitOfWork.Setup(x => x.FileImports.GetByImportId(It.IsAny<int>()))
                .Returns(GetImport());

            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(a => a);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<ProcessedTransaction>(), It.IsAny<bool>()))
                .Returns(new Result());

            MockProcessedTransactionModelBuilder.Setup(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(GetProcessedTransactionModel());
        }

        private void SetupBadImportData()
        {
            MockProcessedTransactionModelBuilder.Setup(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new InvalidCastException());
        }

        [TestMethod]
        public void Process_returns_a_Result()
        {
            // Arrange
            Initialise();
            SetupFileImportProcessor();
            SetupFileImportProcessorArgs();

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
            SetupFileImportProcessor();
            SetupFileImportProcessorArgs();
            SetupBadImportData();

            // Act
            var result = ImportProcessor.Process(Args);

            // Assert
            MockProcessedTransactionModelBuilder.Verify(x => x.BuildFromCsvRow(It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(10));
        }
    }
}
