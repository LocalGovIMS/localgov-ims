using BusinessLogic.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.Models
{
    [TestClass]
    public class JournalTransactionTests
    {
        private const string CreditPspReference = "PspReference";
        private DateTime _creditEntryDate = new DateTime(2021, 01, 01);
        private Entities.User CreditUser = new Entities.User() { UserId = 1, UserName = "TestUser" };
        private const int CreditUserCode = 1;
        private const string CreditNarrative = "Narrative";
        private const decimal CreditAmount = 10.50M;

        private JournalTransaction _journalTransaction;

        public JournalTransactionTests()
        {
            var credit = new Entities.ProcessedTransaction()
            {
                PspReference = CreditPspReference,
                EntryDate = _creditEntryDate,
                User = CreditUser,
                UserCode = CreditUserCode,
                Narrative = CreditNarrative,
                Amount = CreditAmount
            };

            _journalTransaction = new JournalTransaction(credit, null, null);
        }

        [TestMethod]
        public void PspReference_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.PspReference.Should().Be(CreditPspReference);
        }

        [TestMethod]
        public void EntryDate_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.EntryDate.Should().Be(_creditEntryDate);
        }

        [TestMethod]
        public void User_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.User.Should().Be(CreditUser);
        }

        [TestMethod]
        public void UserCode_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.UserCode.Should().Be(CreditUserCode);
        }

        [TestMethod]
        public void Narrative_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.Narrative.Should().Be(CreditNarrative);
        }

        [TestMethod]
        public void Amount_returns_the_expected_value()
        {
            // Arange

            // Act

            // Assert
            _journalTransaction.Amount.Should().Be(CreditAmount);
        }
    }
}
