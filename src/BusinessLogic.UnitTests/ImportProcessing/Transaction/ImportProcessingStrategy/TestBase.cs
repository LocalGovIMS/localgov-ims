using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Services;
using Moq;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategy
{
    public class TestBase
    {
        protected readonly Mock<IRuleEngine> MockRuleEngine = new Mock<IRuleEngine>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();

        protected BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy Strategy;

        protected void SetupDependencies()
        {
            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(x => x);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()))
                .Returns(new Result());
        }

        protected void SetupDependenciesForFailure(string errorMessage)
        {
            MockRuleEngine.Setup(x => x.Process(It.IsAny<ProcessedTransaction>()))
                .Returns<ProcessedTransaction>(x => x);

            MockTransactionService.Setup(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()))
                .Returns(new Result(errorMessage));
        }

        protected void SetupStrategy()
        {
            Strategy = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                MockRuleEngine.Object,
                MockTransactionService.Object);
        }

        protected ImportProcessingStrategyArgs GetArgs()
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
                Row = new ImportRow()
                {
                    Id = 1,
                    ImportId = 1,
                    Data = Newtonsoft.Json.JsonConvert.SerializeObject(new ProcessedTransaction())
                }
            };
        }

    }
}
