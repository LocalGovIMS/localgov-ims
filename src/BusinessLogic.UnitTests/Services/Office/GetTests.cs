using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.Office
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetByVatCodeTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Offices.SingleOrDefault(It.IsAny<Expression<Func<Entities.Office, bool>>>()))
                .Returns(new Entities.Office());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Get("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.Office));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.Get("Test");

            // Assert
            Assert.IsNull(result);
        }
    }
}
