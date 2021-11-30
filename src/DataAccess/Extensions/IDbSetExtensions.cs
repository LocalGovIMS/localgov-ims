using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Extensions
{
    public static class IDbSetExtensions
    {
        public static IDbSet<T> ApplyFilter<T>(this IDbSet<T> dbSet, Expression<Func<T, bool>> filter) where T : class
        {
            return filter == null
                ? dbSet
                : dbSet.Where(filter) as IDbSet<T>;
        }

        public static IDbSet<T> ApplyFilters<T>(this IDbSet<T> dbSet, IEnumerable<Expression<Func<T, bool>>> filters) where T : class
        {
            if (filters == null) return dbSet;

            foreach (var filter in filters)
            {
                dbSet.Where(filter);
            }

            return dbSet;
        }
    }
}
