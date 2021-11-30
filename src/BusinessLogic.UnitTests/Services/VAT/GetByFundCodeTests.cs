using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Vat
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetByFundCodeTests : BaseVatTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Funds.GetByFundCode(It.IsAny<string>()))
                .Returns(new Entities.Fund()
                {
                    Vat = new Entities.Vat()
                });
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetByFundCode("Test");

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
            var result = service.GetByFundCode("Test");

            // Assert
            Assert.IsNull(result);
        }
    }
}
