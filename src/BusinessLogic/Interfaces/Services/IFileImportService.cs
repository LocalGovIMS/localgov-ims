using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Services
{
    public interface IFileImportService
    {
        IResult LoadFromFile(string path);
        IResult Process(string batchReference);
    }
}
