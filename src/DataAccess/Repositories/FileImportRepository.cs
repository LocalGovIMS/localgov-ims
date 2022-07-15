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

        public FileImport GetByImportId(int importId)
        {
            var item = IncomeDbContext.FileImports
                .Include(x => x.Rows)
                .AsQueryable()
                .Where(x => x.ImportId == importId)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }
    }
}