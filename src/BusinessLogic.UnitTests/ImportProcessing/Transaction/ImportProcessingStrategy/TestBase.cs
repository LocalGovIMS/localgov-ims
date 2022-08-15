using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Services;
using MessagePack;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategy
{
    public class TestBase
    {
        protected readonly Mock<IRuleEngine> MockRuleEngine = new Mock<IRuleEngine>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<ISuspenseService> MockSuspenseService = new Mock<ISuspenseService>();

        protected TransactionImportProcessingStrategy Strategy;

        protected void SetupDependencies()
        {
            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(x => x);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()))
                .Returns(new Result());

            MockSuspenseService.Setup(x => x.Create(It.IsAny<CreateSuspenseArgs>()))
               .Returns(new Result());
        }

        protected void SetupDependenciesForFailure(string errorMessage)
        {
            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(x => x);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()))
                .Returns(new Result(errorMessage));

            MockSuspenseService.Setup(x => x.Create(It.IsAny<CreateSuspenseArgs>()))
                .Returns(new Result(errorMessage));
        }

        protected void SetupStrategy()
        {
            Strategy = new TransactionImportProcessingStrategy(
                MockRuleEngine.Object,
                MockTransactionService.Object,
                MockSuspenseService.Object);
        }

        protected ImportProcessingStrategyArgs GetArgs(ProcessedTransaction processedTransaction)
        {
            return new ImportProcessingStrategyArgs()
            {
                Import = new Import()
                {
                    Id = 1,
                    CreatedByUserId = 1,
                    CreatedDate = System.DateTime.Now,
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
                        Data = Convert.ToBase64String(MessagePackSerializer.Serialize(processedTransaction, MessagePack.Resolvers.ContractlessStandardResolver.Options))
                    }
                }
            };
        }

        protected ProcessedTransaction CreatableTransaction()
        {
            return new ProcessedTransaction()
            {
                FundCode = "F1"
            };
        }

        protected ProcessedTransaction NonCreatableTransaction()
        {
            return new ProcessedTransaction()
            {
                FundCode = null
            };
        }
    }
}
