using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.AccountHolder
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        [DataRow("AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "Postcode", "AddressLine1, AddressLine2, AddressLine3, AddressLine4, Postcode")]
        [DataRow("", "AddressLine2", "AddressLine3", "AddressLine4", "Postcode", "AddressLine2, AddressLine3, AddressLine4, Postcode")]
        [DataRow("AddressLine1", "", "AddressLine3", "AddressLine4", "Postcode", "AddressLine1, AddressLine3, AddressLine4, Postcode")]
        [DataRow("AddressLine1", "AddressLine2", "", "AddressLine4", "Postcode", "AddressLine1, AddressLine2, AddressLine4, Postcode")]
        [DataRow("AddressLine1", "AddressLine2", "AddressLine3", "", "Postcode", "AddressLine1, AddressLine2, AddressLine3, Postcode")]
        [DataRow("AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "", "AddressLine1, AddressLine2, AddressLine3, AddressLine4")]
        [DataRow("AddressLine1 ", "AddressLine2  ", "AddressLine3   ", "AddressLine4     ", "Postcode     ", "AddressLine1, AddressLine2, AddressLine3, AddressLine4, Postcode")]
        [DataRow(" AddressLine1 ", "  AddressLine2", "   AddressLine3", "    AddressLine4", "     Postcode", "AddressLine1, AddressLine2, AddressLine3, AddressLine4, Postcode")]
        [DataRow("AddressLine1", " ", " ", " ", " ", "AddressLine1")]
        [DataRow(" ", "AddressLine2", " ", "      ", "Postcode", "AddressLine2, Postcode")]
        [DataRow("AddressLine1", "AddressLine2", "  ", "AddressLine4", "Postcode", "AddressLine1, AddressLine2, AddressLine4, Postcode")]
        [DataRow("AddressLine1", "AddressLine2     ", "   ", "    AddressLine4", "Postcode", "AddressLine1, AddressLine2, AddressLine4, Postcode")]
        [DataRow(" AddressLine1", " AddressLine2 ", " AddressLine3 ", " AddressLine4 ", "Postcode ", "AddressLine1, AddressLine2, AddressLine3, AddressLine4, Postcode")]
        [DataRow(null, null, null, null, null, "")]
        public void Address_returns_expected_value(
            string addressLine1,
            string addressLine2,
            string addressLine3,
            string addressLine4,
            string postcode,
            string expectedResult
            )
        {
            // Arrange
            var accountHolder = new Entities.AccountHolder()
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                AddressLine3 = addressLine3,
                AddressLine4 = addressLine4,
                Postcode = postcode
            };

            // Act
            var result = accountHolder.Address();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
