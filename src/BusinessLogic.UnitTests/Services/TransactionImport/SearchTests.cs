using BusinessLogic.Models.TransactionImport;
using BusinessLogic.Models.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.TransactionImport
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            var outInt = 0;
            MockUnitOfWork.Setup(x => x.TransactionImports.Search(It.IsAny<SearchCriteria>(), out outInt))
                .Returns(new List<Entities.TransactionImport>());
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.TransactionImport>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria());

            // Assert
            Assert.IsNull(result);
        }
    }
}
