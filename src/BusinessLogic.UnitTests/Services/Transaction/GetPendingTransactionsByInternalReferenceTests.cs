using BusinessLogic.Entities;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetPendingTransactionsByInternalReferenceTests : BaseTransactionTest
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
                            AccountReference = "test"
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
            var result = service.GetPendingTransactionsByInternalReference("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));
        }

        [TestMethod]
        public void ReturnsCorrectAmountOfData()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetPendingTransactionsByInternalReference("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetPendingTransactionsByInternalReference("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));
            Assert.AreEqual(result.Any(), false);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetPendingTransactionsByInternalReference("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));
            Assert.AreEqual(result.Any(), false);
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
                    var result = service.GetPendingTransactionsByInternalReference("Test");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));

                },
                () =>
                {

                    // Act
                    var result = service.GetPendingTransactionsByInternalReference("Test");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<PendingTransaction>));
                    Assert.AreEqual(result.Any(), false);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
