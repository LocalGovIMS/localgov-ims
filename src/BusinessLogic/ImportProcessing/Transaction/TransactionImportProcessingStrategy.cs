using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using System;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessingStrategy : IImportProcessingStrategy
    {
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;

        public TransactionImportProcessingStrategy(
            IRuleEngine ruleEngine
            , ITransactionService transactionService)
        {
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
        }

        public void Process(ImportProcessingStrategyArgs args)
        {
            var processedTransactionToCreate = args.Row.ToProcessedTransaction();

            _ruleEngine.Process(processedTransactionToCreate);

            var result = _transactionService.CreateProcessedTransaction(new Services.CreateProcessedTransactionArgs()
            {
                GenerateNewReference = true,
                SaveChanges = false,
                ProcessedTransaction = processedTransactionToCreate
            });

            if (!result.Success)
                throw new ImportProcessingException(result.Error);
        }
    }
}
