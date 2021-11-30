using Admin.Interfaces.ModelBuilders;
using Admin.Models.SystemMessage;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.HomeControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HomeControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<IList<DetailsViewModel>, string>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<DetailsViewModel>, string>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLoggerIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(null, null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenLoggerIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(null, null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: log");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(_mockLogger.Object, null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(_mockLogger.Object, null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: listViewModelBuilder");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object);

            Assert.IsNotNull(dependencies.Log);
        }
    }
}
