using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class MopExtensions
    {
        public static bool IsACashPayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsACashPayment);
        }

        public static bool IsAnEReturnChequePayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsAnEReturnChequePayment);
        }

        public static bool IsAJournal(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsAJournal);
        }

        public static bool IsAJournalReallocation(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsAJournalReallocation);
        }

        public static bool IsATransferOut(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsATransferOut);
        }

        public static bool IsATransferIn(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsATransferIn);
        }

        public static bool IsACardSelfServicePayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsACardSelfServicePayment);
        }

        public static bool IsACardAtpPayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsACardAtpPayment);
        }

        public static bool IsACardViaStaffPayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsACardViaStaffPayment);
        }

        public static bool IsAChequePayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsAChequePayment);
        }

        public static bool IsARefundablePayment(this Mop item)
        {
            return item.IsA(MopMetaDataKeys.IsARefundablePayment);
        }

        private static bool IsA(this Mop item, string key)
        {
            if (item == null) return false;

            if (item.MetaData == null || !item.MetaData.Any()) return false;

            var metaData = item.MetaData.FirstOrDefault(x => x.Key == key && Convert.ToBoolean(x.Value).Equals(true));

            return metaData != null;
        }

        public static string BackgroundColour(this Mop item)
        {
            return GetMopMetaDataValue(item, MopMetaDataKeys.BackgroundColour, "#CCCCCC");
        }

        public static string TextColour(this Mop item)
        {
            return GetMopMetaDataValue(item, MopMetaDataKeys.TextColour, "#FFFFFF");
        }

        public static string GetMopMetaDataValue(this Mop item, string key)
        {
            return GetMopMetaDataValue(item, key, string.Empty);
        }

        public static string GetMopMetaDataValue(this Mop item, string key, string defaultValue)
        {
            if (item.MetaData == null) return defaultValue;

            return item.MetaData.FirstOrDefault(x => x.Key == key)?.Value ?? defaultValue;
        }
    }
}
