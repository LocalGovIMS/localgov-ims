﻿using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Validators.Payment;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Validation.ValidateTransferItemCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using Model = BusinessLogic.Models.TransferItem;

namespace Admin.UnitTests.Classes.Commands.Validation
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTransferItemCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionJournalService> _mockTransactionJournalService = new Mock<ITransactionJournalService>();
        private readonly Mock<IPaymentValidationHandler> _mockAccountReferenceValidator = new Mock<IPaymentValidationHandler>();

        private void SetupAccountReferenceValidator(Mock<IPaymentValidationHandler> item)
        {
            item.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new BusinessLogic.Classes.Result.Result());
        }

        private Model GenerateModel()
        {
            return new Model()
            {
                AccountReference = "Test",
                Amount = 10,
                FundCode = "F1",
                FundName = "TestFund",
                Id = new System.Guid()
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupAccountReferenceValidator(_mockAccountReferenceValidator);

            var command = new Command(
                _mockLogger.Object,
                _mockAccountReferenceValidator.Object);

            // Act
            var result = command.Execute(GenerateModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void MissingFundCodeCausesError()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockAccountReferenceValidator.Object);

            var model = GenerateModel();
            model.FundCode = string.Empty;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void MissingAcountReferenceCausesError()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockAccountReferenceValidator.Object);

            var model = GenerateModel();
            model.AccountReference = string.Empty;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }
    }
}
