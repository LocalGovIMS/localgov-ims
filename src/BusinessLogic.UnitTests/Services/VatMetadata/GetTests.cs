using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.VatMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTests : BaseVatMetadataTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.VatMetadatas.Get(
                It.IsAny<int>()))
                .Returns(new Entities.VatMetaData());
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
            var result = service.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.VatMetaData));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Get(1);

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
            var result = service.Get(1);

            // Assert
            Assert.IsNull(result);
        }
    }
}
