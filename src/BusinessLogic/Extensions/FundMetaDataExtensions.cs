using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class FundMetadataExtensions
    {
        public static string GetValue(this ICollection<FundMetadata> metadata, string key)
        {
            if (metadata == null || !metadata.Any()) return string.Empty;

            var item = metadata.FirstOrDefault(x => x.MetadataKey.Name == key);

            return (item == null)
                ? string.Empty
                : item.Value;
        }
    }
}
