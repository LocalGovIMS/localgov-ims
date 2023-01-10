//using BusinessLogic.UnitTests.Helpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using Type = BusinessLogic.Entities.Template;

//namespace BusinessLogic.UnitTests.Services.Template
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class GetAllMopsTests : BaseTest
//    {
//        private void SetupUnitOfWork()
//        {
//            MockUnitOfWork.Setup(x => x.Templates.GetAll())
//                .Returns(new List<Type>());
//        }

//        private void SetupSecurityContext(bool result)
//        {
//            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
//                .Returns(result);
//        }

//        [TestMethod]
//        public void ReturnsCorrectType()
//        {
//            // Arrange
//            SetupUnitOfWork();
//            SetupSecurityContext(true);
//            var service = GetService();

//            // Act
//            var result = service.GetAllTemplates();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(List<Type>));
//        }

//        [TestMethod]
//        public void OnErrorReturnsEmptyList()
//        {
//            // Arrange
//            SetupSecurityContext(true);
//            var service = GetService();

//            // Act
//            var result = service.GetAllTemplates();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(List<Type>));
//            Assert.AreEqual(result.Count, 0);
//        }

//        [TestMethod]
//        public void IncorrectSecurityReturnsEmptyList()
//        {
//            // Arrange
//            SetupSecurityContext(false);
//            var service = GetService();

//            // Act
//            var result = service.GetAllTemplates();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result, typeof(List<Type>));
//            Assert.AreEqual(result.Count, 0);
//        }

//        [TestMethod]
//        public void CheckPermissions()
//        {
//            // Arrange
//            var roles = new List<string>() { BusinessLogic.Security.Role.SystemAdmin };
//            var service = GetService();
//            SetupUnitOfWork();

//            var helper = new PermissionTestHelper(
//                MockSecurityContext,
//                roles,
//                () =>
//                {

//                    // Act
//                    var result = service.GetAllTemplates();

//                    // Assert
//                    Assert.IsNotNull(result);
//                    Assert.IsInstanceOfType(result, typeof(List<Type>));

//                },
//                () =>
//                {

//                    // Act
//                    var result = service.GetAllTemplates();

//                    // Assert
//                    Assert.IsNotNull(result);
//                    Assert.IsInstanceOfType(result, typeof(List<Type>));
//                    Assert.AreEqual(result.Count, 0);

//                });

//            // Act
//            helper.CheckRoles();
//        }
//    }
//}
