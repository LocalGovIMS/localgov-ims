using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Office
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Offices.GetAll())
                .Returns(new List<Entities.Office>()
                {
                    new Entities.Office()
                });
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.Office>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNull(result);
        }
    }
}
