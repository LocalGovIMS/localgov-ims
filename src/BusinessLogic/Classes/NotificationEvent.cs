namespace BusinessLogic.Classes
{
    public static class NotificationEvent
    {
        public const string Authorisation = "AUTHORISATION";
        public const string Cancellation = "CANCELLATION";
        public const string Refund = "REFUND";
        public const string CancelOrRefund = "CANCEL_OR_REFUND";
        public const string Capture = "CAPTURE";
        public const string RefundedReversed = "REFUNDED_REVERSED";
        public const string CaptureFailed = "CAPTURE_FAILED";
        public const string RequestForInformation = "REQUEST_FOR_INFORMATION";
        public const string NotificationOfChargeback = "NOTIFICATION_OF_CHARGEBACK";
        public const string Chargeback = "CHARGEBACK";
        public const string ChargebackReversed = "CHARGEBACK_REVERSED";
        public const string ReportAvailable = "REPORT_AVAILABLE";
    }
}