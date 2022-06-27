using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Validator = BusinessLogic.ImportProcessing.TransactionImportProcessorValidator;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.TransactionImportProcessorValidator
{
    [TestClass]
    public class ValidateTests
    {
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();
        
        private const int ValidTransactionTypeId = 1;
        private const int InvalidTransactionTypeId = 2;

        private const int ValidTotalNumberOfTransactions = 2;
        private const int InvalidTotalNumberOfTransactions = 3;

        private const decimal Transaction1Amount = 10;
        private const decimal Transaction2Amount = 20;
        private const decimal ValidTotalAmount = Transaction1Amount + Transaction2Amount;

        public ValidateTests()
        {
           
        }

        private void SetupTransactionImportTypeService()
        {
            _mockTransactionImportTypeService.Setup(x => x.Get(ValidTransactionTypeId))
                .Returns(new TransactionImportType());

            _mockTransactionImportTypeService.Setup(x => x.Get(InvalidTransactionTypeId))
                .Returns<TransactionImportType>(null);
        }

        [TestMethod]
        public void Validate_UnknownTransactionType_ThrowsException()
        {
            // Arrange
            SetupTransactionImportTypeService();

            var transactionImport = GetTransactionImport();
            transactionImport.TransactionImportTypeId = InvalidTransactionTypeId;

            var validator = new Validator(_mockTransactionImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(transactionImport);

            // Assert
            act.Should()
                .Throw<TransactionImportProcessorException>()
                .WithMessage("The Transaction Type is not valid");
        }

        [TestMethod]
        public void Validate_TotalNumberOfRowsDoesNotMatchNumerOfRows_ThrowsException()
        {
            // Arrange
            SetupTransactionImportTypeService();

            var transactionImport = GetTransactionImport();
            transactionImport.TotalNumberOfTransactions = InvalidTotalNumberOfTransactions;
 
            var validator = new Validator(_mockTransactionImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(transactionImport);

            // Assert
            act.Should()
                .Throw<TransactionImportProcessorException>()
                .WithMessage("The number of transactions expected does not match the number of transactions provided");
        }

        [TestMethod]
        [DataRow("29")]
        [DataRow("31")]
        public void Validate_TotalAmountDoesNotMatchSumOfRowTotal_ThrowsException(string invalidTotalAmount)
        {
            // Arrange
            SetupTransactionImportTypeService();

            var transactionImport = GetTransactionImport();
            transactionImport.TotalAmount = Convert.ToDecimal(invalidTotalAmount);

            var validator = new Validator(_mockTransactionImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(transactionImport);
            
            // Assert
            act.Should()
                .Throw<TransactionImportProcessorException>()
                .WithMessage("The total amount specified does not match the total of all the rows provided");
        }
        
        [TestMethod]
        public void Validate_WithValidData_Passes()
        {
            // Arrange
            SetupTransactionImportTypeService();

            var transactionImport = GetTransactionImport();

            var validator = new Validator(_mockTransactionImportTypeService.Object);

            // Act
            Action act = () => validator.Validate(transactionImport);

            act.Should()
                .NotThrow();
        }

        private TransactionImport GetTransactionImport()
        {
            return new TransactionImport()
            {
                TransactionImportTypeId = ValidTransactionTypeId,
                TotalNumberOfTransactions = ValidTotalNumberOfTransactions,
                TotalAmount = ValidTotalAmount,
                Rows = new List<TransactionImportRow>()
                {   
                    new TransactionImportRow() { Data = GetTransactionData(Transaction1Amount) },
                    new TransactionImportRow() { Data = GetTransactionData(Transaction2Amount) }
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
