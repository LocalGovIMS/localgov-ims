using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportRepository : IRepository<Import>
    {
        Import GetByBatchReference(string batchReference);
    }
}