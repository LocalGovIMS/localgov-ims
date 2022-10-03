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
    public class SubmitTests : BaseTest
    {
        private void SetupUnitOfWork(EReturnStatus status
            , EReturnType type
            , List<Entities.EReturnCash> cashes
            , List<Entities.EReturnCheque> cheques)
        {
            MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
                .Returns(new Type()
                {
                    StatusId = (int)status,
                    TypeId = (int)type,
                    EReturnNo = "R1",
                    PendingTransactions = new List<Entities.PendingTransaction>() {
                        new Entities.PendingTransaction()
                        {
                            Id = 1,
                            Amount = 1,
                            TemplateRowId = 1

                        }
                    },
                    EReturnCashes = cashes,
                    EReturnCheques = cheques
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

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private List<Entities.EReturnCash> GetCash(string bagNumber)
        {
            return new List<Entities.EReturnCash>() {
                new Entities.EReturnCash()
                {
                    BagNumber = bagNumber
                }
            };
        }

        private List<Entities.EReturnCash> GetCash(string bagNumber, decimal amount)
        {
            return new List<Entities.EReturnCash>() {
                new Entities.EReturnCash()
                {
                    BagNumber = bagNumber,
                    Total = amount
                }
            };
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork(EReturnStatus.InProgress, EReturnType.Cash, GetCash("123456"), null);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Submit(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [DataRow(EReturnStatus.Submitted, "Unable to submit eReturn R1 - it has already been submitted")]
        [DataRow(EReturnStatus.Deleted, "Unable to submit eReturn R1 - it has already been deleted")]
        [DataRow(EReturnStatus.Voided, "Unable to submit eReturn R1 - it has already been voided")]
        [DataRow(EReturnStatus.Authorised, "Unable to submit eReturn R1 - it has already been authorised")]
        [TestMethod]
        public void CannotSubmitEReturnIfEReturnInTheseStatuses(EReturnStatus status, string errorMessage)
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(status, EReturnType.Cash, null, null);

            var service = GetService();

            // Act
            var result = service.Submit(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, errorMessage);
        }

        [TestMethod]
        public void CannotSubmitCashEReturnIfBagNoIsMissing()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.InProgress, EReturnType.Cash, GetCash(null), null);

            var service = GetService();

            // Act
            var result = service.Submit(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Unable to submit eReturn R1 - the bag number is missing");
        }

        [TestMethod]
        public void CannotSubmitChequeEReturnIfNoChequesExist()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.InProgress, EReturnType.Cheque, null, null);

            var service = GetService();

            // Act
            var result = service.Submit(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Unable to submit eReturn R1 - no cheque details have been provided");
        }

        [TestMethod]
        public void CannotSubmitChequeEReturnIfChequesSumZero()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork(EReturnStatus.InProgress, EReturnType.Cheque, null, new List<Entities.EReturnCheque> {
                new Entities.EReturnCheque() {
                    Amount = 0
                }
            });

            var service = GetService();

            // Act
            var result = service.Submit(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Error, "Unable to submit eReturn R1 - the total cheque value must be greater than zero");
        }

        //// SystemAdmin or Finance can update submitted

        //// SystemAdmin or EReturn can update standard

        //[TestMethod]
        //public void OnErrorReturnsFailure()
        //{
        //    // Arrange
        //    SetupSecurityContext(true);
        //    var service = GetService();

        //    // Act
        //    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //}

        //[TestMethod]
        //public void IncorrectSecurityReturnsFailure()
        //{
        //    // Arrange
        //    SetupSecurityContext(false);
        //    var service = GetService();

        //    // Act
        //    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsFalse(result.Success);
        //}

        //[TestMethod]
        //public void NoTransactionPendingsReturnsFailure()
        //{
        //    // Arrange
        //    SetupUnitOfWork(EReturnStatus.InProgress);
        //    SetupTemplateService();
        //    SetupSecurityContext(true);
        //    var service = GetService();

        //    var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
        //    eReturn.TransactionsPendings = null;

        //    // Act
        //    var result = service.Update(eReturn);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsFalse(result.Success);
        //    Assert.AreEqual(result.Error, "Data for eReturn R1 is incomplete");
        //}

        //[DataRow(0)]
        //[DataRow(null)]
        //[TestMethod]
        //public void ZeroOrNullTransactionValuesCanStillBeSaved(int? amount)
        //{
        //    // Arrange
        //    SetupUnitOfWork(EReturnStatus.InProgress);
        //    SetupTemplateService();
        //    SetupSecurityContext(true);
        //    var service = GetService();

        //    var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
        //    foreach (var item in eReturn.TransactionsPendings)
        //    {
        //        item.Amount = amount;
        //    }

        //    // Act
        //    var result = service.Update(eReturn);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsTrue(result.Success);
        //}

        //[TestMethod]
        //public void UnmatchedTemplateRowIdReturnsFailure()
        //{
        //    // Arrange
        //    SetupUnitOfWork(EReturnStatus.InProgress);

        //    MockUnitOfWork.Setup(x => x.EReturns.GetEReturn(It.IsAny<int>()))
        //        .Returns(new Type()
        //        {
        //            StatusId = (int)EReturnStatus.InProgress,
        //            TransactionsPendings = new List<Entities.TransactionsPending>() {
        //                new Entities.TransactionsPending()
        //                {
        //                    ID = 2,
        //                    Amount = 1,
        //                    TemplateRowID = 1
        //                }
        //            }
        //        });

        //    SetupTemplateService();
        //    SetupSecurityContext(true);
        //    var service = GetService();           

        //    // Act
        //    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsFalse(result.Success);
        //    Assert.AreEqual(result.Error, "Data for eReturn R1 is incomplete");
        //}

        //[DataRow(false, true, "eReturn reference 'Test' is not valid")]
        //[DataRow(true, false, "eReturn description 'Test' is not valid")]
        //[TestMethod]
        //public void InvalidRowValuesReturnFailure(bool referenceValidatorSuccess, bool descriptionValidatorSuccess, string errorMessage)
        //{
        //    // Arrange
        //    SetupUnitOfWork(EReturnStatus.InProgress);
        //    SetupTemplateService();
        //    SetupSecurityContext(true);
        //    SetupValidators(referenceValidatorSuccess, descriptionValidatorSuccess);
        //    var service = GetService();

        //    // Act
        //    var result = service.Update(GetEReturn(EReturnStatus.InProgress, EReturnType.Cash));

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsFalse(result.Success);
        //    Assert.AreEqual(result.Error, errorMessage);
        //}

        //[DataRow(null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   "0")]
        //[DataRow("50.0", null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   "50.0")]
        //[DataRow(null,   "20.0", null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   "20.0")]
        //[DataRow(null,   null,   "10.0", null,   null,   null,   null,   null,   null,   null,   null,   null,   "10.0")]
        //[DataRow(null,   null,   null,   "5.0",  null,   null,   null,   null,   null,   null,   null,   null,   "5.0")]
        //[DataRow(null,   null,   null,   null,   "2.0",  null,   null,   null,   null,   null,   null,   null,   "2.0")]
        //[DataRow(null,   null,   null,   null,   null,   "1.0",  null,   null,   null,   null,   null,   null,   "1.0")]
        //[DataRow(null,   null,   null,   null,   null,   null,   "0.5",  null,   null,   null,   null,   null,   "0.5")]
        //[DataRow(null,   null,   null,   null,   null,   null,   null,   "0.2",  null,   null,   null,   null,   "0.2")]
        //[DataRow(null,   null,   null,   null,   null,   null,   null,   null,   "0.1",  null,   null,   null,   "0.1")]
        //[DataRow(null,   null,   null,   null,   null,   null,   null,   null,   null,   "0.05", null,   null,   "0.05")]
        //[DataRow(null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   "0.02", null,   "0.02")]
        //[DataRow(null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   null,   "0.01", "0.01")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "0.0")]
        //[DataRow("50.0", "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "50.0")]
        //[DataRow("0.0",  "20.0", "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "20.0")]
        //[DataRow("0.0",  "0.0",  "10.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "10.0")]
        //[DataRow("0.0",  "0.0",  "0.0",  "5.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "5.0")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "2.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "2.0")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "1.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "1.0")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.5", "0.0", "0.0", "0.0",  "0.0",  "0.0",  "0.5")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.2", "0.0", "0.0",  "0.0",  "0.0",  "0.2")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.1", "0.0",  "0.0",  "0.0",  "0.1")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.05", "0.0",  "0.0",  "0.05")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.02", "0.0",  "0.02")]
        //[DataRow("0.0",  "0.0",  "0.0",  "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0",  "0.0",  "0.01", "0.01")]
        //[TestMethod]
        //public void CashTotalCanBeCalculated(
        //    string pounds50,
        //    string pounds20,
        //    string pounds10,
        //    string pounds5,
        //    string pounds2,
        //    string pounds1,
        //    string pence50,
        //    string pence20,
        //    string pence10,
        //    string pence5,
        //    string pence2,
        //    string pence1,
        //    string total)
        //{
        //    // Arrange
        //    SetupUnitOfWork(EReturnStatus.InProgress);
        //    SetupTemplateService();
        //    SetupSecurityContext(true);
        //    SetupValidators(true, true);
        //    var service = GetService();

        //    var eReturn = GetEReturn(EReturnStatus.InProgress, EReturnType.Cash);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds50 = string.IsNullOrEmpty(pounds50) ? null : (decimal?)Convert.ToDecimal(pounds50);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds20 = string.IsNullOrEmpty(pounds20) ? null : (decimal?)Convert.ToDecimal(pounds20);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds10 = string.IsNullOrEmpty(pounds10) ? null : (decimal?)Convert.ToDecimal(pounds10);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds5 = string.IsNullOrEmpty(pounds5) ? null : (decimal?)Convert.ToDecimal(pounds5);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds2 = string.IsNullOrEmpty(pounds2) ? null : (decimal?)Convert.ToDecimal(pounds2);
        //    eReturn.EReturnCashes.FirstOrDefault().Pounds1 = string.IsNullOrEmpty(pounds1) ? null : (decimal?)Convert.ToDecimal(pounds1);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence50 = string.IsNullOrEmpty(pence50) ? null : (decimal?)Convert.ToDecimal(pence50);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence20 = string.IsNullOrEmpty(pence20) ? null : (decimal?)Convert.ToDecimal(pence20);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence10 = string.IsNullOrEmpty(pence10) ? null : (decimal?)Convert.ToDecimal(pence10);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence5 = string.IsNullOrEmpty(pence5) ? null : (decimal?)Convert.ToDecimal(pence5);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence2 = string.IsNullOrEmpty(pence2) ? null : (decimal?)Convert.ToDecimal(pence2);
        //    eReturn.EReturnCashes.FirstOrDefault().Pence1 = string.IsNullOrEmpty(pence1) ? null : (decimal?)Convert.ToDecimal(pence1);

        //    // Act
        //    var result = service.Update(eReturn);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(Result));
        //    Assert.IsTrue(result.Success);
        //    Assert.AreEqual(eReturn.EReturnCashes.FirstOrDefault().Total, Convert.ToDecimal(total));
        //}

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() {
                BusinessLogic.Security.Role.EReturns
            };

            SetupUnitOfWork(EReturnStatus.InProgress, EReturnType.Cash, GetCash("423432", 1), null);

            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.Submit(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Submit(1);

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
