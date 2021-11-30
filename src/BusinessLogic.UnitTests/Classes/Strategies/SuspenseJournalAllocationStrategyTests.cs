using BusinessLogic.Classes.Strategies;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Classes.Strategies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SuspenseJournalAllocationStrategyTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();
        private readonly Mock<ITransactionJournalService> _mockTransactionJournalService = new Mock<ITransactionJournalService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        [TestMethod]
        public void ConstructorAcceptsCorrectArguments()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Mop>() {
                    new Mop()
                    {
                        MopCode = "JR",
                        MetaData = new List<MopMetaData>()
                        {
                            new MopMetaData()
                            {
                                Key = MopMetaDataKeys.IsAJournalReallocation,
                                Value = "True"
                            }
                        }
                    }
                });

            _mockUnitOfWork.Setup(x => x.Vats.GetAll(It.IsAny<bool>()))
                .Returns(new List<Vat>() {
                    new Vat()
                    {
                        VatCode = "M0",
                        MetaData = new List<VatMetaData>()
                        {
                            new VatMetaData()
                            {
                                Key = VatMetaDataKeys.IsASuspenseJournalVatCode,
                                Value = "True"
                            }
                        }
                    }
                });

            _mockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Fund>() {
                    new Fund()
                    {
                        FundCode = "01",
                        MetaData = new List<FundMetaData>()
                        {
                            new FundMetaData()
                            {
                                Key = FundMetaDataKeys.IsASuspenseJournalFund,
                                Value = "True"
                            }
                        }
                    }
                });

            try
            {
                // Act
                var strategy = new SuspenseJournalAllocationStrategy(
                    _mockLogger.Object,
                    _mockUnitOfWork.Object,
                    _mockSecurityContext.Object,
                    _mockTransactionJournalService.Object,
                    _mockFundService.Object);
            }
            catch (Exception)
            {
                // Assert;
                Assert.Fail();
            }
        }

        //[TestMethod]
        //public void ExceptionReturnsIResult()
        //{
        //    // Arrange

        //    var mockSuspenseRepository = new Mock<ISuspenseRepository>();
        //    mockSuspenseRepository.Setup(x => x.Lock(It.IsAny<List<int>>(), It.IsAny<Guid>()));

        //    var unitOfWork = new UnitOfWork(
        //        new Mock<IncomeDbContext>().Object,
        //        new Mock<ISecurityContext>().Object,
        //        new Mock<IAccountHolderRepository>().Object,
        //        new Mock<IAccountValidationRepository>().Object,
        //        new Mock<IAccountValidationWeightingRepository>().Object,
        //        new Mock<IActivityLogRepository>().Object,
        //        new Mock<IEReturnRepository>().Object,
        //        new Mock<IEReturnCashRepository>().Object,
        //        new Mock<IEReturnChequeRepository>().Object,
        //        new Mock<IEReturnStatusRepository>().Object,
        //        new Mock<IEReturnTypeRepository>().Object,
        //        new Mock<IFundRepository>().Object,
        //        new Mock<IFundGroupRepository>().Object,
        //        new Mock<IFundGroupFundRepository>().Object,
        //        new Mock<IMethodOfPaymentRepository>().Object,
        //        new Mock<IOriginRepository>().Object,
        //        new Mock<IPendingTransactionRepository>().Object,
        //        new Mock<IRoleRepository>().Object,
        //        new Mock<IStopMessageRepository>().Object,
        //        mockSuspenseRepository.Object,
        //        new Mock<ITemplateRepository>().Object,
        //        new Mock<ITransactionRepository>().Object,
        //        new Mock<ITransactionNotificationRepository>().Object,
        //        new Mock<IUserRepository>().Object,
        //        new Mock<IUserFundGroupRepository>().Object,
        //        new Mock<IUserRoleRepository>().Object,
        //        new Mock<IUserTemplateRepository>().Object,
        //        new Mock<IVatRepository>().Object,
        //        new Mock<IAuditLogger>().Object,
        //        new Mock<ILog>().Object);


        //    var suspenseItems = new List<int>() { 1 };
        //    var journalItems = new List<Journal>() {
        //        new Journal() {
        //            AccountReference = "",
        //            Amount = 1,
        //            FundCode = "F1",
        //            VatCode = "V1"
        //        }
        //    };
        //    var creditNotes = new List<CreditNote>();

        //    var strategy = new SuspenseJournalAllocationStrategy(
        //            _mockLogger.Object,
        //            _mockUnitOfWork.Object,
        //            _mockSecurityContext.Object,
        //            _mockTransactionJournalService.Object);

        //    // Act
        //    var result = strategy.Execute(suspenseItems, journalItems, creditNotes);

        //    // Assert;
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(IResult));
        //}
    }
}
