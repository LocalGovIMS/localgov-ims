using System;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.Models
{
    [ExcludeFromCodeCoverage]
    class PaymentCard
    {
        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string SecurityCode { get; set; }

        public string NameOnCard { get; set; }

        public PaymentCard(string cardNumber, DateTime expiryDate, string securityCode)
        {
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            SecurityCode = securityCode;
            NameOnCard = "Dr. Automated U. I. Test";
        }
    }
}
