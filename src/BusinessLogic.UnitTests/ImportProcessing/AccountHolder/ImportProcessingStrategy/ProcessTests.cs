using BusinessLogic.ImportProcessing;
using BusinessLogic.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        [TestMethod]
        public void Process_WhenTheAccountHolderExists_CallsAccountHolderServiceUpdateOnce()
        {
            // Arrange
            SetupDependenciesForExistingAccountHolder();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderService.Verify(x => x.Update(It.IsAny<UpdateAccountHolderArgs>()), Times.Once);
        }

        public void Process_WhenTheAccountHolderExists_DoesNotCallAccountHolderServiceCreate()
        {
            // Arrange
            SetupDependenciesForExistingAccountHolder();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderService.Verify(x => x.Create(It.IsAny<CreateAccountHolderArgs>()), Times.Never);
        }

        public void Process_WhenTheAccountHolderDoesNotExists_CallsAccountHolderServiceCreateOnce()
        {
            // Arrange
            SetupDependenciesForNewAccountHolder();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs());

            // Assert
            MockAccountHolderService.Verify(x => x.Create(It.IsAny<CreateAccountHolderArgs>()), Times.Never);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Another Test")]
        public void Process_WhenAccountHolderCreationFails_ThrowsAnExceptionWithTheResultError(string errorMessage)
        {
            // Arrange
            SetupDependenciesForAccountHolderCreationFailure(errorMessage);
            SetupStrategy();

            // Act
            Action action = () => Strategy.Process(GetArgs());

            // Assert
            action.Should()
                .Throw<ImportProcessingException>()
                .WithMessage(errorMessage);
        }
    }
}
