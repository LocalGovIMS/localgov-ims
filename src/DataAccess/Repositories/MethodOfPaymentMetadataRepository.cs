﻿using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.MethodOfPaymentMetadata;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class MethodOfPaymentMetadataRepository : Repository<MopMetadata>, IMethodOfPaymentMetadataRepository
    {
        public MethodOfPaymentMetadataRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<MopMetadata> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.MopMetadata
                .Include(x => x.MetadataKey)
                .AsQueryable();

            items = items.Where(x => x.MopCode == criteria.MopCode);

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.Id)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public MopMetadata Get(int id)
        {
            var item = IncomeDbContext.MopMetadata
                .Include(x => x.MetadataKey)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(MopMetadata entity)
        {
            var item = IncomeDbContext.MopMetadata
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.MopCode = entity.MopCode;
            item.MetadataKeyId = entity.MetadataKeyId;
            item.Value = entity.Value;
        }
    }
}