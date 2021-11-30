using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.Models
{
    [ExcludeFromCodeCoverage]
    class Payment
    {
        public string Type { get; set; }
        public string Reference { get; set; }
        public string Amount { get; set; }
    }
}
