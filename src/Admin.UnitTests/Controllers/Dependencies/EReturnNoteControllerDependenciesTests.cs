using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturnNote;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.EReturnNoteControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EReturnNoteControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, int>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, int>>();

        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockCreateCommand.Object);

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
                    null,
                    _mockCreateCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    null);

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
                    _mockListViewModelBuilder.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: createCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateCommand);
        }
    }
}
