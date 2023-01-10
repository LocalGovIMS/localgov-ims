using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Type = BusinessLogic.Entities.EReturn;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UpdateTests : BaseTest
    {
        private void SetupUnitOfWork(EReturnStatus status)
        {
            MockUnitOfWork.Setup(x => x.EReturnCashes.Update(It.IsAny<Entities.EReturnCash>(), It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.EReturnCheques.Update(It.IsAny<List<Entities.EReturnCheque>>(), It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.PendingTransactions.Update(It.IsAny<List<Entities.PendingTransaction>>(), It.IsAny<int>()));

            MockUnitOfWork.Setup(x => x.EReturns.Add(It.IsAny<Type>()));

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)status,
                    PendingTransactions = new List<Entities.PendingTransaction>() {
                        new Entities.PendingTransaction()
                        {
                            Id = 1,
                            Amount = 1,
                            TemplateRowId = 1
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Templates.GetTemplate(It.IsAny<int>()))
                .Returns(new Entities.Template()
                {
                    Id = 1,
                    TemplateRows = new List<Entities.TemplateRow>()
                    {
                        new Entities.TemplateRow()
                        {
                            Id = 1,
                            Reference = "Test",
                            VatCode = "V1",
                            Description = "Test",
                            VAT = new Entities.Vat()
                            {
                                Percentage = 20
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Fund>() {
                    new Entities.Fund()
                    {
                        FundCode = "13",
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = FundMetadataKeys.IsAnEReturnDefaultFund
                                },
                                Value = "True"
                            }
                        }
                    }
                });
        }

        private void SetupTemplateService()
        {
            MockEReturnTemplateService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Entities.Template()
                {
                    Id = 1,
                    TemplateRows = new List<Entities.TemplateRow>()
                    {
                        new Entities.TemplateRow()
                        {
                            Id = 1,
                            Reference = "Test",
                            VatCode = "V1",
                            Description = "Test",
                            VAT = new Entities.Vat()
                            {
                                Percentage = 20
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

        private void SetupValidators(bool referenceValidatorSuccess, bool descriptionValidatorSuccess)
        {
            MockEReturnReferenceValidator.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(referenceValidatorSuccess ? new Result() : new Result("Error"));

            MockEReturnDescriptionValidator.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(descriptionValidatorSuccess ? new Result() : new Result("Error"));
        }

        private Type GetEReturn(EReturnStatus status, EReturnType type)
        {
            return new Type()
            {
                Id = 1,
                EReturnNo = "R1",
                StatusId = (int)status,
                TypeId = (int)type,
                PendingTransactions = new List<Entities.PendingTransaction>() {
                    new Entities.PendingTransaction()
                    {
                        Id = 1,
                        Amount = 1,
                        AccountReference = "Test",
                        Narrative = "Test"
                    }
                },
                EReturnCashes = new List<Entities.EReturnCash>() {
                    new Entities.EReturnCash() {
                        Id = 1,
                        CreatedAt = DateTime.Now,
                        EReturnId = 1,
                        Pence1 = 0,
                        Pence2 = 0,
                        Pence5 = 0,
                        Pence10 = 0,
                        Pence20 = 0,
                        Pence50 = 0,
                        Pounds1 = 0,
                        Pounds2 = 0,
                        Pounds5 = 0,
                        Pounds10 = 0,
                        Pounds20 = 0,
                        Pounds50 = 0,
                        BagNumber = "123456789",
                        Total = 0
                    }
                }
            };
        }

        [DataRow(EReturnStatus.New)]
        [DataRow(EReturnStatus.InProgress)]
        [TestMethod]
        public void ReturnsCorrectType(EReturnStatus status)
        {
            // Arrange
            SetupUnitOfWork(status);
            SetupTemplateService();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(status, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [DataRow(EReturnStatus.Authorised, "eReturn R1 has already been authorised, so cannot be edited")]
        [DataRow(EReturnStatus.Deleted, "eReturn R1 has already been deleted, so cannot be edited")]
        [DataRow(EReturnStatus.Voided, "eReturn R1 has already been voided, so cannot be edited")]
        [TestMethod]
        public void CannotUpdateEReturnIfEReturnInTheseStatuses(EReturnStatus status, string errorMessage)
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(status);

            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(status, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, errorMessage);
        }

        [TestMethod]
        public void EReturnAuthoriserCanUpdateSubmitted()
        {
            // Arrange
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.EReturnAuthoriser)).Returns(true);
            SetupUnitOfWork(EReturnStatus.Submitted);

            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.Submitted, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void EReturnUserCannotUpdateSubmitted()
        {
            // Arrange
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.EReturns)).Returns(false);
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.EReturnAuthoriser)).Returns(false);
            SetupUnitOfWork(EReturnStatus.Submitted);

            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.Submitted, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void EReturnUserCanUpdateStandard()
        {
            // Arrange
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.EReturns)).Returns(true);
            MockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.EReturnAuthoriser)).Returns(false);
            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupValidators(true, true);

            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
        }

        // SystemAdmin or EReturn can update standard

        [TestMethod]
        public void OnErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.Authorised);

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Throws(new NullReferenceException());

            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

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
            var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void NoTransactionPendingsReturnsFailure()
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupTemplateService();
            SetupSecurityContext(true);
            var service = GetService();

            var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
            eReturn.PendingTransactions = null;

            // Act
            var result = service.Update(eReturn);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Data for eReturn R1 is incomplete");
        }

        [DataRow(0)]
        [DataRow(null)]
        [TestMethod]
        public void ZeroOrNullTransactionValuesCanStillBeSaved(int? amount)
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupTemplateService();
            SetupSecurityContext(true);
            var service = GetService();

            var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
            foreach (var item in eReturn.PendingTransactions)
            {
                item.Amount = amount;
            }

            // Act
            var result = service.Update(eReturn);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void UnmatchedTemplateRowIdReturnsFailure()
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress);

            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)EReturnStatus.InProgress,
                    PendingTransactions = new List<Entities.PendingTransaction>() {
                        new Entities.PendingTransaction()
                        {
                            Id = 2,
                            Amount = 1,
                            TemplateRowId = 1
                        }
                    }
                });

            SetupTemplateService();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Data for eReturn R1 is incomplete");
        }

        [DataRow(false, true, "eReturn reference 'Test' is not valid")]
        [DataRow(true, false, "eReturn description 'Test' is not valid")]
        [TestMethod]
        public void InvalidRowValuesReturnFailure(bool referenceValidatorSuccess, bool descriptionValidatorSuccess, string errorMessage)
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupTemplateService();
            SetupSecurityContext(true);
            SetupValidators(referenceValidatorSuccess, descriptionValidatorSuccess);
            var service = GetService();

            // Act
            var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, errorMessage);
        }

        [DataRow(null, null, null, null, null, null, null, null, null, null, null, null, "0")]
        [DataRow("50.0", null, null, null, null, null, null, null, null, null, null, null, "50.0")]
        [DataRow(null, "20.0", null, null, null, null, null, null, null, null, null, null, "20.0")]
        [DataRow(null, null, "10.0", null, null, null, null, null, null, null, null, null, "10.0")]
        [DataRow(null, null, null, "5.0", null, null, null, null, null, null, null, null, "5.0")]
        [DataRow(null, null, null, null, "2.0", null, null, null, null, null, null, null, "2.0")]
        [DataRow(null, null, null, null, null, "1.0", null, null, null, null, null, null, "1.0")]
        [DataRow(null, null, null, null, null, null, "0.5", null, null, null, null, null, "0.5")]
        [DataRow(null, null, null, null, null, null, null, "0.2", null, null, null, null, "0.2")]
        [DataRow(null, null, null, null, null, null, null, null, "0.1", null, null, null, "0.1")]
        [DataRow(null, null, null, null, null, null, null, null, null, "0.05", null, null, "0.05")]
        [DataRow(null, null, null, null, null, null, null, null, null, null, "0.02", null, "0.02")]
        [DataRow(null, null, null, null, null, null, null, null, null, null, null, "0.01", "0.01")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0")]
        [DataRow("50.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "50.0")]
        [DataRow("0.0", "20.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "20.0")]
        [DataRow("0.0", "0.0", "10.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "10.0")]
        [DataRow("0.0", "0.0", "0.0", "5.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "5.0")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "2.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "2.0")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "1.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "1.0")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.5", "0.0", "0.0", "0.0", "0.0", "0.0", "0.5")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.2", "0.0", "0.0", "0.0", "0.0", "0.2")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.1", "0.0", "0.0", "0.0", "0.1")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.05", "0.0", "0.0", "0.05")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.02", "0.0", "0.02")]
        [DataRow("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.01", "0.01")]
        [TestMethod]
        public void CashTotalCanBeCalculated(
            string pounds50,
            string pounds20,
            string pounds10,
            string pounds5,
            string pounds2,
            string pounds1,
            string pence50,
            string pence20,
            string pence10,
            string pence5,
            string pence2,
            string pence1,
            string total)
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupTemplateService();
            SetupSecurityContext(true);
            SetupValidators(true, true);
            var service = GetService();

            var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
            eReturn.EReturnCashes.FirstOrDefault().Pounds50 = string.IsNullOrEmpty(pounds50) ? null : (decimal?)Convert.ToDecimal(pounds50);
            eReturn.EReturnCashes.FirstOrDefault().Pounds20 = string.IsNullOrEmpty(pounds20) ? null : (decimal?)Convert.ToDecimal(pounds20);
            eReturn.EReturnCashes.FirstOrDefault().Pounds10 = string.IsNullOrEmpty(pounds10) ? null : (decimal?)Convert.ToDecimal(pounds10);
            eReturn.EReturnCashes.FirstOrDefault().Pounds5 = string.IsNullOrEmpty(pounds5) ? null : (decimal?)Convert.ToDecimal(pounds5);
            eReturn.EReturnCashes.FirstOrDefault().Pounds2 = string.IsNullOrEmpty(pounds2) ? null : (decimal?)Convert.ToDecimal(pounds2);
            eReturn.EReturnCashes.FirstOrDefault().Pounds1 = string.IsNullOrEmpty(pounds1) ? null : (decimal?)Convert.ToDecimal(pounds1);
            eReturn.EReturnCashes.FirstOrDefault().Pence50 = string.IsNullOrEmpty(pence50) ? null : (decimal?)Convert.ToDecimal(pence50);
            eReturn.EReturnCashes.FirstOrDefault().Pence20 = string.IsNullOrEmpty(pence20) ? null : (decimal?)Convert.ToDecimal(pence20);
            eReturn.EReturnCashes.FirstOrDefault().Pence10 = string.IsNullOrEmpty(pence10) ? null : (decimal?)Convert.ToDecimal(pence10);
            eReturn.EReturnCashes.FirstOrDefault().Pence5 = string.IsNullOrEmpty(pence5) ? null : (decimal?)Convert.ToDecimal(pence5);
            eReturn.EReturnCashes.FirstOrDefault().Pence2 = string.IsNullOrEmpty(pence2) ? null : (decimal?)Convert.ToDecimal(pence2);
            eReturn.EReturnCashes.FirstOrDefault().Pence1 = string.IsNullOrEmpty(pence1) ? null : (decimal?)Convert.ToDecimal(pence1);

            // Act
            var result = service.Update(eReturn);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.Success);
            Assert.AreEqual(eReturn.EReturnCashes.FirstOrDefault().Total, Convert.ToDecimal(total));
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() {
                BusinessLogic.Security.Role.EReturns,
                BusinessLogic.Security.Role.EReturnAuthoriser
            };

            SetupUnitOfWork(EReturnStatus.InProgress);
            SetupTemplateService();
            SetupValidators(true, true);

            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    if (MockSecurityContext.Object.IsInRole(BusinessLogic.Security.Role.Finance))
                    {
                        SetupUnitOfWork(EReturnStatus.Submitted);
                    }

                    // Act
                    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

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
