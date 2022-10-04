using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.FundMessage;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundMessageRepository : Repository<FundMessage>, IFundMessageRepository
    {
        public FundMessageRepository(IncomeDbContext context) : base(context)
        {
        }

        public IEnumerable<FundMessage> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.FundMessages
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .Include(x => x.Fund)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.FundCode))
            {
                items = items.Where(x => x.FundCode.Contains(criteria.FundCode));
            }

            if (!string.IsNullOrEmpty(criteria.Message))
            {
                items = items.Where(x => x.Message.Contains(criteria.Message));
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.FundCode)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public new IEnumerable<FundMessage> GetAll()
        {
            return IncomeDbContext.FundMessages
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .Include(x => x.Fund)
                .AsQueryable()
                .ApplyFilters(Filters)
                .ToList();
        }

        public FundMessage GetById(int id)
        {
            return IncomeDbContext.FundMessages
                .Include(x => x.Metadata)
                .Include(x => x.Metadata.Select(y => y.MetadataKey))
                .Include(x => x.Fund)
                .ApplyFilters(Filters)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(FundMessage entity)
        {
            var item = IncomeDbContext.FundMessages
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.FundCode = entity.FundCode;
            item.Message = entity.Message;
        }
    }
}