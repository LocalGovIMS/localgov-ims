using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using FluentAssertions;
using MessagePack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Validator = BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategyValidator;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    [TestClass]
    public class ValidateTests
    {
        private readonly Mock<IImportTypeService> _mockImportTypeService = new Mock<IImportTypeService>();
        protected readonly Mock<IAccountHolderFundMessageValidator> _mockAccountHolderFundMessageValidator = new Mock<IAccountHolderFundMessageValidator>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private const int ValidImportTypeId = 2;
        private const int MismatchedImportTypeId = 1;
        private const int InvalidImportTypeId = 3;

        private const int ValidNumberOfRows = 2;
        private const int InvalidNumberOfRows = 3;

        private const string ValidFundCode = "A1";
        private const string InvalidFundCode = "B1";

        public ValidateTests()
        {

        }

        private void SetupImportTypeService()
        {
            _mockImportTypeService.Setup(x => x.Get(ValidImportTypeId))
                .Returns(new ImportType() { DataType = (byte)ImportDataTypeEnum.AccountHolder });

            _mockImportTypeService.Setup(x => x.Get(MismatchedImportTypeId))
                .Returns(new ImportType() { DataType = (byte)ImportDataTypeEnum.Transaction });

            _mockImportTypeService.Setup(x => x.Get(InvalidImportTypeId))
                .Returns<ImportType>(null);

            _mockAccountHolderFundMessageValidator.Setup(x => x.Validate(It.IsAny<Entities.AccountHolder>()))
                .Returns(new Result());

            _mockFundService.Setup(x => x.GetAllFunds())
                .Returns(new List<Fund>() {
                    new Fund()
                    {
                        FundCode = ValidFundCode
                    }
                });
        }

        private Validator GetValidator()
        {
            return new Validator(
                _mockImportTypeService.Object,
                _mockAccountHolderFundMessageValidator.Object,
                _mockFundService.Object);
        }

        [TestMethod]
        public void Validate_UnknownImportType_ThrowsException()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();
            import.ImportTypeId = InvalidImportTypeId;

            var validator = GetValidator();

            // Act
            Action act = () => validator.Validate(GetValidatorArgs(import));

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

            var validator = GetValidator();

            // Act
            Action act = () => validator.Validate(GetValidatorArgs(import));

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

            var validator = GetValidator();

            // Act
            Action act = () => validator.Validate(GetValidatorArgs(import));

            // Assert
            act.Should()
                .Throw<ImportProcessingException>()
                .WithMessage("The number of expected rows does not match the number of rows provided");
        }

        [TestMethod]
        public void Validate_UnknownFundCode_ThrowsException()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport(InvalidFundCode);

            var validator = GetValidator();

            // Act
            Action act = () => validator.Validate(GetValidatorArgs(import));

            // Assert
            act.Should()
                .Throw<ImportProcessingException>()
                .WithMessage($"Fund code '{InvalidFundCode}' on row 1 not recognised");
        }

        [TestMethod]
        public void Validate_WithValidData_Passes()
        {
            // Arrange
            SetupImportTypeService();

            var import = GetImport();

            var validator = GetValidator();

            // Act
            Action act = () => validator.Validate(GetValidatorArgs(import));

            act.Should()
                .NotThrow();
        }

        private Import GetImport()
        {
            return GetImport(ValidFundCode);
        }

        private Import GetImport(string fundCode)
        {
            return new Import()
            {
                ImportTypeId = ValidImportTypeId,
                NumberOfRows = ValidNumberOfRows,
                Rows = new List<ImportRow>()
                {
                    new ImportRow() { Data = GetAccountHolderData(fundCode) },
                    new ImportRow() { Data = GetAccountHolderData(fundCode) }
                }
            };
        }

        private ImportProcessingStrategyValidatorArgs GetValidatorArgs(Import import)
        {
            return new ImportProcessingStrategyValidatorArgs()
            {
                Import = import,
                ImportRows = import.Rows.ToList()
            };
        }

        private string GetAccountHolderData(string fundCode)
        {
            var item = new Entities.AccountHolder() { FundCode = fundCode };

            return Convert.ToBase64String(MessagePackSerializer.Serialize(item, MessagePack.Resolvers.ContractlessStandardResolver.Options));
        }
    }
}
