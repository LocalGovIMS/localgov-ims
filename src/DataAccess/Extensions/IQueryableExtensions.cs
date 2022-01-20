using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter)
        {
            return filter == null
                ? query
                : query.Where(filter);
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, bool>>> filters)
        {
            if (filters == null) return query;

            foreach (var filter in filters)
            {
                query = query.ApplyFilter(filter);
            }

            return query;
        }

        public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source, bool track) where T : class
        {
            return track
                ? source
                : source.AsNoTracking();
        }
    }
}
