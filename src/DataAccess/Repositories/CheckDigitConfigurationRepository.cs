using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.CheckDigitConfiguration;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class CheckDigitConfigurationRepository : Repository<CheckDigitConfiguration>, ICheckDigitConfigurationRepository
    {
        public CheckDigitConfigurationRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<CheckDigitConfiguration> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.CheckDigitConfigurations
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                items = items.Where(x => x.Name.Contains(criteria.Name));
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.Name)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public void Update(CheckDigitConfiguration entity)
        {
            var item = IncomeDbContext.CheckDigitConfigurations
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.ApplySubtraction = entity.ApplySubtraction;
            item.Modulus = entity.Modulus;
            item.Name = entity.Name;
            item.ResultSubstitutions = entity.ResultSubstitutions;
            item.SourceSubstitutions = entity.SourceSubstitutions;
            item.Type = entity.Type;
            item.Weightings = entity.Weightings;
        }
    }
}