using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.Models
{
    [ExcludeFromCodeCoverage]
    class Address
    {
        public string Name { get; set; }
        public string HouseNumberOrName { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }

        public Address()
        {
            Name = "Fred Smith";
            HouseNumberOrName = "Town Hall";
            Street = "Church St.";
            Town = "Townville";
            PostCode = "T1 1HL";
        }
    }
}
