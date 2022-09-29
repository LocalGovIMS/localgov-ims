using BusinessLogic.Classes;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Refund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RefundTransactionsTests : BaseRefundTest
    {

        private readonly RefundService _service;
        private const string RefundableMopCode = "90";

        public RefundTransactionsTests()
        {
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService();
            _service = GetService();
        }

        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Roles.GetAll())
                .Returns(new List<Entities.Role>());
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private void SetupTransactionService()
        {
            MockTransactionService.Setup(x => x.GetPendingRefunds(It.IsAny<string>()))
                .Returns(new List<PendingTransaction>());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange

            // Act
            var result = _service.RefundTransaction(new RefundRequest(), "Overcharged");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
        }

        [TestMethod]
        public void OnErrorReturnsPaymentResponseWithErrorMessage()
        {
            // Arrange

            // Act
            var result = _service.RefundTransaction(new RefundRequest(), "Overcharged");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual("Refund amount must be greater than 0", result.Message);
        }

        [TestMethod]
        public void CantStartNewRefundWithRefundInProgress()
        {
            // Arrange
            MockTransactionService.Setup(x => x.GetPendingRefunds(It.IsAny<string>()))
                .Returns(new List<PendingTransaction>() { new PendingTransaction() });

            // Act
            var result = _service.RefundTransaction(new RefundRequest()
            {
                RefundAmount = (decimal)10.00,
                TransactionReference = "TESTREFUND"
            }, "User error");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("Payment has pending refunds, please wait for them to complete and try again", result.Message);
        }

        [TestMethod]
        public void CantRefundWithoutReason()
        {
            // Arrange

            // Act
            var result = _service.RefundTransaction(new RefundRequest()
            {
                RefundAmount = (decimal)10.00,
                TransactionReference = "TESTREFUND"
            }, String.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("You must provide a reason for the refund", result.Message);
        }

        [TestMethod]
        public void CantRefundMoreThanRemainingAmount()
        {
            // Arrange
            MockTransactionService.Setup(x => x.GetTransaction(It.IsAny<string>()))
                .Returns(new ProcessedTransaction());
            MockTransactionService.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>()))
                .Returns(SetupTransaction(10, 0, 0, 0));

            // Act
            var result = _service.RefundTransaction(new RefundRequest()
            {
                RefundAmount = (decimal)12.00,
                TransactionReference = "TESTREFUND"
            }, "Paid in error");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("Refund amount is greater than remaining transaction amount", result.Message);
        }

        [TestMethod]
        public void CantRefundToDifferentPspReferencesInSameRequest()
        {
            // Arrange
            MockTransactionService.Setup(x => x.GetTransaction("TESTREFUND"))
                .Returns(GetTransactionProcessed("PSP1", true));

            MockTransactionService.Setup(x => x.GetTransaction("TESTREFUND2"))
                .Returns(GetTransactionProcessed("PSP2", true));

            MockTransactionService.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>()))
                .Returns(SetupTransaction(30, 0, 0, 0));

            // Act
            var result = _service.RefundTransactions(new List<RefundRequest>()
            {
                new RefundRequest() {RefundAmount = (decimal) 10.00, TransactionReference = "TESTREFUND"},
                new RefundRequest() {RefundAmount = (decimal) 10.00, TransactionReference = "TESTREFUND2"}
            }, "Paid in error");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("Transaction contains refunds for multiple PSP References", result.Message);
        }

        [TestMethod]
        public void CantRefundNonRefundableMop()
        {
            // Arrange
            MockTransactionService.Setup(x => x.GetTransaction(It.IsAny<string>()))
                .Returns(GetTransactionProcessed("PSP1", false));

            MockTransactionService.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>()))
                .Returns(SetupTransaction(12, 0, 0, 0));

            // Act
            var result = _service.RefundTransaction(new RefundRequest()
            {
                RefundAmount = (decimal)10.00,
                TransactionReference = "TESTREFUND"
            }, "Paid in error");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("You can't refund a payment that hasn't been processed via the payment provider", result.Message);
        }

        [TestMethod]
        public void CantDoPartialRefundWithoutPartialRefundPermission()
        {
            // Arrange

            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.TransactionRefund))
                .Returns(true);

            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.TransactionPartialRefund))
                .Returns(false);

            MockTransactionService.Setup(x => x.GetTransaction(It.IsAny<string>()))
                .Returns(GetTransactionProcessed("PSP1", true));

            MockTransactionService.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>()))
                .Returns(SetupTransaction(12, 0, 0, 0));

            // Act
            var result = _service.RefundTransaction(new RefundRequest()
            {
                RefundAmount = (decimal)6.00,
                TransactionReference = "TESTREFUND"
            }, "Paid in error");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RefundStatus));
            Assert.AreEqual(RefundStatusType.Error, result.Status);
            Assert.AreEqual("You don't have permission to perform a partial refund", result.Message);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.RefundTransactions(new List<RefundRequest>(), "Overcharged");

            // Assert
            Assert.IsNull(result);
        }

        private ProcessedTransaction GetTransactionProcessed(string pspReference, bool isRefundableMop)
        {
            return new ProcessedTransaction()
            {
                PspReference = pspReference,
                MopCode = RefundableMopCode,
                Mop = new Mop()
                {
                    MopCode = RefundableMopCode,
                    Metadata = new List<MopMetadata>()
                    {
                        new MopMetadata()
                        {
                            MopMetadataKey = new MopMetadataKey()
                            {
                                Name = MopMetadataKeys.IsARefundablePayment
                            },
                            Value = isRefundableMop.ToString(),
                            MopCode = RefundableMopCode
                        }
                    }
                },
                Amount = 12
            };
        }

        private BusinessLogic.Models.Transaction SetupTransaction(
            decimal transactionValue,
            decimal processedRefundValue,
            decimal pendingRefundValue,
            decimal transferredValue)
        {
            var transaction = new BusinessLogic.Models.Transaction(
                new List<ProcessedTransaction>(),
                new List<PendingTransaction>(),
                new List<PendingTransaction>(),
                new List<ProcessedTransaction>(),
                new List<ProcessedTransaction>(),
                "TESTPSP");

            if (transactionValue > 0)
            {
                transaction.TransactionLines.Add(new ProcessedTransaction()
                {
                    TransactionReference = "TESTREFUND",
                    Amount = transactionValue,
                    MopCode = RefundableMopCode,
                });

                transaction.TransactionLines.Add(new ProcessedTransaction()
                {
                    TransactionReference = "TESTREFUND2",
                    Amount = transactionValue
                });
            }

            if (processedRefundValue > 0)
            {
                transaction.ProcessedRefunds.Add(new ProcessedTransaction()
                {
                    Amount = processedRefundValue
                });
            }

            if (pendingRefundValue > 0)
            {
                transaction.PendingRefunds.Add(new PendingTransaction()
                {
                    Amount = pendingRefundValue
                });
            }

            if (transferredValue > 0)
            {
                transaction.Transfers.Add(new ProcessedTransaction()
                {
                    Amount = transferredValue
                });
            }

            return transaction;
        }
    }
}
