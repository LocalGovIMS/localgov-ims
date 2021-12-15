using BusinessLogic.Models.VatMetadata;
using BusinessLogic.Models.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.VatMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTests : BaseVatMetadataTest
    {
        private void SetupUnitOfWork()
        {
            var test = 1;

            MockUnitOfWork.Setup(x => x.VatMetadatas.Search(It.IsAny<SearchCriteria>(), out test))
                .Returns(new List<Entities.VatMetaData>()
                    {
                        new Entities.VatMetaData()
                        {
                            Id = 1,
                            Key = "Key",
                            Value = "Test"
                        }
                    }
                 );
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
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.VatMetaData>));
        }

        [TestMethod]
        public void ReturnsSameResultPage()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria() { Page = 1 });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.VatMetaData>));
            Assert.AreEqual(result.Page, 1);
        }

        [TestMethod]
        public void ReturnsSameResultPageSize()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria() { PageSize = 20 });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.VatMetaData>));
            Assert.AreEqual(result.PageSize, 20);
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria());

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
            var result = service.Search(new SearchCriteria());

            // Assert
            Assert.IsNull(result);
        }
    }
}
