﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.FundMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetStringFundCodeStringKeyTests : BaseFundMetadataTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.FundMetadatas.Get(
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Entities.FundMetaData());
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Get("FC", "Key");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.FundMetaData));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Get("FC", "Key");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.Get("FC", "Key");

            // Assert
            Assert.IsNull(result);
        }
    }
}
