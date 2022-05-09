using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.Fund;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundRepository : Repository<Fund>, IFundRepository
    {
        public FundRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<Fund> GetAll(bool includeDisabled)
        {
            if (includeDisabled)
            {
                return IncomeDbContext.Funds
                    .Include(x => x.MetaData)
                    .AsQueryable()
                    .ApplyFilters(Filters)
                    .ToList();
            }

            return IncomeDbContext.Funds
                .Include(x => x.MetaData)
                .AsQueryable()
                .Where(x => x.Disabled == false)
                .ApplyFilters(Filters)
                .ToList();
        }

        public Fund GetByFundCode(string fundCode)
        {
            return IncomeDbContext.Funds
                .Include(x => x.MetaData)
                .Include(s => s.Vat)
                .Include(s => s.AccountReferenceValidator)
                .ApplyFilters(Filters)
                .FirstOrDefault(x => x.FundCode == fundCode);
        }

        public IEnumerable<Fund> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.Funds
                .Include(x => x.MetaData)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.FundCode))
            {
                items = items.Where(x => x.FundCode.Contains(criteria.FundCode));
            }

            if (!string.IsNullOrEmpty(criteria.FundName))
            {
                items = items.Where(x => x.FundName.Contains(criteria.FundName));
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

        public void Update(Fund entity)
        {
            var item = IncomeDbContext.Funds
                .AsQueryable()
                .Where(x => x.FundCode == entity.FundCode)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.AccountExist = entity.AccountExist;
            item.AccountHolders = entity.AccountHolders;
            item.AquireAddress = entity.AquireAddress;
            item.DisplayName = entity.DisplayName;
            item.FundName = entity.FundName;
            item.MaximumAmount = entity.MaximumAmount;
            item.OverPayAccount = entity.OverPayAccount;
            item.StopMessages = entity.StopMessages;
            item.AccountReferenceValidatorId = entity.AccountReferenceValidatorId;
            item.VatOverride = entity.VatOverride;
            item.VatCode = entity.VatCode;
            item.Disabled = entity.Disabled;
        }
    }
}