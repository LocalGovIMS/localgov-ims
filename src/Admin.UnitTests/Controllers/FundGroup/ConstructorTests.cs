﻿using Admin.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UnitTests.Controllers.FundGroup
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
                var controller = new FundGroupController(null);

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
                var controller = new FundGroupController(null);

            }
            catch (ArgumentNullException exception)
            {
                exception.ParamName.ToLower().Should().Be("dependencies");
            }
        }
    }
}
