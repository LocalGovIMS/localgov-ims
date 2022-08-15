namespace BusinessLogic.ImportProcessing
{
    public interface IImportProcessingStrategy
    {
        void Process(ImportProcessingStrategyArgs args);

        int NumberOfSuccessfullyProcessedRows { get; }
    }
}