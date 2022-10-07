using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class VatExtensions
    {
        public static bool IsASuspenseJournalVatCode(this Vat item)
        {
            return item.IsA(VatMetadataKeys.IsASuspenseJournalVatCode);
        }

        private static bool IsA(this Vat item, string key)
        {
            if (item == null) return false;

            if (item.Metadata == null || !item.Metadata.Any()) return false;

            var metadata = item.Metadata.FirstOrDefault(x => x.MetadataKey.Name == key && Convert.ToBoolean(x.Value).Equals(true));

            return metadata != null;
        }
    }
}
