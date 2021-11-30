using BusinessLogic.Entities;

namespace BusinessLogic.ImportProcessing
{
    public interface IRuleEngine
    {
        ProcessedTransaction Process(ProcessedTransaction transaction);
    }
}
