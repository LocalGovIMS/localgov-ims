using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using MessagePack;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportInitialisationStrategy
{
    public class TestBase
    {
        protected readonly Mock<IMetadataKeyService> MockMetadataKeyService = new Mock<IMetadataKeyService>();

        protected TransactionImportInitialisationStrategy Strategy;

        protected void SetupDependencies(int metadataKeyId)
        {
            MockMetadataKeyService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MetadataKey.SearchCriteria>()))
                .Returns(new BusinessLogic.Models.Shared.SearchResult<MetadataKey>()
                {
                    Items = new List<MetadataKey>()
                    {
                        new MetadataKey()
                        {
                            Id = metadataKeyId,
                            Description = "Test"
                        }
                    }
                });
        }

        //protected void SetupDependenciesForFailure(string errorMessage)
        //{
        //    MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
        //        .Returns<ProcessedTransaction>(x => x);

        //    MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()))
        //        .Returns(new Result(errorMessage));

        //    MockSuspenseService.Setup(x => x.Create(It.IsAny<CreateSuspenseArgs>()))
        //        .Returns(new Result(errorMessage));
        //}

        protected void SetupStrategy()
        {
            Strategy = new TransactionImportInitialisationStrategy(MockMetadataKeyService.Object);
        }

        protected ImportInitialisationStrategyArgs GetArgs(ProcessedTransaction processedTransaction)
        {
            return new ImportInitialisationStrategyArgs()
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
                ImportRows = new List<ImportRow>()
                {
                    new ImportRow() 
                    {
                        Id = 1,
                        ImportId = 1,
                        Data = Convert.ToBase64String(MessagePackSerializer.Serialize(processedTransaction))
                    },
                    new ImportRow()
                    {
                        Id = 1,
                        ImportId = 1,
                        Data = Convert.ToBase64String(MessagePackSerializer.Serialize(processedTransaction))
                    }
                }
            };
        }

        protected ProcessedTransaction Transaction()
        {
            return new ProcessedTransaction()
            {
                Amount = 10
            };
        }
    }
}
