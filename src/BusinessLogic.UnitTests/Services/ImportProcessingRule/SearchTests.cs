using BusinessLogic.Models.ImportProcessingRule;
using BusinessLogic.Models.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.ImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTests : BaseImportProcessingRuleTest
    {
        private void SetupUnitOfWork()
        {
            var test = 1;

            MockUnitOfWork.Setup(x => x.ImportProcessingRules.Search(It.IsAny<SearchCriteria>(), out test))
                .Returns(new List<Entities.ImportProcessingRule>()
                    {
                        new Entities.ImportProcessingRule()
                        {
                            Id = 1,
                            Conditions = new List<Entities.ImportProcessingRuleCondition>(),
                            Actions = new List<Entities.ImportProcessingRuleAction>()
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.ImportProcessingRule>));
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.ImportProcessingRule>));
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.ImportProcessingRule>));
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
