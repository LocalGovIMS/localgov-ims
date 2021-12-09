using Admin.Classes.ViewModelBuilders.AccountHolder;
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
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuider = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs>> _mockDetailsViewModelBuider = new Mock<IModelBuilder<DetailsViewModel, DetailsViewModelBuilderArgs>>();
        private readonly Mock<IModelBuilder<EditViewModel, string>> _mockEditViewModelBuider = new Mock<IModelBuilder<EditViewModel, string>>();
        private readonly Mock<IModelCommand<LookupViewModel>> _mockLookupViewModelCommand = new Mock<IModelCommand<LookupViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

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
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
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
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

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
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    null,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    null,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLookupCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenLookupCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: lookupAccountHolderCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreateCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    null,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCreateCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    null,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: createCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                   _mockLogger.Object,
                    _mockListViewModelBuider.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockLookupViewModelCommand.Object,
                    _mockCreateCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockLogger.Object,
                _mockListViewModelBuider.Object,
                _mockDetailsViewModelBuider.Object,
                _mockEditViewModelBuider.Object,
                _mockLookupViewModelCommand.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
        }
    }
}
