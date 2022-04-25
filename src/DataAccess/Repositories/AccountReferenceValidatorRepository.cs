using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.AccountReferenceValidator;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AccountReferenceValidatorRepository : Repository<AccountReferenceValidator>, IAccountReferenceValidatorRepository
    {
        public AccountReferenceValidatorRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public AccountReferenceValidator Get(int id)
        {
            return IncomeDbContext.AccountReferenceValidators
                .Include(c => c.CheckDigitConfiguration)
                .FirstOrDefault(x => x.Id == id);
        }

        public AccountReferenceValidator GetByFundCode(string fundCode)
        {
            var accountReferenceValidatorId = IncomeDbContext.Funds.FirstOrDefault(x => x.FundCode == fundCode).AccountReferenceValidatorId;

            return IncomeDbContext.AccountReferenceValidators
                .Include(c => c.CheckDigitConfiguration)
                .FirstOrDefault(x => x.Id == accountReferenceValidatorId);
        }

        public IEnumerable<AccountReferenceValidator> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.AccountReferenceValidators
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

        public void Update(AccountReferenceValidator entity)
        {
            var item = IncomeDbContext.AccountReferenceValidators
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.CharacterType = entity.CharacterType;
            item.CheckDigitConfigurationId = entity.CheckDigitConfigurationId;
            item.InputMask = entity.InputMask;
            item.MaxLength = entity.MaxLength;
            item.MinLength = entity.MinLength;
            item.Name = entity.Name;
            item.Regex = entity.Regex;
        }
    }
}