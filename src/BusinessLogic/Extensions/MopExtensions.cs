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
            return item.IsA(MopMetadataKeys.IsACashPayment);
        }

        public static bool IsAnEReturnChequePayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsAnEReturnChequePayment);
        }

        public static bool IsAJournal(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsAJournal);
        }

        public static bool IsAJournalReallocation(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsAJournalReallocation);
        }

        public static bool IsATransferOut(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsATransferOut);
        }

        public static bool IsATransferIn(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsATransferIn);
        }

        public static bool IsACardSelfServicePayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsACardSelfServicePayment);
        }

        public static bool IsACardAtpPayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsACardAtpPayment);
        }

        public static bool IsACardViaStaffPayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsACardViaStaffPayment);
        }

        public static bool IsAChequePayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsAChequePayment);
        }

        public static bool IsARefundablePayment(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsARefundablePayment);
        }

        public static bool IsACardPaymentFee(this Mop item)
        {
            return item.IsA(MopMetadataKeys.IsACardPaymentFee);
        }

        public static bool IsARechargeFee(this Mop item)
        {
            return IsA(item, MopMetadataKeys.IsARechargeFee);
        }

        public static bool IsACentralChargeFee(this Mop item)
        {
            return IsA(item, MopMetadataKeys.IsACentralChargeFee);
        }

        public static bool IsAPaymentReversal(this Mop item)
        {
            return IsA(item, MopMetadataKeys.IsAPaymentReversal);
        }

        private static bool IsA(this Mop item, string key)
        {
            if (item == null) return false;

            if (item.Metadata == null || !item.Metadata.Any()) return false;

            var metaData = item.Metadata.FirstOrDefault(x => x.Key == key && Convert.ToBoolean(x.Value).Equals(true));

            return metaData != null;
        }

        public static string BackgroundColour(this Mop item)
        {
            return GetMopMetadataValue(item, MopMetadataKeys.BackgroundColour, "#CCCCCC");
        }

        public static string TextColour(this Mop item)
        {
            return GetMopMetadataValue(item, MopMetadataKeys.TextColour, "#FFFFFF");
        }

        public static bool IncursAFee(this Mop item)
        {
            return GetMopMetadataValue(item, MopMetadataKeys.IncursAFee, false);
        }

        public static string GetMopMetadataValue(this Mop item, string key)
        {
            return GetMopMetadataValue(item, key, string.Empty);
        }

        public static string GetMopMetadataValue(this Mop item, string key, string defaultValue)
        {
            if (item.Metadata == null) return defaultValue;

            return item.Metadata.FirstOrDefault(x => x.Key == key)?.Value ?? defaultValue;
        }

        public static T GetMopMetadataValue<T>(this Mop item, string key)
        {
            return GetMopMetadataValue(item, key, default(T));
        }

        public static T GetMopMetadataValue<T>(this Mop item, string key, T defaultValue)
        {
            if (item.Metadata == null) return defaultValue;

            return item.Metadata.FirstOrDefault(x => x.Key == key)?.Value == null
                ? defaultValue
                : (T)Convert.ChangeType(item.Metadata.FirstOrDefault(x => x.Key == key)?.Value, typeof(T));
        }
    }
}
