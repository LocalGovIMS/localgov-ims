using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.ImportProcessingRuleOperator
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTests : BaseImportProcessingRuleOperatorTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.ImportProcessingRuleOperators.GetAll())
                .Returns(new List<Entities.ImportProcessingRuleOperator>() { new Entities.ImportProcessingRuleOperator() });
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.ImportProcessingRuleOperator>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNull(result);
        }
    }
}
