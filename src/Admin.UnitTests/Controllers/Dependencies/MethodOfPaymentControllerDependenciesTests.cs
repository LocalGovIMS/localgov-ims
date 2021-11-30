using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.MethodOfPayment;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.MethodOfPaymentControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MethodOfPaymentControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, string>>();
        private readonly Mock<IModelBuilder<IList<DetailsViewModel>, string>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<DetailsViewModel>, string>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();

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
                    null,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockListViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEditViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockListViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editViewModelBuilder");
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
                    _mockEditViewModelBuilder.Object,
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
        public void ThrowsCorrectExceptionDescriptionTypeWhenListViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
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
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
            Assert.IsNotNull(dependencies.EditViewModelBuilder);
            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateCommand);
            Assert.IsNotNull(dependencies.EditCommand);
        }
    }
}
