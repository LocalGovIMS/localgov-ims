using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(
        this IEnumerable<TSource> source,
        int batchSize)
        {
            var batch = new List<TSource>(1000);
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<TSource>(1000);
                }
            }

            if (batch.Any()) yield return batch;
        }
    }
}
