using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.ImportProcessing
{
    public interface IFileImportProcessor
    {
        IResult Process(FileImportProcessorArgs args);
    }
}