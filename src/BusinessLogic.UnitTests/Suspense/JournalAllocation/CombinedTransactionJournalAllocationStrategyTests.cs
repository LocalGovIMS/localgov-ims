using BusinessLogic.Suspense.JournalAllocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Suspense.JournalAllocation
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CombinedTransactionJournalAllocationStrategyTests : BaseTest
    {
        [TestMethod]
        public void ConstructorAcceptsCorrectArguments()
        {
            // Arrange
            SetupUnitOfWork();

            try
            {
                // Act
                var strategy = new CombinedTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockSuspenseJournalService.Object,
                    MockJournalAllocationStrategyValidator.Object);
            }
            catch (Exception)
            {
                // Assert;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Execute_locks_the_suspense_items()
        {
            // Arrange
            SetupUnitOfWork();

            var strategy = new DistinctTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockTransactionJournalService.Object,
                    MockJournalAllocationStrategyValidator.Object);

            // Act
            var result = strategy.Execute(GetSuspenseIds(), GetJournals(), GetCreditNotes());

            // Assert
            MockUnitOfWork.Verify(x => x.Suspenses.Lock(It.IsAny<List<int>>(), It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        public void Execute_unlocks_the_suspense_items()
        {
            // Arrange
            SetupUnitOfWork();

            var strategy = new CombinedTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockSuspenseJournalService.Object,
                    MockJournalAllocationStrategyValidator.Object);

            // Act
            var result = strategy.Execute(GetSuspenseIds(), GetJournals(), GetCreditNotes());

            // Assert
            MockUnitOfWork.Verify(x => x.Suspenses.Unlock(It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod]
        [DataRow(1, 1, 1, 3)]
        [DataRow(1, 2, 1, 4)]
        [DataRow(1, 1, 2, 4)]
        [DataRow(1, 2, 2, 5)]
        [DataRow(1, 1, 0, 2)]
        public void Execute_creates_the_expected_number_of_ProcessedTransactions(int numberOfSuspenseItems, int numberOfJournals, int numberOfCreditNotes, int expectedNuberOfJournalsCreated)
        {
            // Arrange
            SetupUnitOfWork();

            var strategy = new CombinedTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockSuspenseJournalService.Object,
                    MockJournalAllocationStrategyValidator.Object);

            // Act
            var result = strategy.Execute(GetSuspenseIds(numberOfSuspenseItems), GetJournals(numberOfJournals), GetCreditNotes(numberOfCreditNotes));

            // Assert
            MockSuspenseJournalService.Verify(x => x.CreateJournal(
                It.IsAny<BusinessLogic.Models.TransferItem>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()), Times.Exactly(expectedNuberOfJournalsCreated));
        }
    }
}
