using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.ImportProcessing
{
    public interface IImportProcessor
    {
        IResult Process(ImportProcessorArgs args);
    }
}