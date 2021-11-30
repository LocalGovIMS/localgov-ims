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
            return item.IsA(VatMetaDataKeys.IsASuspenseJournalVatCode);
        }

        private static bool IsA(this Vat item, string key)
        {
            if (item == null) return false;

            if (item.MetaData == null || !item.MetaData.Any()) return false;

            var metaData = item.MetaData.FirstOrDefault(x => x.Key == key && Convert.ToBoolean(x.Value).Equals(true));

            return metaData != null;
        }
    }
}
