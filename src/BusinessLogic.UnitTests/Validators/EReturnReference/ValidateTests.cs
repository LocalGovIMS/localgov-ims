using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.EReturnReference
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseEReturnReferenceTest
    {
        private const string StandardReference = "123456";
        private const string WildcardReference = "***456";

        private const string ValidReferenceForStandardReference = StandardReference;
        private const string InvalidReferenceForStandardReferenceAtPosition1 = "_23456";
        private const string InvalidReferenceForStandardReferenceAtPosition3 = "12_456";
        private const string InvalidReferenceForStandardReferenceAtPosition5 = "1234_6";

        private const string ValidReferenceForWildcardReference = "123456";
        private const string InvalidReferenceForWildcardReferenceAtPosition2 = "1A3456";
        private const string InvalidReferenceForWildcardReferenceAtPosition3 = "12A456";

        private void SetupService(string reference, bool referenceOverride)
        {
            MockEReturnTemplateRowService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Entities.TemplateRow
                    {
                        ReferenceOverride = referenceOverride,
                        Reference = reference
                }
                );
        }

        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [TestMethod]
        public void WhenReferenceIsNullOrEmptyThenTheExpectedResultIsReturned(string reference)
        {
            // Arrange
            SetupService(StandardReference, false);
            var validator = GetValidator();
            
            // Act
            var result = validator.Validate(reference, 1);

            // Assert
            result.Success.Should().BeFalse();
        }

        [DataRow("An non matching reference", false)]
        [DataRow(StandardReference, true)]
        [TestMethod]
        public void WhenReferenceCannotBeOverwrittenThenThenExpectedResultIsReturned(string reference, bool success)
        {
            // Arrange
            SetupService(StandardReference, false);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(reference, 1);

            // Assert
            result.Success.Should().Be(success);
        }

        [DataRow("An non matching reference")]
        [DataRow("Another reference")]
        [TestMethod]
        public void WhenReferenceCannotBeOverwrittenAndTheReferenceDoesNotMatchThenTheExpectedErrorMessageIsReturned(string reference)
        {
            // Arrange
            SetupService(StandardReference, false);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(reference, 1);

            // Assert
            result.Error.Should().Be($"Reference '{StandardReference}' cannot be overridden, but is set to '{reference}'");
        }

        [DataRow(InvalidReferenceForStandardReferenceAtPosition1, 1)]
        [DataRow(InvalidReferenceForStandardReferenceAtPosition3, 3)]
        [DataRow(InvalidReferenceForStandardReferenceAtPosition5, 5)]
        [TestMethod]
        public void WhenStandardReferenceIsOverridableButAnInvalidReferenceIsProvidedThenTheExpectedResultIsReturned(string reference, int position)
        {
            // Arrange
            SetupService(StandardReference, true);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(reference, 1);

            // Assert
            result.Error.Should().Be($"Reference '{reference}' is invalid for mask '{StandardReference}' at position {position}");
        }

        [DataRow(InvalidReferenceForWildcardReferenceAtPosition2)]
        [DataRow(InvalidReferenceForWildcardReferenceAtPosition3)]
        [TestMethod]
        public void WhenWildcardReferenceIsOverridableButAnInvalidReferenceIsProvidedThenTheExpectedResultIsReturned(string reference)
        {
            // Arrange
            SetupService(WildcardReference, true);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(reference, 1);

            // Assert
            result.Error.Should().Be($"{reference} is not a valid reference - it must only contain digits");
        }

        [DataRow(StandardReference, ValidReferenceForStandardReference)]
        [DataRow(WildcardReference, ValidReferenceForWildcardReference)]
        [TestMethod]
        public void WhenAValidReferenceIsSuppliedThenTheExpectedResultIsReturned(string reference, string referenceToTest)
        {
            // Arrange
            SetupService(reference, true);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(referenceToTest, 1);

            // Assert
            result.Success.Should().BeTrue();
        }

        //[DataRow("A reference")]
        //[DataRow("Another reference")]
        //[DataRow("Yet another reference")]
        //[TestMethod]
        //public void WhenReferenceCanBeOverriddenThenTheExpectedResultIsReturned(string description)
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

