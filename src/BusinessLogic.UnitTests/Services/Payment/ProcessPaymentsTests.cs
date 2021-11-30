using BusinessLogic.Classes;
using BusinessLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessPaymentsTests : BasePaymentTest
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
            var result = service.ProcessPayments(new List<PaymentDetails>()
            {
                new PaymentDetails(), new PaymentDetails()
            }, PaymentTypeEnum.Cheque);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessPaymentResponse));
        }

        [TestMethod]
        public void ChequeWithoutChequeSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.PostPayment))
                .Returns(true);
            var service = GetService();

            // Act
            var result = service.ProcessPayments(new List<PaymentDetails>(), PaymentTypeEnum.Cheque);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostWithoutPostSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.ChequeProcess))
                .Returns(true);
            var service = GetService();

            // Act
            var result = service.ProcessPayments(new List<PaymentDetails>(), PaymentTypeEnum.Post);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.ProcessPayments(new List<PaymentDetails>(), PaymentTypeEnum.Cheque);

            // Assert
            Assert.IsNull(result);
        }

    }
}
