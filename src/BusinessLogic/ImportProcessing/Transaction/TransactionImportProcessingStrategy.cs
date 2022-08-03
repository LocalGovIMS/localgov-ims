using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using System;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessingStrategy : IImportProcessingStrategy
    {
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;
        private readonly ISuspenseService _suspenseService;

        public TransactionImportProcessingStrategy(
            IRuleEngine ruleEngine
            , ITransactionService transactionService
            , ISuspenseService suspenseService)
        {
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _suspenseService = suspenseService ?? throw new ArgumentNullException("suspenseService");
        }

        public void Process(ImportProcessingStrategyArgs args)
        {
            var processedTransactionToCreate = args.Row.ToProcessedTransaction();

            _ruleEngine.Process(processedTransactionToCreate);

            if (processedTransactionToCreate.IsCreatable())
            {
                CreateTransaction(processedTransactionToCreate);
            }
            else
            {
                CreateSuspense(processedTransactionToCreate);
            }
        }

        private void CreateTransaction(Entities.ProcessedTransaction processedTransaction)
        {
            var result = _transactionService.CreateProcessedTransaction(new Services.CreateProcessedTransactionArgs()
            {
                GenerateNewReference = true,
                SaveChanges = false,
                ProcessedTransaction = processedTransaction
            });

            if (!result.Success)
                throw new ImportProcessingException(result.Error);
        }

        private void CreateSuspense(Entities.ProcessedTransaction processedTransaction)
        {
            var result = _suspenseService.Create(new Services.CreateSuspenseArgs()
            {
                SaveChanges = false,
                Suspense = processedTransaction.ToSuspense()
            });

            if (!result.Success)
                throw new ImportProcessingException(result.Error);
        }
    }
}
