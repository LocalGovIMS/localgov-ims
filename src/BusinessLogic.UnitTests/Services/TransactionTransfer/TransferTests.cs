using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.TransactionTransfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferTests : BaseTransactionTransferTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Users.GetAll())
                .Returns(new List<Entities.User>() {
                    new Entities.User()
                });

            MockUnitOfWork.Setup(x => x.Transactions.Add(It.IsAny<Entities.ProcessedTransaction>()));

            MockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Mop>() {
                    new Mop()
                    {
                        MopCode = "10",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MopMetadataKey = new MopMetadataKey()
                                {
                                    Name = MopMetadataKeys.IsATransferIn
                                },
                                Value = "True"
                            }
                        }
                    },
                    new Mop()
                    {
                        MopCode = "11",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MopMetadataKey = new MopMetadataKey()
                                {
                                    Name = MopMetadataKeys.IsATransferOut
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>()))
                .Returns(new Entities.Fund()
                {
                    VatCode = "V1",
                    Vat = new Entities.Vat() { Percentage = 20 }
                });
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }


        private void SetupValidator(bool success)
        {
            MockTransactionTransferValidator.Setup(x => x.Validate(
                It.IsAny<TransferItem>(),
                It.IsAny<List<TransferItem>>()))
                .Returns(success
                    ? new Result()
                    : new Result("Error"));
        }


        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            SetupValidator(true);

            var service = GetService();

            // Act
            var result = service.Transfer(new List<TransferItem>() { new TransferItem() }, new TransferItem());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.AreEqual(true, ((IResult)result).Success);
        }

        [TestMethod]
        public void OnErrorReturnsIResultMessage()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupValidator(true);
            SetupUnitOfWork();

            MockTransactionTransferValidator.Setup(x => x.Validate(
                It.IsAny<TransferItem>(),
                It.IsAny<List<TransferItem>>()))
                .Throws(new NullReferenceException());

            var service = GetService();

            // Act
            var result = service.Transfer(new List<TransferItem>() { new TransferItem() }, new TransferItem());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.AreEqual(((IResult)result).Success, false);
        }

        [TestMethod]
        public void OnInvalidDataReturnsIResultMessage()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(false);
            SetupValidator(false);
            var service = GetService();

            // Act
            var result = service.Transfer(new List<TransferItem>() { new TransferItem() }, new TransferItem());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.AreEqual(((IResult)result).Success, false);
        }

    }
}
