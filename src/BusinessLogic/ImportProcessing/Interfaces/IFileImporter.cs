using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.ImportProcessing
{
    public interface IFileImporter
    {
        IResult ImportFile(FileImporterArgs args);
    }
}