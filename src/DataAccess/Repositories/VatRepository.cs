using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class VatRepository : Repository<Vat>, IVatRepository
    {
        public VatRepository(IncomeDbContext context) : base(context)
        {
        }

        public Vat GetVatByVatCode(string vatCode)
        {
            return IncomeDbContext.Vat
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .ApplyFilters(Filters)
                .Single(x => x.VatCode == vatCode);
        }

        public void Update(Vat entity)
        {
            var item = IncomeDbContext.Vat
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .AsQueryable()
                .Where(x => x.VatCode == entity.VatCode)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Percentage = entity.Percentage;
            item.Disabled = entity.Disabled;
        }

        public IEnumerable<Vat> GetAll(bool includeDisabled)
        {
            if (includeDisabled)
            {
                return IncomeDbContext.Vat
                    .Include(x => x.Metadata)
                    .Include(x => x.Metadata.Select(y => y.MetadataKey))
                    .AsQueryable()
                    .ApplyFilters(Filters)
                    .ToList();
            }

            return IncomeDbContext.Vat
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .AsQueryable()
                .Where(x => x.Disabled == false)
                .ApplyFilters(Filters)
                .ToList();
        }
    }
}