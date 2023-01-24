using BusinessLogic.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Models.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.ImportTypeImportProcessingRule;

namespace BusinessLogic.UnitTests.Services.ImportTypeImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTransactionsTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            int test;
            
            MockUnitOfWork.Setup(x => x.ImportTypeImportProcessingRules.Search(It.IsAny<SearchCriteria>(), out test))
                .Returns(new List<Entities.ImportTypeImportProcessingRule>()
                    {
                        new Entities.ImportTypeImportProcessingRule()
                        {
                            Id = 1
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
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Type>));
        }

        [TestMethod]
        public void ReturnsSameResultPage()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria() { Page = 1 });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Type>));
            Assert.AreEqual(result.Page, 1);
        }

        [TestMethod]
        public void ReturnsSameResultPageSize()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Search(new SearchCriteria() { PageSize = 20 });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<Type>));
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
