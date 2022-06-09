using BusinessLogic.Classes.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.TransactionImportTypeImportProcessingRule;

namespace BusinessLogic.UnitTests.Services.TransactionImportTypeImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.TransactionImportTypeImportProcessingRules.Remove(It.IsAny<Type>()));
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
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void SaveReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.SystemAdmin };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.Delete(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Delete(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsFalse(result.Success);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
