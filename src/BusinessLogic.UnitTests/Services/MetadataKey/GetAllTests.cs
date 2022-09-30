using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.MetadataKey
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllTests : BaseMetadataKeyTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.MetadataKeys.GetAll())
                .Returns(new List<Entities.MetadataKey>());
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
            Assert.IsInstanceOfType(result, typeof(List<Entities.MetadataKey>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.MetadataKey>));
        }
    }
}
