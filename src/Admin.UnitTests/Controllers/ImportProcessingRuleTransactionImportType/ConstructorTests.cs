using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Controller = Admin.Controllers.ImportProcessingRuleTransactionImportTypeController;

namespace Admin.UnitTests.Controllers.ImportProcessingRuleTransactionImportType
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
                var controller = new Controller(null);

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
                var controller = new Controller(null);

            }
            catch (ArgumentNullException exception)
            {
                exception.ParamName.ToLower().Should().Be("dependencies");
            }
        }
    }
}
