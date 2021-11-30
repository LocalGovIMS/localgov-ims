using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Template;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.TemplateControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TemplateControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, int>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
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
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
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
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("listViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEditViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("editViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreateCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    null,
                    _mockEditCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCreateCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    null,
                    _mockEditCommand.Object);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("createCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(NullReferenceException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    null);
            }
            catch (NullReferenceException exception)
            {
                exception.Message.Should().Be("editCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.EditViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateCommand);
            Assert.IsNotNull(dependencies.EditCommand);
        }
    }
}
