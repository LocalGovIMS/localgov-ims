using Admin.Interfaces.ModelBuilders;
using Admin.Models.TransactionImport;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.TransactionImportControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransactionImportControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDetailsViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockListViewModelBuilder.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenDetailsViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                     _mockLogger.Object,
                     null,
                     _mockListViewModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenListViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                     _mockLogger.Object,
                     _mockDetailsViewModelBuilder.Object,
                     null);
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
        }
    }
}
