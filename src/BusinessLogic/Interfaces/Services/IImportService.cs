using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportService
    {
        IResult LoadFromFile(string path);
        IResult Process(string batchReference);
    }
}
