using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.AccountHolder;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.AccountHolderControllerDependencies;

namespace Admin.UnitTests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AccountHolderControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchViewModel>> _mockListViewModelBuider = new Mock<IModelBuilder<ListViewModel, SearchViewModel>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, string>> _mockDetailsViewModelBuider = new Mock<IModelBuilder<DetailsViewModel, string>>();
        private readonly Mock<IModelCommand<LookupViewModel>> _mockLookupViewModelCommand = new Mock<IModelCommand<LookupViewModel>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockDetailsViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                   null,
                   _mockDetailsViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: listViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDetailsViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                   _mockListViewModelBuider.Object,
                   null,
                    _mockLookupViewModelCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenDetailsViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                   _mockListViewModelBuider.Object,
                   null,
                    _mockLookupViewModelCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockLogger.Object,
                _mockListViewModelBuider.Object,
                _mockDetailsViewModelBuider.Object,
                _mockLookupViewModelCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
        }
    }
}
