using BusinessLogic.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateHppPaymentTests : BasePaymentTest
    {

        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Roles.GetAll())
                .Returns(new List<Entities.Role>());
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
            var result = service.CreateHppPayment(new PaymentDetails());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PaymentResponse));
        }

        [TestMethod]
        public void OnErrorReturnsPaymentResponseWithErrorMessage()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            MockFundService.Setup(x => x.GetAllFunds()).Throws(new Exception("An error occured"));
            var service = GetService();

            // Act
            var result = service.CreateHppPayment(new PaymentDetails());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PaymentResponse));
            Assert.AreEqual("An error occured", result.ErrorMessage);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.CreateHppPayment(new PaymentDetails());

            // Assert
            Assert.IsNull(result);
        }

    }
}
