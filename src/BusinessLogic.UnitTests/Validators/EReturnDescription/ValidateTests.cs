using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.EReturnDescription
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseEReturnDescriptionTest
    {
        private const string DefaultReference = "ABC123";
        private const string DefaultDescription = "DefaultDescription";

        private void SetupService(bool descriptionOverride)
        {
            MockEReturnTemplateRowService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Entities.TemplateRow
                    {
                        Description = DefaultDescription,
                        DescriptionOverride = descriptionOverride,
                        Reference = DefaultReference
                }
                );
        }

        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [TestMethod]
        public void WhenDescriptionIsNullOrEmptyThenTheExpectedResultIsReturned(string description)
        {
            // Arrange
            SetupService(false);
            var validator = GetValidator();
            
            // Act
            var result = validator.Validate(description, 1);

            // Assert
            result.Success.Should().BeFalse();
        }

        [DataRow("An non matching description", false)]
        [DataRow(DefaultDescription, true)]
        [TestMethod]
        public void WhenDescriptionCannotBeOverwrittenThenThenExpectedResultIsReturned(string description, bool success)
        {
            // Arrange
            SetupService(false);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(description, 1);

            // Assert
            result.Success.Should().Be(success);
        }

        [DataRow("An non matching description")]
        [DataRow("Another description")]
        [TestMethod]
        public void WhenDescriptionCannotBeOverwrittenAndTheDescriptionDoesNotMatchThenTheExpectedErrorMessageIsReturned(string description)
        {
            // Arrange
            SetupService(false);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(description, 1);

            // Assert
            result.Error.Should().Be($"Description '{DefaultReference}' cannot be overridden, but is set to '{description}'");
        }

        [DataRow("A description")]
        [DataRow("Another description")]
        [DataRow("Yet another description")]
        [TestMethod]
        public void WhenDescriptionCanBeOverriddenThenTheExpectedResultIsReturned(string description)
        {
            // Arrange
            SetupService(true);
            var validator = GetValidator();

            // Act
            var result = validator.Validate(description, 1);

            // Assert
            result.Success.Should().BeTrue();
        }
    }
}

