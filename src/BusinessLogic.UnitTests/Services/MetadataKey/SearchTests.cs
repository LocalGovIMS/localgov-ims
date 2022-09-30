using BusinessLogic.Enums;
using BusinessLogic.Models.MetadataKey;
using BusinessLogic.Models.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.MetadataKey
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SearchTests : BaseMetadataKeyTest
    {
        private void SetupUnitOfWork()
        {
            var test = 1;

            MockUnitOfWork.Setup(x => x.MetadataKeys.Search(It.IsAny<SearchCriteria>(), out test))
                .Returns(new List<Entities.MetadataKey>()
                    {
                        new Entities.MetadataKey()
                        {
                            Name = "Name",
                            EntityType = (byte)MetadataKeyEntityType.Mop,
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.MetadataKey>));
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.MetadataKey>));
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
            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.MetadataKey>));
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

        //[TestMethod]
        //public void CheckPermissions()
        //{
        //    // Arrange
        //    var roles = new List<string>() { Security.Role.SystemAdmin };
        //    var service = GetService();
        //    SetupUnitOfWork();

        //    var helper = new PermissionTestHelper(
        //        MockSecurityContext,
        //        roles,
        //        () => {

        //            // Act
        //            var result = service.Search(new SearchCriteria());

        //            // Assert
        //            Assert.IsNotNull(result);
        //            Assert.IsInstanceOfType(result, typeof(SearchResult<Entities.MetadataKey>));

        //        },
        //        () => {

        //            // Act
        //            var result = service.Search(new SearchCriteria());

        //            // Assert
        //            Assert.IsNull(result);

        //        });

        //    // Act
        //    helper.CheckRoles();
        //}
    }
}
