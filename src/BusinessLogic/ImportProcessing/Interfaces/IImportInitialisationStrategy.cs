using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.ImportProcessing
{
    public interface IImportInitialisationStrategy
    {
        void Initialise(ImportInitialisationStrategyArgs args);
    }
}