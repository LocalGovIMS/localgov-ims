using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using BusinessLogic.Validators.Payment;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Validators.TransactionJournal
{
    [TestClass]
    public class TransactionJournalTests
    {
        private Mock<IPaymentValidationHandler> _mockPaymentValidationHandler = new Mock<IPaymentValidationHandler>();

        private BusinessLogic.Validators.TransactionJournalValidator GetValidator()
        {
            return new BusinessLogic.Validators.TransactionJournalValidator(_mockPaymentValidationHandler.Object);
        }

        [TestMethod]
        public void ValidateReturnsError()
        {
            var validator = GetValidator();

            var result = validator.Validate(null, null, null);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to find the transaction to validate transfers against");
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]

        public void ValidateAccountReferenceIsInvaild()
        {
            var t = new Transaction(new List<Entities.ProcessedTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                string.Empty);

            List<TransferItem> transferItems = new List<TransferItem>()
            {
                new TransferItem()
                {
                    AccountReference="123",
                    Amount=100,
                    FundCode="12",
                    FundName="MOCKFUND",
                    Id=Guid.NewGuid()
                }
            };
            string errorMsg = "MOCK ERROR";
            _mockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result(errorMsg));

            string transactionReference = "1234";

            var validator = GetValidator();

            var result = validator.Validate(t, transferItems, transactionReference);

            result.Success.Should().BeFalse();
            result.Error.Should().Be(errorMsg);
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]

        public void ValidateAmountLessthanZeroError()
        {
            var t = new Transaction(new List<Entities.ProcessedTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                string.Empty);

            List<TransferItem> transferItems = new List<TransferItem>()
            {
                new TransferItem()
                {
                    AccountReference="123",
                    Amount=-100,
                    FundCode="12",
                    FundName="MOCKFUND",
                    Id=Guid.NewGuid()
                }
            };

            _mockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result());

            string transactionReference = "1234";

            var validator = GetValidator();

            var result = validator.Validate(t, transferItems, transactionReference);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("The amount cannot be less than zero");
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]

        public void ValidateAmountIsZeroError()
        {
            var t = new Transaction(new List<Entities.ProcessedTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.PendingTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                new List<Entities.ProcessedTransaction>(),
                string.Empty);

            List<TransferItem> transferItems = new List<TransferItem>()
            {
                new TransferItem()
                {
                    AccountReference="123",
                    Amount=0,
                    FundCode="12",
                    FundName="MOCKFUND",
                    Id=Guid.NewGuid()
                }
            };

            _mockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result());

            string transactionReference = "1234";

            var validator = GetValidator();

            var result = validator.Validate(t, transferItems, transactionReference);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("The amount cannot be zero");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]

        public void ValidateReturnsOK()
        {
            var transactionReference = "123";

            var transaction = BuildTransaction(transactionReference);

            List<TransferItem> transferItems = new List<TransferItem>()
            {
                new TransferItem()
                {
                    AccountReference = transactionReference,
                    Amount=10,
                    FundCode="12",
                    FundName="MOCKFUND",
                    Id=Guid.NewGuid()
                }
            };

            _mockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result());

            var validator = GetValidator();

            var result = validator.Validate(transaction, transferItems, transactionReference);

            result.Success.Should().BeTrue();
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        public Transaction BuildTransaction(string transactionReference)
        {
            return new Transaction(new List<Entities.ProcessedTransaction>()
                {
                    GetTransactionProcessed(150, transactionReference)
                },
                new List<Entities.PendingTransaction>()
                {
                    GetTransactionPending(20, transactionReference)
                },
                new List<Entities.PendingTransaction>()
                {
                    GetTransactionPending(20, transactionReference)
                },
                new List<Entities.ProcessedTransaction>()
                {
                    GetTransactionProcessed(10, transactionReference)
                },
                new List<Entities.ProcessedTransaction>()
                {
                    GetTransactionProcessed(20, transactionReference)
                },
                string.Empty
                );
        }

        private Entities.ProcessedTransaction GetTransactionProcessed(decimal? amount, string transactionReference)
        {
            return new Entities.ProcessedTransaction()
            {
                TransactionReference = transactionReference,
                Amount = amount,
                MopCode = "M1",
                Mop = new Entities.Mop()
                {
                    MopCode = "M1",
                    MetaData = new List<Entities.MopMetaData>()
                }
            };
        }

        private Entities.PendingTransaction GetTransactionPending(decimal? amount, string transactionReference)
        {
            return new Entities.PendingTransaction()
            {
                TransactionReference = transactionReference,
                Amount = amount,
                MopCode = "M1",
                Mop = new Entities.Mop()
                {
                    MopCode = "M1",
                    MetaData = new List<Entities.MopMetaData>()
                }
            };
        }
    }
}
