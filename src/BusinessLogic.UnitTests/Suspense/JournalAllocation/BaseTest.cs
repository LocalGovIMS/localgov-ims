using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Suspense;
using BusinessLogic.Suspense.JournalAllocation;
using log4net;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Suspense.JournalAllocation
{
    public class BaseTest
    {
        protected Mock<ILog> MockLogger = new Mock<ILog>();
        protected Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected Mock<ITransactionJournalService> MockTransactionJournalService = new Mock<ITransactionJournalService>();
        protected Mock<ISuspenseJournalService> MockSuspenseJournalService = new Mock<ISuspenseJournalService>();
        protected Mock<IJournalAllocationStrategyValidator> MockJournalAllocationStrategyValidator = new Mock<IJournalAllocationStrategyValidator>();
        
        protected void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Mop>() {
                    new Mop()
                    {
                        MopCode = "JR",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
                                Value = "True"
                            }
                        }
                    },
                    new Mop()
                    {
                        MopCode = "JN",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                MetadataKey = new MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournal
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Vats.GetAll(It.IsAny<bool>()))
                .Returns(new List<Vat>() {
                    new Vat()
                    {
                        VatCode = "M0",
                        Metadata = new List<VatMetadata>()
                        {
                            new VatMetadata()
                            {
                                MetadataKey = new MetadataKey()
                                {
                                    Name = VatMetadataKeys.IsASuspenseTransactionVatCode
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Fund>() {
                    new Fund()
                    {
                        FundCode = "01",
                        Metadata = new List<FundMetadata>()
                        {
                            new FundMetadata()
                            {
                                MetadataKey = new MetadataKey()
                                {
                                    Name = FundMetadataKeys.IsASuspenseTransactionFund
                                },
                                Value = "True"
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Suspenses.Lock(It.IsAny<List<int>>(), It.IsAny<Guid>()));
            MockUnitOfWork.Setup(x => x.Suspenses.Unlock(It.IsAny<Guid>()));

            MockUnitOfWork.Setup(x => x.Suspenses.GetSuspensesBeingProcessed(It.IsAny<Guid>()))
                .Returns(new List<Entities.Suspense>()
                {
                    new Entities.Suspense()
                    {
                        Id = 1,
                        AccountNumber = "1234",
                        Amount = 10,
                        CreatedAt = DateTime.Now,
                        ImportId = 1,
                        Narrative = "Test",
                        ProcessId = Guid.NewGuid().ToString(),
                        TransactionDate = DateTime.Now
                    }
                });

            MockTransactionJournalService.Setup(x => x.CreateJournal(
                It.IsAny<BusinessLogic.Models.TransferItem>(),
                It.IsAny<BusinessLogic.Models.TransferItem>(),
                It.IsAny<List<BusinessLogic.Models.TransferItem>>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()))
                .Returns(new Result());

            MockSuspenseJournalService.Setup(x => x.GetPspReference())
                .Returns("PspReference");

            MockSuspenseJournalService.Setup(x => x.CreateJournal(
                It.IsAny<BusinessLogic.Models.TransferItem>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()))
                .Returns(new Result());
        }

        protected List<int> GetSuspenseIds()
        {
            return GetSuspenseIds(1);
        }

        protected List<int> GetSuspenseIds(int numberOfItems)
        {
            var list = new List<Entities.Suspense>();

            for(var i = 1; i<= numberOfItems; i++)
            {
                list.Add(new Entities.Suspense()
                {
                    Id = i,
                    AccountNumber = "1234",
                    Amount = 10,
                    CreatedAt = DateTime.Now,
                    ImportId = 1,
                    Narrative = "Test",
                    ProcessId = Guid.NewGuid().ToString(),
                    TransactionDate = DateTime.Now
                });
            }

            MockUnitOfWork.Setup(x => x.Suspenses.GetSuspensesBeingProcessed(It.IsAny<Guid>()))
                .Returns(list);

            return list.Select(x => x.Id).ToList();
        }

        protected List<Journal> GetJournals()
        {
            return GetJournals(1);
        }

        protected List<Journal> GetJournals(int numberOfItems)
        {
            var list = new List<Journal>();

            for (var i = 1; i <= numberOfItems; i++)
            {
                list.Add(new Journal()
                {
                    AccountReference = "Test",
                    Amount = 10,
                    FundCode = "3",
                    MopCode = "M1",
                    Narrative = "Narrative",
                    VatCode = "V1"
                });
            }

            return list;
        }

        protected List<CreditNote> GetCreditNotes()
        {
            return GetCreditNotes(1);
        }

        protected List<CreditNote> GetCreditNotes(int numberOfItems)
        {
            var list = new List<CreditNote>();

            for (var i = 1; i <= numberOfItems; i++)
            {
                list.Add(new CreditNote()
                {
                    AccountReference = "Test",
                    Amount = 10,
                    FundCode = "N1",
                    VatCode = "V1"
                });
            }

            return list;
        }
    }
}
