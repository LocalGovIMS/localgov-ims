using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Security;
using MessagePack;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Strategy = BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    public class TestBase
    {
        protected readonly Mock<IAccountHolderRepository> MockAccountHolderRepository = new Mock<IAccountHolderRepository>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected Strategy Strategy;

        protected void SetupDependencies()
        {
            MockAccountHolderRepository.Setup(x => x.BulkSelectNotExisting(It.IsAny<IEnumerable<Entities.AccountHolder>>()))
                .Returns(AccountHoldersToInsert());

            MockAccountHolderRepository.Setup(x => x.BulkInsert(It.IsAny<IEnumerable<Entities.AccountHolder>>()));

            MockAccountHolderRepository.Setup(x => x.BulkUpdate(It.IsAny<IEnumerable<Entities.AccountHolder>>(), It.IsAny<IEnumerable<string>>()));
        }

        protected void SetupDependenciesForFailure(string errorMessage)
        {
            SetupDependencies();

            MockAccountHolderRepository.Setup(x => x.BulkInsert(It.IsAny<IEnumerable<Entities.AccountHolder>>()))
                .Throws(new NotImplementedException(errorMessage));
        }

        protected void SetupStrategy()
        {
            Strategy = new Strategy(
                MockAccountHolderRepository.Object,
                MockSecurityContext.Object);
        }

        protected ImportProcessingStrategyArgs GetArgs()
        {
            var rows = AccountHoldersToInsert().Concat(AccountHoldersToUpdate());

            return new ImportProcessingStrategyArgs()
            {
                Import = new Import()
                {
                    Id = 1,
                    CreatedByUserId = 1,
                    CreatedDate = DateTime.Now,
                    ImportTypeId = 1,
                    Notes = "Notes",
                    NumberOfRows = 1
                },
                ImportRows = AccountHoldersToInsert().Concat(AccountHoldersToUpdate())
                    .Select(x => new ImportRow() 
                    { 
                        Id = 1,
                        ImportId = 1,
                        Data = Convert.ToBase64String(MessagePackSerializer.Serialize(x, MessagePack.Resolvers.ContractlessStandardResolver.Options))
                    }).ToList()
            };
        }

        private IEnumerable<Entities.AccountHolder> AccountHoldersToInsert()
        {
            return new List<Entities.AccountHolder>()
            {
                new Entities.AccountHolder()
                {
                    AccountReference = "1234"
                },
                new Entities.AccountHolder()
                {
                    AccountReference = "2345"
                }
            };
        }

        private IEnumerable<Entities.AccountHolder> AccountHoldersToUpdate()
        {
            return new List<Entities.AccountHolder>()
            {
                new Entities.AccountHolder()
                {
                    AccountReference = "9876"
                },
                new Entities.AccountHolder()
                {
                    AccountReference = "8765"
                }
            };
        }

    }
}
