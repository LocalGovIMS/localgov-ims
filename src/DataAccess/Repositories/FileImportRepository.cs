using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FileImportRepository : Repository<FileImport>, IFileImportRepository
    {
        public FileImportRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public FileImport GetByTransactionImportId(int transactionImportId)
        {
            var item = IncomeDbContext.FileImports
                .Include(x => x.Rows)
                .AsQueryable()
                .Where(x => x.TransactionImportId == transactionImportId)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }
    }
}