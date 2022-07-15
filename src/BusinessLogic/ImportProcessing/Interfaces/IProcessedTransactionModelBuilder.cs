using BusinessLogic.Models;

namespace BusinessLogic.ImportProcessing
{
    public interface IProcessedTransactionModelBuilder
    {
        ProcessedTransactionModel BuildFromCsvRow(string csvRow, int importId);
    }
}