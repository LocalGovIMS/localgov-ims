using BusinessLogic.Entities;
using BusinessLogic.Models;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SavePendingTransactionsTests : BaseTransactionTest
    {
        private void SetupUnitOfWork(bool setVatRate)
        {
            MockUnitOfWork.Setup(x => x.TransactionNotifications.Add(It.IsAny<TransactionNotification>()));

            MockUnitOfWork.Setup(x => x.PendingTransactions.AddRange(It.IsAny<IEnumerable<PendingTransaction>>()));

            var vat = new Entities.Vat()
            {
                VatCode = "V1",
                Percentage = setVatRate == true
                    ? (decimal?)17.5M
                    : null
            };

            MockUnitOfWork.Setup(x => x.Funds.GetByFundCode(
                It.IsAny<string>()))
                .Returns(new Entities.Fund()
                {
                    Vat = vat
                });

            MockUnitOfWork.Setup(x => x.Vats.GetVatByVatCode(
                It.IsAny<string>()))
                .Returns(vat);
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(
                new List<PendingTransaction>()
                {
                    new PendingTransaction() {
                        AccountReference = "test",
                        FundCode = "F1",
                        VatCode = "V1",
                        Amount = 75
                    }
                }
                , "source");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(new List<PendingTransaction>(), "source");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(new List<PendingTransaction>(), "source");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FundCodePickedBySourceWhenPendingTransacionFundCodeIsEmpty()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(
                new List<PendingTransaction>()
                {
                    new PendingTransaction() {
                        AccountReference = "test"
                    }
                }
                , "source");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void VatCodePickedByFundCodeWhenPendingTranscationHasNoVatCode()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(
                new List<PendingTransaction>()
                {
                    new PendingTransaction() {
                        AccountReference = "test",
                        FundCode = "F1",
                    }
                }
                , "source");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void WhenVatPercentageIsNullZeroIsUsed()
        {
            // Arrange
            SetupUnitOfWork(false);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SavePendingTransactions(
                new List<PendingTransaction>()
                {
                    new PendingTransaction() {
                        AccountReference = "test",
                        FundCode = "F1",
                        Amount = 75
                    }
                }
                , "source");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionCreate, BusinessLogic.Security.Role.TransactionRefund, BusinessLogic.Security.Role.Payments };
            var service = GetService();
            SetupUnitOfWork(true);

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.SavePendingTransactions(
                        new List<PendingTransaction>()
                        {
                            new PendingTransaction() {
                                AccountReference = "test",
                                FundCode = "F1",
                                VatCode = "V1",
                                Amount = 75
                            }
                        }
                        , "source");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Response));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.SavePendingTransactions(new List<PendingTransaction>(), "source");

                    // Assert
                    Assert.IsNull(result);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
