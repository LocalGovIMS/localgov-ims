using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.Models.Transactions;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessPaymentTests : BasePaymentTest
    {
        private string _incursAFee = "False";

        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Roles.GetAll())
                .Returns(new List<Entities.Role>());

            MockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Mop>() {
                    new Mop()
                    {
                        MopCode = "90",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsACardSelfServicePayment
                                },
                                Value = "True"
                            }
                        }
                    },
                    new Mop()
                    {
                        MopCode = "PF",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsACardPaymentFee
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Mops.GetMop(It.IsAny<string>()))
                .Returns(new Mop()
                {
                    MopCode = "90",
                    Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsACardSelfServicePayment
                                },
                                Value = "True"
                            },
                            new MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IncursAFee
                                },
                                Value = _incursAFee
                            }
                        }
                }
                );
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private void SetupTransactionService(PendingTransaction pendingTransaction)
        {
            MockTransactionService.Setup(x =>
                    x.GetPendingTransactionsByInternalReference(pendingTransaction.InternalReference))
                .Returns(new List<PendingTransaction>() { pendingTransaction });

            MockTransactionService.Setup(x =>
                x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()))
                .Returns(new Response() { Success = true });

            MockTransactionService.Setup(x =>
                x.SuspendPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Result());

            MockTransactionService.Setup(x =>
                x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Result());
        }

        private void SetupValidator()
        {
            MockTransactionFeeValidator.Setup(x => x.Validate(It.IsAny<TransactionFeeValidatorArgs>()))
                .Returns(new Result());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                SuccessUrl = "http://www.test.com/success"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Authorised });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
        }

        [TestMethod]
        public void OnErrorReturnsFalseAndFailUrl()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                FailUrl = "http://www.test.com/fail"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Error });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual("http://www.test.com/fail", result.RedirectUrl);
            MockTransactionService.Verify(x => x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()), Times.Never);
            MockTransactionService.Verify(x => x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void OnRefusedReturnsFalseAndFailUrl()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                FailUrl = "http://www.test.com/fail"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Refused });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual("http://www.test.com/fail", result.RedirectUrl);
            MockTransactionService.Verify(x => x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()), Times.Never);
            MockTransactionService.Verify(x => x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }


        [TestMethod]
        public void OnSuccessReturnsSuccessUrl()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                SuccessUrl = "http://www.test.com/success"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Authorised, PspReference = "1234" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual("http://www.test.com/success/1234", result.RedirectUrl);
            MockTransactionService.Verify(x => x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()), Times.Once);
            MockTransactionService.Verify(x => x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OnPendingReturnsSuccessUrl()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                SuccessUrl = "http://www.test.com/success"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Pending });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual("http://www.test.com/success", result.RedirectUrl);
            MockTransactionService.Verify(x => x.SuspendPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            MockTransactionService.Verify(x => x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()), Times.Never);
            MockTransactionService.Verify(x => x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OnCancelReturnCancelUrl()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                CancelUrl = "http://www.test.com/cancel"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Cancelled });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual("http://www.test.com/cancel", result.RedirectUrl);
            MockTransactionService.Verify(x => x.FailPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            MockTransactionService.Verify(x => x.SuspendPendingTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            MockTransactionService.Verify(x => x.AuthorisePendingTransactionByInternalReference(It.IsAny<AuthorisePendingTransactionByInternalReferenceArgs>()), Times.Never);
        }

        [TestMethod]
        public void TestLegacyFlagSetCorrectly()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                CancelUrl = "http://www.test.com/cancel",
                Legacy = true
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Cancelled });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
            Assert.AreEqual(true, result.IsLegacy);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Authorised });

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void When_the_fee_is_zero_a_processed_transaction_for_it_is_not_added_to_the_database()
        {
            // Arrange
            _incursAFee = "True";
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                SuccessUrl = "http://www.test.com/success"
            });
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Authorised, Fee = 0 });

            // Assert
            MockUnitOfWork.Verify(x => x.Transactions.Add(It.IsAny<ProcessedTransaction>()), Times.Never);
        }

        [TestMethod]
        public void When_the_fee_greater_than_zero_a_processed_transaction_for_it_is_added_to_the_database()
        {
            // Arrange
            _incursAFee = "True";
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTransactionService(new PendingTransaction()
            {
                SuccessUrl = "http://www.test.com/success"
            });
            SetupValidator();
            var service = GetService();

            // Act
            var result = service.ProcessPayment(new PaymentResult() { AuthResult = ResponseCode.Authorised, Fee = 0.50M });

            // Assert
            MockTransactionService.Verify(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()), Times.Once);
        }

    }
}
