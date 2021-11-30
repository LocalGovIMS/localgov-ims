using BusinessLogic.Classes.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.Template;

namespace BusinessLogic.UnitTests.Services.Template
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UpdateTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Templates.Update(It.IsAny<Type>()));
            MockUnitOfWork.Setup(x => x.TemplateRows.Update(It.IsAny<Type>()));
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private void SetupTemplateRowValidator(string error)
        {
            MockTemplateRowValidator.Setup(x => x.Validate(It.IsAny<Entities.TemplateRow>()))
                .Returns(string.IsNullOrEmpty(error) ? new Result() : new Result(error));
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(new Type());

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
            SetupTemplateRowValidator(string.Empty);

            var service = GetService();

            // Act
            var result = service.Update(new Type()
            {
                TemplateRows = new List<Entities.TemplateRow>()
                {
                    new Entities.TemplateRow()
                    {
                        Id = 0
                    }
                }
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
        }


        [TestMethod]
        public void TemaplteRowErrorReturnsFailure()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupTemplateRowValidator("Error");

            var service = GetService();

            // Act
            var result = service.Update(new Type()
            {
                TemplateRows = new List<Entities.TemplateRow>()
                {
                    new Entities.TemplateRow()
                    {
                        Id = 0
                    }
                }
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(new Type());

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
            var result = service.Update(new Type());

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
                    var result = service.Update(new Type());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Update(new Type());

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
