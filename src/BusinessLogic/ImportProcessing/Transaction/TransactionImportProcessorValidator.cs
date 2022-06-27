using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class TransactionImportProcessorValidator : IValidator<TransactionImport>
    {

        private readonly ITransactionImportTypeService _transactionImportTypeService;

        public TransactionImportProcessorValidator(ITransactionImportTypeService transactionImportTypeService)
        {
            _transactionImportTypeService = transactionImportTypeService ?? throw new ArgumentNullException("transactionImportTypeService");
        }

        public virtual void Validate(TransactionImport transactionImport)
        {
            ValidateType(transactionImport);
            ValidateRowCount(transactionImport);
            ValidateTotalAmount(transactionImport);
        }

        private void ValidateType(TransactionImport transactionImport)
        {
            var transactionImportType = _transactionImportTypeService.Get(transactionImport.TransactionImportTypeId);

            if (transactionImportType is null)
                throw new TransactionImportProcessorException("The Transaction Type is not valid");
        }

        private void ValidateRowCount(TransactionImport transactionImport)
        {
            if ((transactionImport.Rows?.Count() ?? 0) != transactionImport.TotalNumberOfTransactions)
                throw new TransactionImportProcessorException("The number of transactions expected does not match the number of transactions provided");
        }

        private void ValidateTotalAmount(TransactionImport transactionImport)
        {
            if (transactionImport.TotalAmount != transactionImport.Rows.Sum(x => x.ToProcessedTransaction().Amount))
                throw new TransactionImportProcessorException("The total amount specified does not match the total of all the rows provided");
        }

        public IValidator<TransactionImport> SetNext(IValidator<TransactionImport> validator)
        {
            throw new NotImplementedException();
        }
    }
}
