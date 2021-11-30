using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Vat
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllCodesTests : BaseVatTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Vats.GetAll())
                .Returns(new List<Entities.Vat>()
                {
                    new Entities.Vat()
                });
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetAllCodes();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.Vat>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetAllCodes();

            // Assert
            Assert.IsNull(result);
        }
    }
}
