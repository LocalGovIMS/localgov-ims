using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.EReturn;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteTests : BaseTest
    {
        private void SetupUnitOfWork(EReturnStatus status)
        {
            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)status,
                    EReturnNo = "R1"
                });

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

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.Authorised);
            var service = GetService();

            // Act
            var result = service.Create(new Type());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(false);
            SetupUnitOfWork(EReturnStatus.Authorised);
            var service = GetService();

            // Act
            var result = service.Create(new Type());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [DataRow(EReturnStatus.Authorised, "Unable to delete eReturn R1 - it has already been authorised")]
        [DataRow(EReturnStatus.Deleted, "Unable to delete eReturn R1 - it has already been deleted")]
        [DataRow(EReturnStatus.Voided, "Unable to delete eReturn R1 - it has already been voided")]
        [TestMethod]
        public void CannotDeleteEReturnIfEReturnInTheseStatuses(EReturnStatus status, string errorMessage)
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(status);

            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, errorMessage);
        }

        [TestMethod]
        public void CannotDeleteEReturnIfProcessedTransactionsExist()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.Authorised);

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)EReturnStatus.InProgress,
                    EReturnNo = "R1",
                    ProcessedTransactions = new List<Entities.ProcessedTransaction>() {
                        new Entities.ProcessedTransaction() {
                            Id = 0
                        }
                    }
                });

            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Unable to delete eReturn R1 - processed transactions exist");
        }

        [DataRow(EReturnStatus.InProgress, EReturnStatus.Voided)]
        [DataRow(EReturnStatus.Submitted, EReturnStatus.Deleted)]
        [TestMethod]
        public void CheckCorrectStatusIsAppliedDuringDeletion(EReturnStatus initialStatus, EReturnStatus updatedStatus)
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.Authorised);

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)initialStatus,
                    EReturnNo = "R1"
                });

            var service = GetService();

            // Act
            var result = service.Delete(1);
            var eReturn = MockUnitOfWork.Object.EReturns.GetEReturn(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(eReturn.StatusId, (int)updatedStatus);
        }

        [TestMethod]
        public void CheckExceptionReturnsCorrectResult()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.Authorised);

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    EReturnNo = "R2",
                    ProcessedTransactions = null
                });

            var service = GetService();

            // Act
            var result = service.Delete(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.EReturnDelete };
            SetupUnitOfWork(EReturnStatus.Authorised);
            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Arrange
                    SetupUnitOfWork(EReturnStatus.InProgress);

                    // Act
                    var result = service.Delete(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Delete(1);

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
