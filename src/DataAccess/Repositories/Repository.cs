using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IncomeDbContext IncomeDbContext;

        private IList<Expression<Func<TEntity, bool>>> _filters = new List<Expression<Func<TEntity, bool>>>();
        public IEnumerable<Expression<Func<TEntity, bool>>> Filters
        {
            get
            {
                return _filters;
            }
        }

        public void AddFilter(Expression<Func<TEntity, bool>> filter)
        {
            _filters.Add(filter);
        }

        protected IQueryable<TEntity> DbSet
        {
            get
            {
                return (_filters.Any())
                    ? IncomeDbContext.Set<TEntity>().AsQueryable().ApplyFilters(_filters)
                    : IncomeDbContext.Set<TEntity>().AsQueryable();
            }
        }

        public Repository(IncomeDbContext context)
        {
            IncomeDbContext = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsQueryable().ApplyFilters(_filters).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsQueryable().ApplyFilters(_filters).Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsQueryable().ApplyFilters(_filters).SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            IncomeDbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            IncomeDbContext.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            IncomeDbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            IncomeDbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            var bulkInsert = new Operations.BulkInsert(IncomeDbContext);

            bulkInsert.Execute(entities);
        }
    }
}