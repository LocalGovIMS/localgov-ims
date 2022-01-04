using BusinessLogic.Classes.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.SuspenseNote
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateTests : BaseSuspenseNoteTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.SuspenseNotes.Add(It.IsAny<Entities.SuspenseNote>()));
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
            var result = service.Create(new Entities.SuspenseNote());

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
            var result = service.Create(new Entities.SuspenseNote());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Create(new Entities.SuspenseNote());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.Create(new Entities.SuspenseNote());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.Finance };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.Create(new Entities.SuspenseNote());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.AreEqual(result.Success, true);

                },
                () =>
                {

                    // Act
                    var result = service.Create(new Entities.SuspenseNote());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.AreEqual(result.Success, false);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
