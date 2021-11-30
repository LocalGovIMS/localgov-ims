using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class FundMetaDataExtensions
    {
        public static string GetValue(this ICollection<FundMetaData> metaData, string key)
        {
            if (metaData == null || !metaData.Any()) return string.Empty;

            var item = metaData.FirstOrDefault(x => x.Key == key);

            return (item == null)
                ? string.Empty
                : item.Value;
        }
    }
}
