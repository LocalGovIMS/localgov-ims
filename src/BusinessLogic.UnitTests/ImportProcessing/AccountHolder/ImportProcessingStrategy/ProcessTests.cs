using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        [TestMethod]
        public void Process_WhenCalled_CallsAccountHolderServiceBulksSelectNotExisting_Once()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderRepository.Verify(x => x.BulkSelectNotExisting(It.IsAny<IEnumerable<Entities.AccountHolder>>()), Times.Once);
        }

        [TestMethod]
        public void Process_WhenCalledWithRowsThatNeedInserting_CallsAccountHolderServiceBulksInsert_Once()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderRepository.Verify(x => x.BulkInsert(It.IsAny<IEnumerable<Entities.AccountHolder>>()), Times.Once);
        }

        [TestMethod]
        public void Process_WhenCalledWithRowsThatNeedUpdating_CallsAccountHolderServiceBulksUpdate_Once()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderRepository.Verify(x => x.BulkUpdate(It.IsAny<IEnumerable<Entities.AccountHolder>>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [TestMethod]
        [DataRow("An error")]
        [DataRow("Another error")]
        public void Process_WhenInsertFails_AddsAnErrorToTheImport(string errorMessage)
        {
            // Arrange
            SetupDependenciesForFailure(errorMessage);
            SetupStrategy();
            var args = GetArgs();

            // Act
            Strategy.Process(args);

            // Assert
            args.Import.Errors().Count.Should().Be(1);
            args.Import.Errors().First().Message.Should().Be(errorMessage);
        }
    }
}
