using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTransactionByPspReferenceTests : BaseTransactionTest
    {
        private const string CardViaStaffMopCode = "92";
        private const string JournalReallocationMopCode = "JR";

        private void SetupUnitOfWork(bool returnTransactions, string transactionMopCode)
        {
            SetupUnitOfWork(returnTransactions, transactionMopCode, "Test", "Test");
        }

        private void SetupUnitOfWork(bool returnTransactions, string transactionMopCode, string mopMetadataKey, string mopMetadataValue)
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetByPspReference(
                It.IsAny<string>()))
                .Returns(returnTransactions
                    ? new List<ProcessedTransaction>()
                        {
                            new ProcessedTransaction()
                            {
                                MopCode = transactionMopCode,
                                Mop = new Mop()
                                {
                                    MopCode = transactionMopCode,
                                    Metadata = new List<MopMetadata>()
                                    {
                                        new MopMetadata
                                        {
                                            MetadataKey = new MetadataKey()
                                            {
                                                Name = mopMetadataKey
                                            },
                                            Value = mopMetadataValue,
                                            MopCode = transactionMopCode
                                        }
                                    }
                                }
                            }
                        }
                    : new List<ProcessedTransaction>()
                 );

            MockUnitOfWork.Setup(x => x.PendingTransactions.GetPendingRefunds(
                It.IsAny<string>()))
                .Returns(new List<PendingTransaction>()
                    {
                        new PendingTransaction()
                        {
                            AccountReference = "TransactionReference"
                        }
                    }
                 );

            MockUnitOfWork.Setup(x => x.Transactions.GetProcessedRefunds(
            It.IsAny<string>()))
            .Returns(new List<ProcessedTransaction>()
                {
                    new ProcessedTransaction()
                    {
                        AccountReference = "TransactionReference"
                    }
                }
             );
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
            SetupUnitOfWork(true, CardViaStaffMopCode);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BusinessLogic.Models.Transaction));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NoTransactionsReturnsNull()
        {
            // Arrange
            SetupUnitOfWork(false, CardViaStaffMopCode);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void NullPspReferenceReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnsNullIfMopCodeIsJournalReAllocation()
        {
            // Arrange
            SetupUnitOfWork(true, JournalReallocationMopCode, MopMetadataKeys.IsAJournalReallocation, "True");
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnsNullIfMopCodeIsJournal()
        {
            // Arrange
            SetupUnitOfWork(true, JournalReallocationMopCode, MopMetadataKeys.IsAJournal, "True");
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransactionByPspReference("Test");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionList };
            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    SetupUnitOfWork(true, CardViaStaffMopCode);

                    // Act
                    var result = service.GetTransactionByPspReference("Test");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(BusinessLogic.Models.Transaction));

                },
                () =>
                {
                    SetupUnitOfWork(false, CardViaStaffMopCode);

                    // Act
                    var result = service.GetTransactionByPspReference("Test");

                    // Assert
                    Assert.IsNull(result);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
