using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllCheckDigitConfigurationsTests : BaseCheckDigitConfigurationTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.CheckDigitConfigurations.GetAll())
                .Returns(new List<Entities.CheckDigitConfiguration>());
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
            Assert.IsInstanceOfType(result, typeof(List<Entities.CheckDigitConfiguration>));
        }
    }
}
