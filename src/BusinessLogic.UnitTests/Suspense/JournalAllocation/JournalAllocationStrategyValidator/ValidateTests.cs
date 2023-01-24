using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Suspense.JournalAllocation;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Suspense.JournalAllocation.JournalAllocationStrategyValidator
{
    [TestClass]
    public class ValidateTests
    {
        private Mock<IFundService> _mockFundService = new Mock<IFundService>();
        
        private const string SuspenseJournalFundCode = "01";
        private const string CreditNoteEnabledFundCode = "0V";
        private const string NonCreditNoteEnabledFundCode = "0I";

        public ValidateTests()
        {
            _mockFundService.Setup(x => x.GetAllFunds())
                .Returns(new List<Entities.Fund>()
                {
                    new Entities.Fund()
                    {
                        FundCode = SuspenseJournalFundCode,
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = FundMetadataKeys.IsASuspenseTransactionFund
                                },
                                Value = "True",
                                FundCode = SuspenseJournalFundCode
                            }
                        }
                    },
                    new Entities.Fund()
                    {
                        FundCode = CreditNoteEnabledFundCode,
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = FundMetadataKeys.IsACreditNoteEnabledFund
                                },
                                Value = "True",
                                FundCode = CreditNoteEnabledFundCode
                            }
                        }
                    }
                });
        }

        private JournalAllocationStrategyValidatorArgs GetArgs()
        {
            return new JournalAllocationStrategyValidatorArgs()
            {
                Suspenses = new List<BusinessLogic.Models.Suspense.SuspenseWrapper>()
                {
                    new BusinessLogic.Models.Suspense.SuspenseWrapper(new Entities.Suspense() { Amount = 10 })
                },
                CreditNotes = new List<BusinessLogic.Models.Suspense.CreditNote>()
                {
                    new BusinessLogic.Models.Suspense.CreditNote() { Amount = 10, FundCode = CreditNoteEnabledFundCode }
                },
                JournalItems = new List<BusinessLogic.Models.Suspense.Journal>()
                {
                    new BusinessLogic.Models.Suspense.Journal() { Amount = 20 }
                }
            };
        }

        private IJournalAllocationStrategyValidator GetValidator()
        {
            return new BusinessLogic.Suspense.JournalAllocation.JournalAllocationStrategyValidator(_mockFundService.Object);
        }

        [TestMethod]
        public void Validate_throws_an_exception_when_no_suspense_items_are_specified()
        {
            // Arrange
            var args = GetArgs();
            var validator = GetValidator();

            args.Suspenses = null;

            // Act
            Action act = () => validator.Validate(args);

            // Assert
            act.Should()
                .Throw<SuspenseJournalAllocationException>()
                .WithMessage("You must choose some suspense items");
        }

        [TestMethod]
        public void Validate_throws_an_exception_when_the_sum_of_the_amount_remaing_to_allocate_is_less_than_or_equal_to_zero()
        {
            // Arrange
            var args = GetArgs();
            var validator = GetValidator();

            args.Suspenses = new List<BusinessLogic.Models.Suspense.SuspenseWrapper>() 
            { 
                new BusinessLogic.Models.Suspense.SuspenseWrapper(new Entities.Suspense() { Amount = 0 }) 
            };

            // Act
            Action act = () => validator.Validate(args);

            // Assert
            act.Should()
                .Throw<SuspenseJournalAllocationException>()
                .WithMessage("Value of chosen suspense items must be greater than zero");
        }

        [TestMethod]
        public void Validate_throws_an_exception_when_the_combined_value_of_the_suspense_items_and_credit_notes_does_not_match_the_value_to_be_journalled()
        {
            // Arrange
            var args = GetArgs();
            var validator = GetValidator();

            args.JournalItems = new List<BusinessLogic.Models.Suspense.Journal>()
            {
                new BusinessLogic.Models.Suspense.Journal() { Amount = 30 }
            };

            // Act
            Action act = () => validator.Validate(args);

            // Assert
            act.Should()
                .Throw<SuspenseJournalAllocationException>()
                .WithMessage("Value of chosen suspense items and credit notes must match amount to be journalled");
        }

        [TestMethod]
        public void Validate_throws_an_exception_when_the_journal_items_is_being_journalled_back_to_the_suspense_journal_fund()
        {
            // Arrange
            var args = GetArgs();
            var validator = GetValidator();

            args.JournalItems = new List<BusinessLogic.Models.Suspense.Journal>()
            {
                new BusinessLogic.Models.Suspense.Journal() { Amount = 20, FundCode = SuspenseJournalFundCode }
            };

            // Act
            Action act = () => validator.Validate(args);

            // Assert
            act.Should()
                .Throw<SuspenseJournalAllocationException>()
                .WithMessage("You cannot journal a suspense item back to suspense");
        }

        [TestMethod]
        public void Validate_throws_an_exception_when_the_credit_notes_are_for_none_credit_note_funds()
        {
            // Arrange
            var args = GetArgs();
            var validator = GetValidator();

            args.CreditNotes = new List<BusinessLogic.Models.Suspense.CreditNote>()
            {
                new BusinessLogic.Models.Suspense.CreditNote() { Amount = 10, FundCode = NonCreditNoteEnabledFundCode }
            };

            // Act
            Action act = () => validator.Validate(args);

            // Assert
            act.Should()
                .Throw<SuspenseJournalAllocationException>()
                .WithMessage("Credit notes exist with invalid funds");
        }
    }
}
