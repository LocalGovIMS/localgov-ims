using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UpdateCardDetailsTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Transactions.Find(
                It.IsAny<Expression<Func<ProcessedTransaction, bool>>>()))
                .Returns(new List<ProcessedTransaction>()
                    {
                    new ProcessedTransaction()
                    {
                        AccountReference = "Reference"
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
        public void ReturnsSuccessWhenUpdateIssuccessful()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.UpdateCardDetails(new BusinessLogic.Models.Transactions.UpdateCardDetailsArgs() { MerchantReference = "12345" });

            // Assert
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public void ReturnsFailureWhenUpdateFails()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork();
            var service = GetService();

            // Setup a failure
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>())).Throws(new InvalidOperationException());

            // Act
            var result = service.UpdateCardDetails(new BusinessLogic.Models.Transactions.UpdateCardDetailsArgs() { MerchantReference = "12345" });

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.AreEqual("Unable to update the card details for transactions with internal reference: 12345", result.Error);
        }
    }
}
