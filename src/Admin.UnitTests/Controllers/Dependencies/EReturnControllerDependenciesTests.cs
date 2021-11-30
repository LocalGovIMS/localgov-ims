using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.EReturn;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.EReturnControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EReturnControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<CreateViewModel, int>> _mockCreateViewModelBuilder = new Mock<IModelBuilder<CreateViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();

        private readonly Mock<IModelCommand<CreateViewModel>> _mockCreateCommand = new Mock<IModelCommand<CreateViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockApproverEditCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();
        private readonly Mock<IModelCommand<int>> _mockSubmitCommand = new Mock<IModelCommand<int>>();
        private readonly Mock<IModelCommand<ApproveViewModel>> _mockAuthoriseCommand = new Mock<IModelCommand<ApproveViewModel>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: listViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenCreateViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenCreateViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: createViewModelBuilder");
            }
        }


        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDetailsViewModelBuilderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    null,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    null,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editViewModelBuilder");
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
                    _mockCreateViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    null,
                    _mockApproverEditCommand.Object,
                    _mockEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    null,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    null,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: editCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenApproverEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    null,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenApproverEditCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    null,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: approverEditCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDeleteCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    null,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
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
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    null,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: deleteCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSubmitCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    null,
                    _mockAuthoriseCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenSubmitCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    null,
                    _mockAuthoriseCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: submitCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenAuthoriseCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenAuthoriseCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: authoriseCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockCreateViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockEditViewModelBuilder.Object,
                    _mockCreateCommand.Object,
                    _mockEditCommand.Object,
                    _mockApproverEditCommand.Object,
                    _mockDeleteCommand.Object,
                    _mockSubmitCommand.Object,
                    _mockAuthoriseCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateViewModelBuilder);
            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
            Assert.IsNotNull(dependencies.EditViewModelBuilder);
            Assert.IsNotNull(dependencies.CreateCommand);
            Assert.IsNotNull(dependencies.EditCommand);
            Assert.IsNotNull(dependencies.ApproverEditCommand);
            Assert.IsNotNull(dependencies.DeleteCommand);
            Assert.IsNotNull(dependencies.SubmitCommand);
            Assert.IsNotNull(dependencies.AuthoriseCommand);
        }
    }
}
