using BusinessLogic.Classes.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Suspense
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateSuspenseTests : TestBase
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Suspenses.Add(It.IsAny<Entities.Suspense>()));
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Create(new Entities.Suspense());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void SaveReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Create(new Entities.Suspense());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.Create(new Entities.Suspense());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }
    }
}
