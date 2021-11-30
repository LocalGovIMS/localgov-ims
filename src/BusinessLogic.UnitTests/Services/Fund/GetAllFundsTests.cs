using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Fund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllFundsTests : BaseFundTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Funds.GetAll(false))
                .Returns(new List<Entities.Fund>());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetAllFunds();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.Fund>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetAllFunds();

            // Assert
            Assert.IsNull(result);
        }
    }
}
