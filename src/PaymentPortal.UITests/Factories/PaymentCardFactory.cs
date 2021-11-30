using PaymentPortal.UITests.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.Factories
{
    [ExcludeFromCodeCoverage]
    class PaymentCardFactory
    {
        private static readonly DateTime DefaultExpiryDate = new DateTime(2030, 03, 1);
        private const string DefaultSecurityCode = "737";

        public static PaymentCard GetPaymentCard()
        {
            return new PaymentCard("5555 4444 3333 1111", DefaultExpiryDate, DefaultSecurityCode);
        }

        public static PaymentCard GetMasterCardDebit()
        {
            return new PaymentCard("5500 0000 0000 0004", DefaultExpiryDate, DefaultSecurityCode);
        }

        public static PaymentCard GetVisaDebit()
        {
            return new PaymentCard("4444 3333 2222 1111", DefaultExpiryDate, DefaultSecurityCode);
        }

        public static PaymentCard GetExpiredCard()
        {
            return new PaymentCard("4444 3333 2222 1111", DateTime.Today.AddMonths(-2), DefaultSecurityCode);
        }

        public static PaymentCard GetInvalidCard()
        {
            return new PaymentCard("4444 3333 2222 1111", DefaultExpiryDate, "111");
        }
    }
}