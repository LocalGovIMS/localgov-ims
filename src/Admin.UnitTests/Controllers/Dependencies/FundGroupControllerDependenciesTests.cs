using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.FundGroup;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.FundGroupControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FundGroupControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuider = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockCreateViewModelBuider = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuider = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<IList<DetailsViewModel>, int>> _mockListViewModelBuider = new Mock<IModelBuilder<IList<DetailsViewModel>, int>>();

        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDetailsViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);

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
                    null,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreateViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    null,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCreateViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    null,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: createViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    null,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);

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
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    null,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);

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
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: listViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreateCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    null,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);

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
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    null,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object);
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
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    null,
                    _mockDeleteCommand.Object);

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
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    null,
                    _mockDeleteCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDeleteCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenDeleteCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuider.Object,
                    _mockCreateViewModelBuider.Object,
                    _mockEditViewModelBuider.Object,
                    _mockListViewModelBuider.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: deleteCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockLogger.Object,
                _mockDetailsViewModelBuider.Object,
                _mockCreateViewModelBuider.Object,
                _mockEditViewModelBuider.Object,
                _mockListViewModelBuider.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object,
                _mockDeleteCommand.Object);

            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateViewModelBuilder);
            Assert.IsNotNull(dependencies.EditViewModelBuilder);
            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateCommand);
            Assert.IsNotNull(dependencies.EditCommand);
            Assert.IsNotNull(dependencies.DeleteCommand);
        }
    }
}
