﻿using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.CheckDigitConfiguration;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.CheckDigitConfigurationControllerDependencies;

namespace Admin.UnitTests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CheckDigitConfigurationControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
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
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
        public void ThrowsCorrectExceptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                   _mockDetailsViewModelBuilder.Object,
                   _mockEditViewModelBuilder.Object,
                   _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: deleteCommand");
            }
        }
    }
}
