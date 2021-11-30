using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FailPendingTransactionTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.PendingTransactions.GetByInternalReference(
                It.IsAny<string>()))
                .Returns(new List<PendingTransaction>()
                {
                    {
                        new PendingTransaction()
                        {
                            AccountReference = "Reference"
                        }
                    }
                });
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
            var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
        }

        [TestMethod]
        public void ReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionEdit };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.FailPendingTransaction("Reference", "PspReference", "AuthResult");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsFalse(result.Success);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
