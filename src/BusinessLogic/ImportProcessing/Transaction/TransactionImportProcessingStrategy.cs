using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessingStrategy : IImportProcessingStrategy
    {
        public int NumberOfSuccessfullyProcessedRows { get; private set; } = 0;

        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;
        private readonly ISuspenseService _suspenseService;
        private ImportProcessingStrategyArgs _args;
        private decimal _totalAmountImported = 0;
        private List<string> _errors = new List<string>();

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
            try
            {
                _args = args;

                foreach (var transaction in args.ImportRows.Select(x => x.ToProcessedTransaction()))
                {
                    transaction.ImportId = args.Import.Id;

                    ProcessTransaction(transaction);

                    NumberOfSuccessfullyProcessedRows++;
                }
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);
            }
            finally
            {
                args.Import.AddErrors(_errors);
            }            
        }

        private void ProcessTransaction(Entities.ProcessedTransaction transaction)
        {
            try
            {
                // We need to do this before the rule engine runs, as a rule could
                // change the amount, and we're only interested in the original value.
                _totalAmountImported += transaction.Amount ?? 0;

                _ruleEngine.Process(transaction, _args.Import.ImportTypeId);

                if (transaction.IsCreatable())
                {
                    CreateTransaction(transaction);
                }
                else
                {
                    CreateSuspense(transaction);
                }
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);
            }
        }

        private void CreateTransaction(Entities.ProcessedTransaction transaction)
        {
            var result = _transactionService.CreateProcessedTransaction(new Services.CreateProcessedTransactionArgs()
            {
                GenerateNewReference = true,
                SaveChanges = false,
                ProcessedTransaction = transaction
            });

            if (!result.Success)
                _errors.Add(result.Error);
        }

        private void CreateSuspense(Entities.ProcessedTransaction transaction)
        {
            var result = _suspenseService.Create(new Services.CreateSuspenseArgs()
            {
                SaveChanges = false,
                Suspense = transaction.ToSuspense()
            });

            if (!result.Success)
                _errors.Add(result.Error);
        }
    }
}
