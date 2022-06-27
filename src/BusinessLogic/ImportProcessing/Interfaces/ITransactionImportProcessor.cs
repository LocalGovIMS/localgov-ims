using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.ImportProcessing
{
    public interface ITransactionImportProcessor
    {
        IResult Process(TransactionImportProcessorArgs args);
    }
}