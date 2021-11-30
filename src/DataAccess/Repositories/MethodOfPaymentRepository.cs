using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.MethodOfPayment;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class MethofOfPaymentRepository : Repository<Mop>, IMethodOfPaymentRepository
    {
        public MethofOfPaymentRepository(IncomeDbContext context) : base(context)
        {
        }

        public Mop GetMop(string id)
        {
            var item = IncomeDbContext.MOPs
                .Include(x => x.MetaData)
                .AsQueryable()
                .Where(x => x.MopCode == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public List<Mop> Search(SearchCriteria criteria, out int resultCount)
        {
            var mops = IncomeDbContext.MOPs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(criteria.Type))
            {
                mops = mops.Where(x => x.MetaData.Any(y => y.Key == criteria.Type && y.Value == "TRUE"));
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            mops = mops.ApplyFilters(Filters);

            resultCount = mops.Count();

            mops = mops
                .OrderByDescending(x => x.MopCode)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return mops.ToList();
        }

        public void Update(Mop entity)
        {
            var item = IncomeDbContext.MOPs
                .Include(x => x.MetaData)
                .AsQueryable()
                .Where(x => x.MopCode == entity.MopCode)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.MopName = entity.MopName;
            item.MaximumAmount = entity.MaximumAmount;
            item.MinimumAmount = entity.MinimumAmount;
            item.Disabled = entity.Disabled;
        }

        public IEnumerable<Mop> GetAll(bool includeDisabled)
        {
            if (includeDisabled)
            {
                return IncomeDbContext.MOPs
                    .Include(x => x.MetaData)
                    .AsQueryable()
                    .ApplyFilters(Filters).ToList();
            }

            return IncomeDbContext.MOPs
                .Include(x => x.MetaData)
                .AsQueryable()
                .Where(x => x.Disabled == false)
                .ApplyFilters(Filters)
                .ToList();
        }
    }
}