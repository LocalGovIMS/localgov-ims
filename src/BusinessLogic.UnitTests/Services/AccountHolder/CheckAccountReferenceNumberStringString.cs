//using BusinessLogic.UnitTests.Helpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq.Expressions;

//namespace BusinessLogic.UnitTests.Services.AccountHolder
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class CheckAccountReferenceNumberStringString : BaseAccountHolderTest
//    {
//        private void SetupUnitOfWork()
//        {
//            MockUnitOfWork.Setup(x => x.AccountHolders.Find(It.IsAny<Expression<Func<Entities.AccountHolder, bool>>>()))
//                .Returns(new List<Entities.AccountHolder>()
//                {
//                    new Entities.AccountHolder()
//                });

//            MockUnitOfWork.Setup(x => x.Origins.Find(It.IsAny<Expression<Func<Entities.Origin, bool>>>()))
//                .Returns(new List<Entities.Origin>()
//                {
//                    new Entities.Origin()
//                    {
//                        MessageName = "Test"
//                    }
//                });
//        }

//        private void SetupSecurityContext(bool result)
//        {
//            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.Payments)).Returns(true);
//        }

//        private void SetupSecurityContext(string role, bool result)
//        {
//            MockSecurityContext.Reset();
//            MockSecurityContext.Setup(x => x.IsInRole(role)).Returns(result);
//        }

//        [TestMethod]
//        public void ReturnsCorrectType()
//        {
//            // Arrange
//            SetupUnitOfWork();
//            SetupSecurityContext(true);
//            var service = GetService();

//            // Act
//            var result = service.CheckAccountReferenceNumber("Test", "Test");

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(string));
//            Assert.AreEqual(result, "Test");
//        }

//        [TestMethod]
//        public void ReturnsCorrectTypeWithNoFund()
//        {
//            // Arrange
//            SetupUnitOfWork();
//            SetupSecurityContext(true);
//            var service = GetService();

//            // Act
//            var result = service.CheckAccountReferenceNumber("Test", null);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(string));
//            Assert.AreEqual(result, "Test");
//        }

//        [TestMethod]
//        public void OnErrorReturnsNull()
//        {
//            // Arrange
//            SetupSecurityContext(true);
//            var service = GetService();

//            // Act
//            var result = service.CheckAccountReferenceNumber("Test", "Test");

//            // Assert
//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public void IncorrectSecurityReturnsNull()
//        {
//            // Arrange
//            SetupSecurityContext(false);
//            var service = GetService();

//            // Act
//            var result = service.CheckAccountReferenceNumber("Test", "Test");

//            // Assert
//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public void NoMatchingAccountHoldersReturnsNull()
//        {
//            // Arrange
//            SetupUnitOfWork();

//            // This mocks not fnding any records
//            MockUnitOfWork.Setup(x => x.AccountHolders.Find(It.IsAny<Expression<Func<Entities.AccountHolder, bool>>>()))
//                .Returns(new List<Entities.AccountHolder>());

//            SetupSecurityContext(true);

//            var service = GetService();

//            // Act
//            var result = service.CheckAccountReferenceNumber("Test", "Test");

//            // Assert
//            Assert.IsNull(result);
//        }

//        [TestMethod]
//        public void CheckPermissions()
//        {
//            // Arrange
//            var roles = new List<string>() { BusinessLogic.Security.Role.Payments };
//            var service = GetService();
//            SetupUnitOfWork();

//            var helper = new PermissionTestHelper(
//                MockSecurityContext, 
//                roles,
//                () => {

//                    // Assert
//                    var passResult = service.CheckAccountReferenceNumber("Test", "Test");

//                    Assert.IsNotNull(passResult);
//                    Assert.IsInstanceOfType(passResult, typeof(string));
//                    Assert.AreEqual(passResult, "Test");

//                },
//                () => {

//                    // Assert
//                    var failResult = service.CheckAccountReferenceNumber("Test", "Test");
//                    Assert.IsNull(failResult);

//                });

//            // Act
//            helper.CheckRoles();
//        }
//    }
//}
