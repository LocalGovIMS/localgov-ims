using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.AccountHolder
{
    [TestClass]
    public class FullNameAndTitleTests
    {
        private Entities.AccountHolder _accountHolder;

        public FullNameAndTitleTests()
        {
            _accountHolder = new Entities.AccountHolder()
            {
                Title = "Title",
                Forename = "Forename",
                Surname = "Surname"
            };
        }

        [TestMethod]
        public void FullNameAndTitle_returns_expected_value()
        {
            // Arrange

            // Act
            var result = _accountHolder.FullNameAndTitle();

            // Assert
            result.Should().Be($"{_accountHolder.Title} {_accountHolder.Forename} {_accountHolder.Surname}".Trim());
        }
    }
}
