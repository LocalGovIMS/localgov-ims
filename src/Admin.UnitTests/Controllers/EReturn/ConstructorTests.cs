using Admin.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Controller = Admin.Controllers.EReturnController;

namespace Admin.UnitTests.Controllers.EReturn
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
