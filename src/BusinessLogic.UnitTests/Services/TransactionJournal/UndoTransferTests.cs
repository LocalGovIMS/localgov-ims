using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.TransactionJournal
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UndoTransferTests : BaseTransactionJournalTest
    {
        private void SetupUnitOfWork(bool isValid)
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetByAppReference(
                It.IsAny<string>()))
                .Returns(new ProcessedTransaction()
                {
                    AccountReference = "test",
                }
                 );

            MockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Mop>() {
                    new Mop()
                    {
                        MopCode = "JR",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MopMetadataKey = new MopMetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
                                Value = "True"
                            }
                        }
                    },
                    new Mop()
                    {
                        MopCode = "JN",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MopMetadataKey = new MopMetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournal
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockTransactionService.Setup(x => x.GetTransfers(It.IsAny<string>()))
                .Returns(new List<ProcessedTransaction>()
                {
                    {  new ProcessedTransaction() { Amount = 1 } }
                });

            MockRollbackTransactionJournalValidator.Setup(x => x.Validate(It.IsAny<List<ProcessedTransaction>>()))
                .Returns(isValid ? new Result() : new Result("Not Valid"));
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
            var result = service.UndoTransfer("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void OnErrorReturnsResult()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(true);

            MockTransactionService.Setup(x => x.GetTransfers(It.IsAny<string>()))
                .Throws(new NullReferenceException());

            var service = GetService();

            // Act
            var result = service.UndoTransfer(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void InvalidTransactionsReturnResult()
        {
            // Arrange
            SetupUnitOfWork(false);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.UndoTransfer("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NullTransactionAmountReturnsResult()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);

            MockTransactionService.Setup(x => x.GetTransfers(It.IsAny<string>()))
                .Returns(new List<ProcessedTransaction>()
                {
                    {  new ProcessedTransaction() { Amount = null } }
                });

            var service = GetService();

            // Act
            var result = service.UndoTransfer("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            SetupUnitOfWork(true);
            var service = GetService();

            // Act
            var result = service.UndoTransfer("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionJournal };
            SetupUnitOfWork(true);
            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.UndoTransfer("Test");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.UndoTransfer("Test");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsFalse(result.Success);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
