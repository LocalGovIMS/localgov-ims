using BusinessLogic.Enums;

namespace BusinessLogic.Validators.Payment
{
    public class PaymentValidationArgs
    {
        public string Reference { get; set; }
        public string FundCode { get; set; }
        public Entities.Fund Fund { get; set; }
        public decimal Amount { get; set; }
        public AccountReferenceValidationSource Source { get; set; }
        public Entities.AccountReferenceValidator AccountReferenceValidator { get; set; }
    }
}
