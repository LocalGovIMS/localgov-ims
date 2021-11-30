namespace BusinessLogic.Classes
{
    public class RefundStatus
    {
        public RefundStatusType Status { get; set; }
        public string PspReference { get; set; }
        public decimal? Amount { get; set; }
        public string Message { get; set; }


        public static RefundStatus AcceptedStatus(decimal amount)
        {
            return new RefundStatus()
            {
                Status = RefundStatusType.Accepted,
                Amount = amount
            };
        }

        public static RefundStatus SuccessStatus(string message, string pspReference, decimal amount)
        {
            return new RefundStatus()
            {
                Status = RefundStatusType.Success,
                Amount = amount,
                PspReference = pspReference,
                Message = message
            };
        }

        public static RefundStatus FailStatus(string message)
        {
            return new RefundStatus()
            {
                Status = RefundStatusType.Failed,
                Message = message
            };
        }

        public static RefundStatus ErrorStatus(string message)
        {
            return new RefundStatus()
            {
                Status = RefundStatusType.Error,
                Message = message
            };
        }
    }
}