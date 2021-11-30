using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Vat
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetByVatCodeTests : BaseVatTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Vats.GetVatByVatCode(It.IsAny<string>()))
                .Returns(new Entities.Vat());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetByVatCode("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.Vat));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetByVatCode("Test");

            // Assert
            Assert.IsNull(result);
        }
    }
}
