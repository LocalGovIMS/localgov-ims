using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.VatMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetMetadataTests : BaseVatMetadataTest
    {
        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetMetadata();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<BusinessLogic.Models.Metadata>));
        }
    }
}
