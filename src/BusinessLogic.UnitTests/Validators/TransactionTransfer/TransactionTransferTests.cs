using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.TransactionTransfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransactionTransferTests
    {
        Mock<IAccountReferenceValidator> MockAccountReferenceValidator = new Mock<IAccountReferenceValidator>();
        private BusinessLogic.Validators.TransactionTransferValidator GetValidator()
        {
            return new BusinessLogic.Validators.TransactionTransferValidator(MockAccountReferenceValidator.Object);
        }

        private BusinessLogic.Models.TransferItem SetupTranserItem(decimal amount)
        {
            BusinessLogic.Models.TransferItem transferItem = new BusinessLogic.Models.TransferItem()
            {
                AccountReference = "123",
                FundCode = "23",
                Amount = amount,
                FundName = "MOCKFUND",
                Id = Guid.NewGuid()
            };

            return transferItem;
        }

        private List<BusinessLogic.Models.TransferItem> SetupTransferItems(decimal amount)
        {
            List<BusinessLogic.Models.TransferItem> transferItems = new List<BusinessLogic.Models.TransferItem>()
            {
                new BusinessLogic.Models.TransferItem()
                {
                    AccountReference = "123",
                    FundCode = "23",
                    Amount = amount,
                    FundName = "MOCKTransferItem",
                    Id = Guid.NewGuid()
                }
            };

            return transferItems;
        }

        [TestMethod]
        public void ValidateNullTransferItemReturnsError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(100);

            var validator = GetValidator();

            var result = validator.Validate(transferItem, null);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to find any valid transfers");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateEmptyTransferItemReturnsError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(100);

            List<BusinessLogic.Models.TransferItem> transferItems = new List<BusinessLogic.Models.TransferItem>();

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to find any valid transfers");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateSourceItemResultReturnsError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(100);

            List<BusinessLogic.Models.TransferItem> transferItems = SetupTransferItems(10);

            string errorMsg = "Mock error Message";
            MockAccountReferenceValidator.Setup(x => x.ValidateReference(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<AccountReferenceValidationSource>()))
                .Returns(new Result(errorMsg));

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeFalse();
            result.Error.Should().Be(errorMsg);
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void AmountLessThanZeroReturnsError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(-100);

            List<BusinessLogic.Models.TransferItem> transferItems = SetupTransferItems(10);

            MockAccountReferenceValidator.Setup(x => x.ValidateReference(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<AccountReferenceValidationSource>()))
                .Returns(new Result());

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("The amount cannot be less than zero");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void AmountIsZeroReturnsError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(0);

            List<BusinessLogic.Models.TransferItem> transferItems = SetupTransferItems(10);

            MockAccountReferenceValidator.Setup(x => x.ValidateReference(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<AccountReferenceValidationSource>()))
                .Returns(new Result());

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("The amount cannot be zero");
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]
        public void TotalTransferExceedsSumError()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(10);

            List<BusinessLogic.Models.TransferItem> transferItems = SetupTransferItems(100);

            MockAccountReferenceValidator.Setup(x => x.ValidateReference(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<AccountReferenceValidationSource>()))
                .Returns(new Result());

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeFalse();
            result.Error.Should().Be("The total amount to journal (£100.00) is greater than the amount available to journal (£10.00)");
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateReturnsSuccessfully()
        {
            BusinessLogic.Models.TransferItem transferItem = SetupTranserItem(100);

            List<BusinessLogic.Models.TransferItem> transferItems = SetupTransferItems(10);

            MockAccountReferenceValidator.Setup(x => x.ValidateReference(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<AccountReferenceValidationSource>()))
                .Returns(new Result());

            var validator = GetValidator();

            var result = validator.Validate(transferItem, transferItems);

            result.Success.Should().BeTrue();
            result.Should().BeOfType(expectedType: typeof(Result));
        }

    }
}
