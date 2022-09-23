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
    public class DistinctTransactionJournalAllocationStrategyTests : BaseTest
    {
        [TestMethod]
        public void ConstructorAcceptsCorrectArguments()
        {
            // Arrange
            SetupUnitOfWork();

            try
            {
                // Act
                var strategy = new DistinctTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockTransactionJournalService.Object,
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

            var strategy = new DistinctTransactionJournalAllocationStrategy(
                    MockLogger.Object,
                    MockUnitOfWork.Object,
                    MockSecurityContext.Object,
                    MockTransactionJournalService.Object,
                    MockJournalAllocationStrategyValidator.Object);

            // Act
            var result = strategy.Execute(GetSuspenseIds(), GetJournals(), GetCreditNotes());

            // Assert
            MockUnitOfWork.Verify(x => x.Suspenses.Unlock(It.IsAny<Guid>()), Times.Once);
        }


    }
}
