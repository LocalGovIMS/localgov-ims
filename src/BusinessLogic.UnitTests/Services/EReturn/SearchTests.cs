using BusinessLogic.Enums;
using BusinessLogic.Models;
using BusinessLogic.Models.EReturns;
using BusinessLogic.Models.Shared;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.EReturn;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTests : BaseTest
    {
        private void SetupUnitOfWork(int resultCount)
        {
            MockUnitOfWork.Setup(x => x.EReturns.Search(It.IsAny<SearchCriteria>(), out resultCount))
                .Returns(new List<Type>() {
                    {
                        new Type() {
                            Id = 1
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Fund>() {
                    new Entities.Fund()
                    {
                        FundCode = "13",
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = FundMetadataKeys.IsAnEReturnDefaultFund
                                },
                                Value = "True"
                            }
                        }
                    }
                });
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private SearchCriteria GetEReturnSearchCriteria()
        {
            return new SearchCriteria()
            {
                Page = 1,
                PageSize = 20
            };
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            int resultCount = 0;
            SetupUnitOfWork(resultCount);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SearchTransactions(GetEReturnSearchCriteria());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SearchResult<EReturnWrapper>));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            int resultCount = 0;
            SetupUnitOfWork(resultCount);
            SetupSecurityContext(true);

            MockUnitOfWork.Setup(x => x.EReturns.Search(It.IsAny<SearchCriteria>(), out resultCount))
                .Throws(new NullReferenceException());

            var service = GetService();

            // Act
            var result = service.SearchTransactions(GetEReturnSearchCriteria());

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            int resultCount = 0;
            SetupUnitOfWork(resultCount);
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.SearchTransactions(GetEReturnSearchCriteria());

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.EReturnAuthoriser, BusinessLogic.Security.Role.EReturns };
            int resultCount = 0;
            SetupUnitOfWork(resultCount);
            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.SearchTransactions(GetEReturnSearchCriteria());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(SearchResult<EReturnWrapper>));

                },
                () =>
                {

                    // Act
                    var result = service.SearchTransactions(GetEReturnSearchCriteria());

                    // Assert
                    Assert.IsNull(result);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
