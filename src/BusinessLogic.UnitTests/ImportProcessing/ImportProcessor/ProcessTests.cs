using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using FluentAssertions;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.ImportProcessing.ImportProcessor
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        private BusinessLogic.ImportProcessing.ImportProcessor _ImportProcessor;

        private const string ExceptionMessage = "A message";

        public ProcessTests()
        {
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(CompositeResolver.Create(new IFormatterResolver[]
            {
                // This can solve DateTime time zone problem
                NativeDateTimeResolver.Instance,
                ContractlessStandardResolver.Instance
            }));
        }

        private void Setup()
        {
            MockUnitOfWork.Setup(x => x.Imports.Add(It.IsAny<Import>()));
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.ImportRows.BulkInsert(It.IsAny<IEnumerable<ImportRow>>()));

            MockUnitOfWork.Setup(x => x.ImportRows.Find(It.IsAny<Expression<Func<ImportRow, bool>>>()))
                .Returns(GetImportRows());

            MockUnitOfWork.Setup(x => x.ResetChanges());
            MockUnitOfWork.Setup(x => x.ImportTypes.Get(It.IsAny<int>()))
                .Returns(new ImportType() { DataType = (byte)ImportDataTypeEnum.Transaction });

            MockImportProcessingValidator.Setup(x => x.Validate(It.IsAny<ImportProcessingStrategyValidatorArgs>()));

            MockImportProcessingValidatorFactory.Setup(x => x.Invoke(It.IsAny<string>()))
                .Returns(MockImportProcessingValidator.Object);

            MockImportProcessingStrategy.Setup(x => x.Process(It.IsAny<ImportProcessingStrategyArgs>()));

            MockImportProcessingStrategyFactory.Setup(x => x.Invoke(It.IsAny<string>()))
                .Returns(MockImportProcessingStrategy.Object);

            _ImportProcessor = new BusinessLogic.ImportProcessing.ImportProcessor(
                MockLog.Object,
                MockSecurityContext.Object,
                MockUnitOfWork.Object,
                MockImportProcessingStrategyFactory.Object,
                MockImportProcessingValidatorFactory.Object);
        }

        [TestMethod]
        public void Process_ValidImport_ReturnsExpectedResult()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            result.Should().BeOfType(typeof(Result));
        }

        [TestMethod]
        public void Process_ValidImport_CreatesAReceivedStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgs();

            //var import = args.Import;
            //import.StatusHistories.Add(new ImportStatusHistory() { StatusId = 1 });

            //MockUnitOfWork.Setup(x => x.Imports.Get(It.IsAny<int>()))
            //    .Returns(import);

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.Received)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_ValidImport_CreatesAnInProgressStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.InProgress)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_ValidImport_CreatesASucceededStatusHistory()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.Succeeded)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Process_WhenValidationFails_ReturnsAResultWithTheExceptionMessage()
        {
            // Arrange
            Setup();
            MockImportProcessingValidator.Setup(x => x.Validate(It.IsAny<ImportProcessingStrategyValidatorArgs>()))
                .Throws(new ImportProcessingException(ExceptionMessage));

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            result.Error.Should().Be(ExceptionMessage);
        }

        [TestMethod]
        public void Process_WheAnUnexpectedExceptionTypeOccurs_ReturnsAResultWithTheExceptionMessage()
        {
            // Arrange
            Setup();
            MockImportProcessingValidator.Setup(x => x.Validate(It.IsAny<ImportProcessingStrategyValidatorArgs>()))
                .Throws(new NotImplementedException());

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            result.Error.Should().Be("The import is not valid");
        }

        [TestMethod]
        public void Process_WhenErrorsSentInArgs_ShouldNotTryToValidateAnyRows()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgsWithErrors();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            MockImportProcessingValidator.Verify(x => x.Validate(It.IsAny<ImportProcessingStrategyValidatorArgs>()), Times.Never);
        }

        [TestMethod]
        public void Process_WhenErrorsSentInArgs_ShouldNotTryToProcessAnyRows()
        {
            // Arrange
            Setup();

            var args = GetImportProcessorArgsWithErrors();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            MockImportProcessingStrategy.Verify(x => x.Process(It.IsAny<ImportProcessingStrategyArgs>()), Times.Never);
        }

        [TestMethod]
        public void Process_WhenRowProcessingFails_ProcessingCompletesAsAFailure()
        {
            // Arrange
            Setup();
            MockImportProcessingStrategy.Setup(x => x.Process(It.IsAny<ImportProcessingStrategyArgs>()))
                .Throws<InvalidOperationException>();

            var args = GetImportProcessorArgs();

            // Act
            var result = _ImportProcessor.Process(args);

            // Assert
            args.Import.StatusHistories.Any(x => x.StatusId == (int)ImportStatusEnum.Failed)
                .Should()
                .BeTrue();
        }

        private ImportProcessorArgs GetImportProcessorArgsWithErrors()
        {
            var ImportProcessorArgs = GetImportProcessorArgs();

            ImportProcessorArgs.Import.AddError("Test");

            return ImportProcessorArgs;
        }

        private ImportProcessorArgs GetImportProcessorArgs()
        {
            return new ImportProcessorArgs()
            {
                Import = new Import()
                {
                    ImportTypeId = 1,
                    
                    EventLogs = new List<ImportEventLog>()
                },
                ImportRows = GetImportRows()
            };
        }

        private List<ImportRow> GetImportRows()
        {
            return new List<ImportRow>()
            {
                new ImportRow()
                {
                    Data = GetTransactionData()
                }
            };
        }

        private string GetTransactionData()
        {
            var transaction = new ProcessedTransaction() { };

            return Convert.ToBase64String(MessagePackSerializer.Serialize(transaction));
        }
    }
}
