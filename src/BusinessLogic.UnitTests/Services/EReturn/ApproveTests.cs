using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.EReturn;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ApproveTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Fund>() {
                    new Entities.Fund()
                    {
                        FundCode = "13",
                        MetaData = new List<Entities.FundMetaData>()
                        {
                            new Entities.FundMetaData()
                            {
                                Key = FundMetaDataKeys.IsAnEReturnDefaultFund,
                                Value = "True"
                            }
                        }
                    }
                });
        }

        private void SetupApproveEReturnsStrategy()
        {
            MockApproveEReturnsStrategy.Setup(x => x.Execute(It.IsAny<List<Tuple<int, string>>>()))
                .Returns(new Result());
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
            var result = service.Create(new Type());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void CheckExceptionReturnsCorrectResult()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork();
            SetupApproveEReturnsStrategy();

            var service = GetService();

            // Act
            var result = service.Approve(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.EReturnAuthoriser };
            SetupUnitOfWork();
            SetupApproveEReturnsStrategy();

            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.Approve(new List<int>() { 1, 2 });

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Approve(new List<int>() { 1, 2 });

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
