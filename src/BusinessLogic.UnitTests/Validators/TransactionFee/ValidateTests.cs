using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BusinessLogic.UnitTests.Validators.TransactionFee
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseTransactionFeeTest
    {
        private const decimal ValidFee = 12.22m;
        private const decimal InvalidFee = 0m;
        
        private const string DefaultMopCode = "M1";
        private const string CardPaymentFeeMopCode = "CCF";

        private void SetupService(List<Entities.ProcessedTransaction> transactions)
        {
            MockTransactionService.Setup(x => x.GetTransactionsByInternalReference(It.IsAny<string>()))
                .Returns(transactions);
        }

        [TestMethod]
        public void WhenNoArgsAreProvidedThenTheExpectedResultIsReturned()
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(null);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("No args have been provided");
        }

        [TestMethod]
        public void WhenAnInvalidFeeIsProvidedThenTheExpectedResultIsReturned()
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgsWithAnInvalidFee());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The fee is zero, so does not require processing");
        }

        [TestMethod]
        public void WhenAnInvalidMopCodeIsProvidedThenTheExpectedResultIsReturned()
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgsWithAnInvalidMop());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The method of payment doesn't incur a fee");
        }

        [TestMethod]
        public void WhenTheTransactionHasNotYetBeenProcessedAsNoTransactionsAreFoundThenTheExpectedResultIsReturned()
        {
            // Arrange
            SetupService(null);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgs());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The transaction has not yet been processed");
        }

        [TestMethod]
        public void WhenTheTransactionHasNotYetBeenProcessedBecuaseAnEmptyListOfTransactionsHasBeenFoundThenTheExpectedResultIsReturned()
        {
            // Arrange
            SetupService(new List<Entities.ProcessedTransaction>());
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgs());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The transaction has not yet been processed");
        }

        [TestMethod]
        public void WhenAFeeTransactionAlreadyExistsThenTheExpectedResultIsReturned()
        {
            // Arrange
            SetupService(GetATransactionThatAlreadyHasAFeeRecorded());
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgs());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("A fee transaction already exists");
        }

        [TestMethod]
        public void WhenTheTransactionFeeIsValidThenTheExpectedResultIsReturned()
        {
            // Arrange
            SetupService(GetATransactionThatHasNoFeeRecorded());
            var validator = GetValidator();

            // Act
            var result = validator.Validate(GetArgs());

            // Assert
            result.Success.Should().BeTrue();
        }


        private TransactionFeeValidatorArgs GetArgs()
        {
            return new TransactionFeeValidatorArgs()
            {
                PaymentResult = new BusinessLogic.Classes.PaymentResult()
                {
                    Fee = ValidFee
                },
                Mop = new Entities.Mop()
                {
                    MopCode = DefaultMopCode,
                    Metadata = new List<Entities.MopMetadata>()
                    {
                        new Entities.MopMetadata()
                        {
                            MetadataKey = new Entities.MetadataKey()
                            {
                                Name = MopMetadataKeys.IncursAFee
                            },
                            Value = "True"
                        }
                    }
                },
                CardPaymentFeeMopCode = CardPaymentFeeMopCode
            };
        }

        private TransactionFeeValidatorArgs GetArgsWithAnInvalidFee()
        {
            var args = GetArgs();
            
            args.PaymentResult.Fee = InvalidFee;

            return args;
        }

        private TransactionFeeValidatorArgs GetArgsWithAnInvalidMop()
        {
            var args = GetArgs();

            args.Mop.Metadata.First(x => x.MetadataKey.Name.Equals(MopMetadataKeys.IncursAFee)).SetPropertyValue<string>("Value", "False");

            return args;
        }

        private List<Entities.ProcessedTransaction> GetATransactionThatAlreadyHasAFeeRecorded()
        {
            var test = new List<Entities.ProcessedTransaction>
            {
                new Entities.ProcessedTransaction()
                {
                    Amount = ValidFee,
                    MopCode = CardPaymentFeeMopCode
                }
               
            };

            return test;
        }

        private List<Entities.ProcessedTransaction> GetATransactionThatHasNoFeeRecorded()
        {
            var test = new List<Entities.ProcessedTransaction>
            {
                new Entities.ProcessedTransaction()
                {
                    Amount = 1,
                    MopCode = DefaultMopCode
                }

            };

            return test;
        }

        //[DataRow(null)]
        //[DataRow("")]
        //[DataRow(" ")]
        //[TestMethod]
        //public void WhenDescriptionIsNullOrEmptyThenTheExpectedResultIsReturned(string description)
        //{
        //    // Arrange
        //    SetupService(false);
        //    var validator = GetValidator();

        //    // Act
        //    var result = validator.Validate(description, 1);

        //    // Assert
        //    result.Success.Should().BeFalse();
        //}

        //[DataRow("An non matching description", false)]
        //[DataRow(DefaultDescription, true)]
        //[TestMethod]
        //public void WhenDescriptionCannotBeOverwrittenThenThenExpectedResultIsReturned(string description, bool success)
        //{
        //    // Arrange
        //    SetupService(false);
        //    var validator = GetValidator();

        //    // Act
        //    var result = validator.Validate(description, 1);

        //    // Assert
        //    result.Success.Should().Be(success);
        //}

        //[DataRow("An non matching description")]
        //[DataRow("Another description")]
        //[TestMethod]
        //public void WhenDescriptionCannotBeOverwrittenAndTheDescriptionDoesNotMatchThenTheExpectedErrorMessageIsReturned(string description)
        //{
        //    // Arrange
        //    SetupService(false);
        //    var validator = GetValidator();

        //    // Act
        //    var result = validator.Validate(description, 1);

        //    // Assert
        //    result.Error.Should().Be($"Description '{DefaultReference}' cannot be overridden, but is set to '{description}'");
        //}

        //[DataRow("A description")]
        //[DataRow("Another description")]
        //[DataRow("Yet another description")]
        //[TestMethod]
        //public void WhenDescriptionCanBeOverriddenThenTheExpectedResultIsReturned(string description)
        //{
        //    // Arrange
        //    SetupService(true);
        //    var validator = GetValidator();

        //    // Act
        //    var result = validator.Validate(description, 1);

        //    // Assert
        //    result.Success.Should().BeTrue();
        //}
    }
}

