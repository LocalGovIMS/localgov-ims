using Admin.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UnitTests.Controllers.PaymentIntegration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConstructorTests
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeIfDependenciesIsNull()
        {
            try
            {
                var controller = new PaymentIntegrationController(null);

            }
            catch (Exception exception)
            {
                exception.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionParamNameIfDependenciesIsNull()
        {
            try
            {
                var controller = new PaymentIntegrationController(null);

            }
            catch (ArgumentNullException exception)
            {
                exception.ParamName.ToLower().Should().Be("dependencies");
            }
        }
    }
}
