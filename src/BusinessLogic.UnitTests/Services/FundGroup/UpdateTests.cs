using BusinessLogic.Classes.Result;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.FundGroup
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UpdateTests : BaseFundGroupTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.FundGroups.SingleOrDefault(
                 It.IsAny<Expression<Func<Entities.FundGroup, bool>>>()))
                 .Returns(new Entities.FundGroup());

            MockUnitOfWork.Setup(x => x.FundGroupFunds.GetFundGroupFundsByFundGroupId(
                 It.IsAny<int>()))
                 .Returns(new List<Entities.FundGroupFund>()
                 {
                    {
                        new Entities.FundGroupFund()
                        {
                            FundGroupFundId = 1,
                            FundGroupId = 1,
                            FundCode = "F1"
                        }
                    }
                 });

            MockUnitOfWork.Setup(x => x.FundGroupFunds.Add(
                 It.IsAny<Entities.FundGroupFund>()));

            MockUnitOfWork.Setup(x => x.FundGroupFunds.Remove(
                 It.IsAny<Entities.FundGroupFund>()));

            MockUnitOfWork.Setup(x => x.FundGroups.Update(It.IsAny<Entities.FundGroup>()));
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
            var result = service.Update(new Entities.FundGroup());

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
            var result = service.Update(new Entities.FundGroup());

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
            var result = service.Update(new Entities.FundGroup());

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
            var result = service.Update(new Entities.FundGroup());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void FundGroupNameIsUpdated()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(new Entities.FundGroup() { Name = "NewName" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
            Assert.IsInstanceOfType(result.Data, typeof(Entities.FundGroup));
            Assert.AreEqual(((Entities.FundGroup)result.Data).Name, "NewName");
        }

        [TestMethod]
        public void CanDeleteAndAddFundGroupFunds()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(new Entities.FundGroup()
            {
                Name = "NewName",
                FundGroupFunds = new List<Entities.FundGroupFund>()
                {
                    {
                        new Entities.FundGroupFund()
                        {
                            FundGroupFundId = 2,
                            FundGroupId = 2,
                            FundCode = "F2"
                        }
                    }
                 }
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
            Assert.IsInstanceOfType(result.Data, typeof(Entities.FundGroup));
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
                    var result = service.Update(new Entities.FundGroup());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.AreEqual(result.Success, true);

                },
                () =>
                {

                    // Act
                    var result = service.Update(new Entities.FundGroup());

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
