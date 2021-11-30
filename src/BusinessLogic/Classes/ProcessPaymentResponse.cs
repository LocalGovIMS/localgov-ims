namespace BusinessLogic.Classes
{
    public class ProcessPaymentResponse
    {
        public string RedirectUrl { get; set; }
        public bool Success { get; set; }
        public bool IsLegacy { get; set; }

        public ProcessPaymentResponse()
        {
            RedirectUrl = string.Empty;
            Success = false;
            IsLegacy = false;
        }
    }
}