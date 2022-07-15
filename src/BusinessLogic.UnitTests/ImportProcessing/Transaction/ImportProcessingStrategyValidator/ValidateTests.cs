using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Validator = BusinessLogic.ImportProcessing.TransactionImportProcessingStrategyValidator;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategyValidator
{
    [TestClass]
    public class ValidateTests
    {
        private readonly Mock<IImportTypeService> _mockImportTypeService = new Mock<IImportTypeService>();
        
        private const int ValidImportTypeId = 1;
        private const int MismatchedImportTypeId = 2;
        private const int InvalidImportTypeId = 3;

        private const int ValidNumberOfRows = 2;
        private const int InvalidNumberOfRows = 3;

        public ValidateTests()
        {
           
        }

        private void SetupImportTypeService()
        {
            _mockImportTypeService.Setup(x => x.Get(ValidImportTypeId))
                .Returns(new ImportType() {  DataType = (byte)ImportDataTypeEnum.Transaction });

            _mockImportTypeService.Setup(x => x.Get(MismatchedImportTypeId))
                .Returns(new ImportType() { DataType = (byte)ImportDataTypeEnum.AccountHolder });

            _mockImportTypeService.Setup(x => x.Get(InvalidImportTypeId))
                .Returns<ImportType>(null);
        }

        [TestMethod]
        public void Validate_UnknownTransactionType_ThrowsException()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();
            import.ImportTypeId = InvalidImportTypeId;

            var validator = new Validator(_mockImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(import);

            // Assert
            act.Should()
                .Throw<ImportProcessingException>()
                .WithMessage("The import type specified is not recognised");
        }

        [TestMethod]
        public void Validate_MismatchedImportType_ThrowsException()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();
            import.ImportTypeId = MismatchedImportTypeId;

            var validator = new Validator(_mockImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(import);

            // Assert
            act.Should()
                .Throw<ImportProcessingException>()
                .WithMessage($"Expecting import type {((ImportDataTypeEnum)ValidImportTypeId).GetDisplayName()} but received {((ImportDataTypeEnum)MismatchedImportTypeId).GetDisplayName()}");
        }

        [TestMethod]
        public void Validate_RowCountDoesNotMatchNumberOfRows_ThrowsException()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();
            import.NumberOfRows = InvalidNumberOfRows;
 
            var validator = new Validator(_mockImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(import);

            // Assert
            act.Should()
                .Throw<ImportProcessingException>()
                .WithMessage("The number of expected rows does not match the number of rows provided");
        }
        
        [TestMethod]
        public void Validate_WithValidData_Passes()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();

            var validator = new Validator(_mockImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(import);

            act.Should()
                .NotThrow();
        }

        private Import GetImport()
        {
            return new Import()
            {
                ImportTypeId = ValidImportTypeId,
                NumberOfRows = ValidNumberOfRows,
                Rows = new List<ImportRow>()
                {   
                    new ImportRow() { Data = GetTransactionData(10) },
                    new ImportRow() { Data = GetTransactionData(20) }
                }
            };
        }

        private string GetTransactionData(decimal amount)
        {
            var transaction = new ProcessedTransaction() { Amount = amount };

            return Newtonsoft.Json.JsonConvert.SerializeObject(transaction);
        }
    }
}
