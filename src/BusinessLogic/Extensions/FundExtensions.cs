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
            return item.IsA(FundMetaDataKeys.IsACreditNoteEnabledFund);
        }

        public static bool IsAnEReturnDefaultFund(this Fund item)
        {
            return item.IsA(FundMetaDataKeys.IsAnEReturnDefaultFund);
        }

        public static bool IsASuspenseJournalFund(this Fund item)
        {
            return item.IsA(FundMetaDataKeys.IsASuspenseJournalFund);
        }

        public static bool IsABasketFund(this Fund item)
        {
            return item.IsA(FundMetaDataKeys.IsABasketFund);
        }

        private static bool IsA(this Fund item, string key)
        {
            if (item == null) return false;

            if (item.MetaData == null || !item.MetaData.Any()) return false;

            var metaData = item.MetaData.FirstOrDefault(x => x.Key == key && Convert.ToBoolean(x.Value).Equals(true));

            return metaData != null;
        }
    }
}
