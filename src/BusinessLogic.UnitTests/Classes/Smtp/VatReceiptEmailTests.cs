using BusinessLogic.Classes.Smtp.Emails;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Classes.Smtp
{
    [TestClass]
    public class VatReceiptEmailTests
    {
        private const string emailFrom = "test@test.com";
        private readonly Mock<List<PendingTransaction>> _mockPendingRefunds = new Mock<List<PendingTransaction>>();
        private readonly Mock<List<PendingTransaction>> _mockFailedRefunds = new Mock<List<PendingTransaction>>();
        private readonly Mock<List<ProcessedTransaction>> _mockProcessedRefunds = new Mock<List<ProcessedTransaction>>();
        private readonly Mock<List<ProcessedTransaction>> _mockTransfers = new Mock<List<ProcessedTransaction>>();
        private List<ProcessedTransaction> _processedTransactions = new List<ProcessedTransaction>();
        private VatReceiptEmail _email;

        public VatReceiptEmailTests()
        {
            SetupProcessedTransactions();
            SetupEmail();
        }

        private void SetupProcessedTransactions()
        {
            _processedTransactions = new List<ProcessedTransaction>()
            {
                new ProcessedTransaction()
                {
                    Amount = -10,
                    AccountReference= "123",
                    ReceiptIssued = false,
                    PspReference = "123456789",
                    VatCode = "12",
                    VatAmount = 0,
                    Vat = new Vat()
                    {
                        VatCode = "test code",
                        Percentage = 0
                    },
                    CardHolderPremiseNumber = "12",
                    CardHolderStreet = "test road",
                    CardHolderTown = "new town",
                    CardHolderPostCode = "S12 2SA",
                    Fund = new Fund()
                    {
                        FundName= "test fund"
                    },
                    MopCode = "90",
                    Mop = new Mop() {
                        MopCode = "90",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsARefundablePayment
                                },
                                Value = "True"
                            }
                        }
                    }
                }
            };
        }

        private void SetupEmail()
        {
            VatReceiptArgs args = new VatReceiptArgs(
                emailFrom,
                new BusinessLogic.Models.Transaction
                (
                    _processedTransactions,
                    _mockPendingRefunds.Object,
                    _mockFailedRefunds.Object,
                    _mockProcessedRefunds.Object,
                    _mockTransfers.Object,
                    string.Empty
                ));

            _email = new VatReceiptEmail(args);
        }

        [TestMethod]
        public void Email_body_should_contains_PSP_Reference()
        {
            // Arrange

            // Act

            // Assert
            _email.Email.Body.Should().Contain(_processedTransactions[0].PspReference);
        }

        [TestMethod]
        public void Email_subject_for_a_refund_is_as_expected()
        {
            // Arrange

            // Act

            // Assert
            _email.Email.Subject.Should().Contain("Refund confirmation");
        }

        // TODO: Could test more of this email
    }
}
