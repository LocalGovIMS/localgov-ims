using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class FundExtensions
    {
        public static bool IsACreditNoteEnabledFund(this Fund item)
        {
            return item.IsA(FundMetadataKeys.IsACreditNoteEnabledFund);
        }

        public static bool IsAnEReturnDefaultFund(this Fund item)
        {
            return item.IsA(FundMetadataKeys.IsAnEReturnDefaultFund);
        }

        public static bool IsASuspenseTransactionFund(this Fund item)
        {
            return item.IsA(FundMetadataKeys.IsASuspenseTransactionFund);
        }

        public static bool IsABasketFund(this Fund item)
        {
            return item.IsA(FundMetadataKeys.IsABasketFund);
        }

        private static bool IsA(this Fund item, string key)
        {
            if (item == null) return false;

            if (item.Metadata == null || !item.Metadata.Any()) return false;

            var metadata = item.Metadata.FirstOrDefault(x => x.MetadataKey.Name == key && Convert.ToBoolean(x.Value).Equals(true));

            return metadata != null;
        }
    }
}
