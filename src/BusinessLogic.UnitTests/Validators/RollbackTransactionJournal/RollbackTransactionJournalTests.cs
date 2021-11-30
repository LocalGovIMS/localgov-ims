using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.RollbackTransactionJournal
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RollbackTransactionJournalTests
    {
        private BusinessLogic.Validators.RollbackTransactionJournalValidator GetValidator()
        {
            return new BusinessLogic.Validators.RollbackTransactionJournalValidator();
        }

        [TestMethod]
        public void ValidateNullModelReturnsError()
        {
            var validator = GetValidator();

            var result = validator.Validate(null);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to find the transfers to rollback");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void TransactionAllreadyRolledBack()
        {
            List<ProcessedTransaction> transactions = new List<ProcessedTransaction>()
            {
                new ProcessedTransaction()
                {
                     TransferRollbackGuid= Guid.NewGuid().ToString()

                }
            };

            var validator = GetValidator();

            var result = validator.Validate(transactions);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Transfer has already been rolled back");
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]
        public void ValidationIsValid()
        {
            List<ProcessedTransaction> transactions = new List<ProcessedTransaction>()
            {
                new ProcessedTransaction()
                {
                }
            };

            var validator = GetValidator();

            var result = validator.Validate(transactions);

            result.Success.Should().BeTrue();
            result.Should().BeOfType(expectedType: typeof(Result));
        }
    }
}
