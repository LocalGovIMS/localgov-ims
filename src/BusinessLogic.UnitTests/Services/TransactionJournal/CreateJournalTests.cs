using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.TransactionJournal
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateJournalTests : BaseTransactionJournalTest
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

            MockUnitOfWork.Setup(x => x.Transactions.GetByTransactionReference(
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
                                MetadataKey = new Entities.MetadataKey()
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
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournal
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            var processedTransactions = new List<ProcessedTransaction>();
            var pendingTransactions = new List<PendingTransaction>();
            var failedRefunds = new List<PendingTransaction>();
            var processedRefunds = new List<ProcessedTransaction>();
            var transfers = new List<ProcessedTransaction>();

            MockTransactionService.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>())).Returns(
                new BusinessLogic.Models.Transaction(
                    processedTransactions,
                    pendingTransactions,
                    failedRefunds,
                    processedRefunds,
                    transfers,
                    string.Empty));

            MockTransactionJournalValidator.Setup(x => x.Validate(
                It.IsAny<BusinessLogic.Models.Transaction>(),
                It.IsAny<List<TransferItem>>(),
                It.IsAny<string>()))
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
            var result = service.CreateJournal(new TransferItem()
                , new TransferItem()
                , new List<TransferItem>() { new TransferItem() }
                , "TransactionReference"
                , new DateTime());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void AddsPositiveTransferToUnitOfWork()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.CreateJournal(new TransferItem()
            {
                Amount = (decimal)10.15,
                FundCode = "13",
                Narrative = "Test narrative"
            }
                , new TransferItem()
                , new List<TransferItem>() { new TransferItem() }
                , "TransactionReference"
                , new DateTime());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
            MockUnitOfWork.Verify(x => x.Transactions.Add(It.Is<ProcessedTransaction>(
                y => y.Amount == (decimal)10.15
                     && y.Narrative == "Test narrative"
                     && y.FundCode == "13")));
        }

        [TestMethod]
        public void AddsPositiveTransferOutToUnitOfWork()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.CreateJournal(new TransferItem()
                , new TransferItem()
                {
                    Amount = (decimal)-10.15,
                    FundCode = "13",
                    Narrative = "Test narrative"
                }
                , new List<TransferItem>() { new TransferItem() }
                , "TransactionReference"
                , new DateTime());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
            MockUnitOfWork.Verify(x => x.Transactions.Add(It.Is<ProcessedTransaction>(
                y => y.Amount == (decimal)-10.15
                     && y.Narrative == "Test narrative"
                     && y.FundCode == "13")));
        }

        [TestMethod]
        public void AddsPositiveCreditNoteToUnitOfWork()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.CreateJournal(new TransferItem()
                , new TransferItem()
                , new List<TransferItem>() { new TransferItem()
                {
                    Amount = (decimal) 10.15,
                    FundCode = "13",
                    Narrative = "Test narrative"
                } }
                , "TransactionReference"
                , new DateTime());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
            MockUnitOfWork.Verify(x => x.Transactions.Add(It.Is<ProcessedTransaction>(
                y => y.Amount == (decimal)-10.15
                     && y.Narrative == "Test narrative"
                     && y.FundCode == "13")));
        }


    }
}
