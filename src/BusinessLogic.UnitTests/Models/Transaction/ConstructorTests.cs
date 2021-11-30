using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void Constructor_throws_NullReferenceException_when_processedTransactions_parameter_is_null()
        {
            // Arrange

            try
            {
                // Act
                var transaction = new BusinessLogic.Models.Transaction(
                    null,
                    null,
                    null,
                    null,
                    null,
                    null);
            }
            catch (Exception exception)
            {
                // Assert
                exception.Should().BeOfType(typeof(ArgumentNullException));
            }
        }
    }
}
