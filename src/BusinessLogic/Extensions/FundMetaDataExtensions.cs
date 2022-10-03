using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class FundMetadataExtensions
    {
        public static string GetValue(this ICollection<FundMetadata> metaData, string key)
        {
            if (metaData == null || !metaData.Any()) return string.Empty;

            var item = metaData.FirstOrDefault(x => x.MetadataKey.Name == key);

            return (item == null)
                ? string.Empty
                : item.Value;
        }
    }
}
