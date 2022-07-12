using BusinessLogic.Entities;

namespace BusinessLogic.ImportProcessing
{
    public interface IRuleEngine
    {
        ProcessedTransaction Process(ProcessedTransaction transaction);
        ProcessedTransaction Process(ProcessedTransaction transaction, int transactionImportTypeId);
    }
}
